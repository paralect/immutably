using System;
using Immutably.Transitions;

namespace Immutably.Aggregates
{
    public class AggregateSession<TAggregateId> : IAggregateSession<TAggregateId>
    {
        /// <summary>
        /// Aggregate store, this session is working with
        /// </summary>
        private readonly IAggregateStore _store;

        /// <summary>
        /// Aggregate ID for this session.
        /// AggregateSession can work only with one Aggregate.
        /// </summary>
        private readonly TAggregateId _aggregateId;

        /// <summary>
        /// Aggregate Context 
        /// </summary>
        private IAggregateContext _context;

        /// <summary>
        /// Aggregate ID for this session
        /// </summary>
        public TAggregateId AggregateId
        {
            get { return _aggregateId; }
        }

        /// <summary>
        /// Creates AggregateSession
        /// </summary>
        public AggregateSession(IAggregateStore store, TAggregateId aggregateId)
        {
            _store = store;
            _aggregateId = aggregateId;
        }

        public IAggregate LoadAggregate(Type aggregateType)
        {
            var stateType = _store.GetAggregateStateType(aggregateType);

            // Here we can load state from snapshot store, but we are starting from initial state.
            var initialState = _store.CreateState(stateType);

            // Create aggregate 
            IAggregate aggregate = _store.CreateAggregate(aggregateType);

            // Reading transitions and "spooling" of events to receive final state
            ITransition<TAggregateId> lastTransition = null;
            using (var reader = _store.TransitionStore.CreateStreamReader(_aggregateId))
            {
                foreach (var transition in reader.Read())
                {
                    aggregate.Reply(transition.Events);
                    lastTransition = transition;
                }
            }

            if (lastTransition == null)
                throw new AggregateDoesntExistException(aggregateType, _aggregateId);

            _context = _store.CreateAggregateContext(typeof(TAggregateId), stateType,
                lastTransition.StreamId,
                lastTransition.StreamSequence,
                initialState,
                null);

            aggregate.EstablishContext(_context);

            return aggregate;            
        }

        public IAggregate LoadOrCreateAggregate(Type aggregateType)
        {
            return (IAggregate)(Object)null;
        }

        public TAggregate CreateAggregate<TAggregate>()
            where TAggregate : IAggregate<TAggregateId>
        {
            var stateType = _store.GetAggregateStateType(typeof(TAggregate));

            // Here we can load state from snapshot store, but we are starting from initial state.
            var initialState = _store.CreateState(stateType);

            TAggregate aggregate = _store.CreateAggregate<TAggregate>();

            _context = _store.CreateAggregateContext(typeof(TAggregateId), stateType,
                _aggregateId,
                0,
                initialState,
                null);

            aggregate.EstablishContext(_context);

            return aggregate;
        }

        public IAggregate CreateAggregate(Type aggregateType)
        {
            throw new NotImplementedException();
        }

        public TAggregate LoadAggregate<TAggregate>()
            where TAggregate : IAggregate<TAggregateId>
        {
            return (TAggregate)LoadAggregate(typeof(TAggregate));
        }

        public TAggregate LoadOrCreateAggregate<TAggregate>()
            where TAggregate : IAggregate<TAggregateId>
        {
            return (TAggregate)(Object)null;
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
    }
}