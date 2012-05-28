using System;
using System.Runtime.InteropServices;
using Immutably.Data;
using Immutably.States;
using Immutably.Transitions;

namespace Immutably.Aggregates
{
    public class AggregateSession : IAggregateSession
    {
        /// <summary>
        /// Aggregate store, this session is working with
        /// </summary>
        private readonly AggregateStore _store;

        /// <summary>
        /// Aggregate ID for this session.
        /// AggregateSession can work only with one Aggregate.
        /// </summary>
        private readonly String _aggregateId;

        private readonly IDataFactory _dataFactory;
        private readonly AggregateFactory _aggregateFactory;

        /// <summary>
        /// Aggregate Context 
        /// </summary>
        private IAggregateContext _context;

        /// <summary>
        /// Aggregate ID for this session
        /// </summary>
        public String AggregateId
        {
            get { return _aggregateId; }
        }

        /// <summary>
        /// Creates AggregateSession
        /// </summary>
        public AggregateSession(AggregateStore store, String aggregateId, IDataFactory dataFactory, AggregateFactory aggregateFactory)
        {
            _store = store;
            _aggregateId = aggregateId;
            _dataFactory = dataFactory;
            _aggregateFactory = aggregateFactory;
        }

        public IAggregate LoadAggregate(Type aggregateType)
        {
            var definition = _aggregateFactory.GetAggregateDefinition(aggregateType);

            if (definition.AggregateKind == AggregateKind.Stateless)
            {
                ITransition transition;
                using (var reader = _store.TransitionStore.CreateStreamReader(_aggregateId))
                {
                    transition = reader.ReadLast();
                }

                if (transition == null)
                    throw new AggregateDoesntExistException(aggregateType, _aggregateId);

                var context = new StatelessAggregateContext(_aggregateId, transition.StreamSequence, _dataFactory);
                var aggregate = _store.CreateStatelessAggregate(aggregateType);
                aggregate.EstablishContext(context);
                return aggregate;
            }
            
            if (definition.AggregateKind == AggregateKind.Statefull)
            {
                // Here we can load state from snapshot store, but we are starting from initial state.
                var initialState = _store.CreateState(definition.StateType);

                var spooler = new StateSpooler(initialState);
                using (var reader = _store.TransitionStore.CreateStreamReader(_aggregateId))
                {
                    foreach (var transition in reader.ReadAll())
                        spooler.Spool(transition.Events, transition.StreamSequence);
                }

                if (spooler.Data == null)
                    throw new AggregateDoesntExistException(aggregateType, _aggregateId);

                // Create aggregate 
                IStatefullAggregate aggregate = _store.CreateStatefullAggregate(aggregateType);
                var context = new StatefullAggregateContext(spooler.State, _aggregateId, (int) spooler.Data, _dataFactory);
                aggregate.EstablishContext(context);

                return aggregate;      
            }

            throw new Exception("AggregateKind doesn't supported");
        }

        IStatefullAggregate IAggregateSession.LoadOrCreateAggregate(Type aggregateType)
        {
            return (IStatefullAggregate)(Object)null;
        }

        IStatefullAggregate IAggregateSession.CreateAggregate(Type aggregateType)
        {
            var stateType = _store.GetAggregateStateType(aggregateType);

            // Here we can load state from snapshot store, but we are starting from initial state.
            var initialState = _store.CreateState(stateType);

            IStatefullAggregate aggregate = _store.CreateStatefullAggregate(aggregateType);

/*            _context = _store.CreateAggregateContext(typeof(TAggregateId), stateType,
                _aggregateId,
                0,
                initialState,
                null);

            aggregate.EstablishContext(_context);*/

            return aggregate;
        }

        public void SaveChanges()
        {
            if (_context == null)
                return;

            if (!_context.Changed)
                return;

            using (var writer = _store.TransitionStore.CreateStreamWriter(_aggregateId))
            {
                writer.Write(_context.CurrentVersion, builder => builder
                    .AddEvent(_context.Changes[0])
                );
            }
        }

        public void Dispose()
        {
            
        }

        public TAggregate CreateAggregate<TAggregate>()
            where TAggregate : IStatefullAggregate
        {
            return (TAggregate)((IAggregateSession)this).CreateAggregate(typeof(TAggregate));
        }

        public TAggregate LoadAggregate<TAggregate>()
            where TAggregate : IStatefullAggregate
        {
            //base.LoadAggregate(typeof (TAggregate));
            return (TAggregate)((IAggregateSession)this).LoadAggregate(typeof(TAggregate));
        }

        public TAggregate LoadOrCreateAggregate<TAggregate>()
            where TAggregate : IStatefullAggregate
        {
            return (TAggregate)((IAggregateSession)this).LoadOrCreateAggregate(typeof(TAggregate));
        }
    }

}