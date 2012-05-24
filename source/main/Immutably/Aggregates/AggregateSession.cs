using System;
using System.Runtime.InteropServices;
using Immutably.States;
using Immutably.Transitions;

namespace Immutably.Aggregates
{
    public class AggregateSession : IAggregateSession
    {
        /// <summary>
        /// Aggregate store, this session is working with
        /// </summary>
        protected readonly IAggregateStore _store;

        /// <summary>
        /// Aggregate ID for this session.
        /// AggregateSession can work only with one Aggregate.
        /// </summary>
        protected readonly String _aggregateId;

        /// <summary>
        /// Aggregate Context 
        /// </summary>
        protected IAggregateContext _context;

        /// <summary>
        /// Aggregate ID for this session
        /// </summary>
        public Object AggregateId
        {
            get { return _aggregateId; }
        }

        /// <summary>
        /// Creates AggregateSession
        /// </summary>
        public AggregateSession(IAggregateStore store, String aggregateId)
        {
            _store = store;
            _aggregateId = aggregateId;
        }

        public IAggregate LoadAggregate(Type aggregateType)
        {
            var stateType = _store.GetAggregateStateType(aggregateType);

            // Here we can load state from snapshot store, but we are starting from initial state.
            var initialState = _store.CreateState(stateType);

            var spooler = new StateSpooler(initialState);
            using (var reader = _store.TransitionStore.CreateStreamReader(_aggregateId))
            {
                foreach (var transition in reader.Read())
                    spooler.Spool(transition.StreamSequence, transition.Events);
            }

//            if (lastTransition == null)
//                throw new AggregateDoesntExistException(aggregateType, _aggregateId);

            // Create aggregate 
            IAggregate aggregate = _store.CreateAggregate(aggregateType);
            /*_context = _store.CreateAggregateContext(typeof(TAggregateId), stateType,
                _aggregateId,
                spooler.Version,
                initialState,
                null);

            aggregate.EstablishContext(_context);*/

            return aggregate;            
        }

        IAggregate IAggregateSession.LoadOrCreateAggregate(Type aggregateType)
        {
            return (IAggregate)(Object)null;
        }

        IAggregate IAggregateSession.CreateAggregate(Type aggregateType)
        {
            var stateType = _store.GetAggregateStateType(aggregateType);

            // Here we can load state from snapshot store, but we are starting from initial state.
            var initialState = _store.CreateState(stateType);

            IAggregate aggregate = _store.CreateAggregate(aggregateType);

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
            where TAggregate : IAggregate
        {
            return (TAggregate)((IAggregateSession)this).CreateAggregate(typeof(TAggregate));
        }

        public TAggregate LoadAggregate<TAggregate>()
            where TAggregate : IAggregate
        {
            //base.LoadAggregate(typeof (TAggregate));
            return (TAggregate)((IAggregateSession)this).LoadAggregate(typeof(TAggregate));
        }

        public TAggregate LoadOrCreateAggregate<TAggregate>()
            where TAggregate : IAggregate
        {
            return (TAggregate)((IAggregateSession)this).LoadOrCreateAggregate(typeof(TAggregate));
        }
    }

}