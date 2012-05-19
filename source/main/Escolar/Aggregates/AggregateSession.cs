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
        {
            // Here we can load state from snapshot store, but we are starting from initial state.
            var initialStateEnvelope = _store.CreateStateEnvelope(typeof(TAggregate), _aggregateId);

            // Spooler will "spool" our events based on initial state of spooler 
            // (that in our case is just newly created state)
            var spooler = new StateSpooler<TAggregateId>(initialStateEnvelope);

            // Reading transitions and "spooling" of events to receive final state
            using (var reader = _store.TransitionStore.CreateStreamReader(_aggregateId))
            {
                spooler.Spool(reader.Read().SelectMany(t => t.EventEnvelopes));
            }

            // Create aggregate, initialized with final state that we just "spooled"
            return (TAggregate) _store.CreateAggregate(typeof(TAggregate), spooler.StateEnvelope);
        }

        public TAggregate LoadOrCreateAggregate<TAggregate>()
        {
            return (TAggregate)(Object)null;
        }

        public TAggregate CreateAggregate<TAggregate>()
        {
            // Here we can load state from snapshot store, but we are starting from initial state.
            var initialStateEnvelope = _store.CreateStateEnvelope(typeof(TAggregate), _aggregateId);

            // Create aggregate, initialized with final state that we just "spooled"
            return (TAggregate)_store.CreateAggregate(typeof(TAggregate), initialStateEnvelope);
        }

        public void SaveChanges()
        {
            
        }

        public void Dispose()
        {
            
        }
    }
}