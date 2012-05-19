using System;
using System.Linq;
using Escolar.Messages;
using Escolar.States;
using Escolar.Transitions;
using Paralect.Machine.Processes;

namespace Escolar.Aggregates
{
    public class AggregateSession<TAggregateId> : IAggregateSession<TAggregateId>
    {
        private readonly IAggregateStore _store;
        private readonly TAggregateId _aggregateId;

        public TAggregateId AggregateId
        {
            get { return _aggregateId; }
        }

        public AggregateSession(IAggregateStore store, TAggregateId aggregateId)
        {
            _store = store;
            _aggregateId = aggregateId;
        }

        public TAggregate LoadAggregate<TAggregate>()
            where TAggregate : IAggregate<TAggregateId>
        {
            // Here we can load state from snapshot store, but we are starting from initial state.
            var initialState = _store.CreateStateForAggregate(typeof(TAggregate));

            TAggregate aggregate = _store.CreateAggregate<TAggregate>();

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
                throw new Exception(String.Format("There is no aggregate with id {0}", _aggregateId));

            aggregate.Id = lastTransition.StreamId;
            aggregate.InitialVersion = lastTransition.StreamSequence;
            aggregate.State = initialState;


            return aggregate;
        }

        public TAggregate LoadOrCreateAggregate<TAggregate>()
            where TAggregate : IAggregate<TAggregateId>
        {
            return (TAggregate)(Object)null;
        }

        public TAggregate CreateAggregate<TAggregate>()
            where TAggregate : IAggregate<TAggregateId>
        {
            // Here we can load state from snapshot store, but we are starting from initial state.
            var initialStateEnvelope = _store.CreateStateForAggregate(typeof(TAggregate));

            // Create aggregate, initialized with final state that we just "spooled"
            return _store.CreateAggregate<TAggregate>();
        }

        public void SaveChanges()
        {
            
        }

        public void Dispose()
        {
            
        }
    }
}