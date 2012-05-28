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

        /// <summary>
        /// Returns aggregate or throws exception if aggregate wasn't found
        /// </summary>
        public IAggregate LoadAggregate(Type aggregateType)
        {
            var aggregate = LoadAggregateInternal(aggregateType);

            if (aggregate == null)
                throw new AggregateDoesntExistException(aggregateType, _aggregateId);

            return aggregate;
        }

        /// <summary>
        /// Returns aggregate or null, if it wasn't find
        /// </summary>
        private IAggregate LoadAggregateInternal(Type aggregateType)
        {
            var definition = _aggregateFactory.GetAggregateDefinition(aggregateType);

            // Check, if this is stateless aggregate
            if (definition.AggregateKind == AggregateKind.Stateless)
            {
                ITransition transition;
                using (var reader = _store.TransitionStore.CreateStreamReader(_aggregateId))
                {
                    // Read last transition
                    transition = reader.ReadLast();
                }

                // Aggregate doesn't exists, if transition == 0
                if (transition == null)
                    return null;

                return EstablishStatelessAggregate(aggregateType, _aggregateId, transition.StreamSequence, _dataFactory);
            }

            // Check, if this is statefull aggregate
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

                // Aggregate doesn't exists, if spooler.Data is null
                if (spooler.Data == null)
                    return null;

                return EstablishStatefullAggregate(aggregateType, spooler.State, _aggregateId, (int)spooler.Data, _dataFactory);
            }

            throw new Exception("Specified AggregateKind is not supported");
        }

        /// <summary>
        /// Returns aggregate if it already exists, or creates new.
        /// </summary>
        public IAggregate LoadOrCreateAggregate(Type aggregateType)
        {
            var aggregate = LoadAggregateInternal(aggregateType);

            if (aggregate == null)
                aggregate = CreateAggregate(aggregateType);

            return aggregate;
        }

        /// <summary>
        /// Creates and returns aggregate of specified type
        /// </summary>
        public IAggregate CreateAggregate(Type aggregateType)
        {
            var definition = _aggregateFactory.GetAggregateDefinition(aggregateType);

            if (definition.AggregateKind == AggregateKind.Statefull)
            {
                var initialState = _store.CreateState(definition.StateType);
                return EstablishStatefullAggregate(aggregateType, initialState, _aggregateId, 0, _dataFactory);
            }

            if (definition.AggregateKind == AggregateKind.Stateless)
            {
                return EstablishStatelessAggregate(aggregateType, _aggregateId, 0, _dataFactory);
            }

            throw new Exception(String.Format("Cannot create aggregate of type {0}", aggregateType));
        }

        private IStatefullAggregate EstablishStatefullAggregate(Type aggregateType, Object state, String aggregateId, Int32 version, IDataFactory dataFactory)
        {
            var aggregate = _store.CreateStatefullAggregate(aggregateType);
            _context = new StatefullAggregateContext(state, aggregateId, version, dataFactory);
            aggregate.EstablishContext((IStatefullAggregateContext) _context);
            return aggregate;
        }

        private IStatelessAggregate EstablishStatelessAggregate(Type aggregateType, String aggregateId, Int32 version, IDataFactory dataFactory)
        {
            var aggregate = _store.CreateStatelessAggregate(aggregateType);
            _context = new StatelessAggregateContext(aggregateId, version, dataFactory);
            aggregate.EstablishContext((IStatelessAggregateContext) _context);
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

        public TAggregate CreateAggregate<TAggregate>()
            where TAggregate : IAggregate
        {
            return (TAggregate)((IAggregateSession)this).CreateAggregate(typeof(TAggregate));
        }

        public TAggregate LoadAggregate<TAggregate>()
            where TAggregate : IAggregate
        {
            return (TAggregate)((IAggregateSession)this).LoadAggregate(typeof(TAggregate));
        }

        public TAggregate LoadOrCreateAggregate<TAggregate>()
            where TAggregate : IAggregate
        {
            return (TAggregate)((IAggregateSession)this).LoadOrCreateAggregate(typeof(TAggregate));
        }

        public void Dispose()
        {

        }
    }
}