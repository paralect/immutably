using System;
using System.Linq;
using Escolar.Messages;
using Escolar.States;
using Escolar.Transitions;
using Paralect.Machine.Processes;

namespace Escolar.Aggregates
{
    public class AggregateSession : IAggregateSession
    {
        private readonly IAggregateStore _store;

        public AggregateSession(IAggregateStore store)
        {
            _store = store;
        }

        public TAggregate Load<TAggregate>(Guid aggregateId) 
            where TAggregate : IAggregate
        {
            // Here we can load state from snapshot store, but we are starting from initial state.
            var initialStateEnvelope = _store.CreateInitialStateEnvelope(typeof(TAggregate), aggregateId);

            // Spooler will "spool" our events based on initial state of spooler 
            // (that in our case is just newly created state)
            var spooler = new StateSpooler(null, null);

            // Reading transitions and "spooling" of events to receive final state
            IStateEnvelope finalStateEnvelope = null;
            using (var reader = _store.TransitionStore.CreateStreamReader(aggregateId))
            {
                finalStateEnvelope = spooler.Spool(reader.Read().SelectMany(t => t.EventEnvelopes));
            }

            // Create aggregate, initialized with final state that we just "spooled"
            return (TAggregate) _store.CreateAggregate(typeof(TAggregate), finalStateEnvelope);
        }

        public TAggregate Load2<TAggregate>(Guid aggregateId)
            where TAggregate : IAggregate
        {
            // Here we can load state from snapshot store, but we are starting from initial state.
            var initialStateEnvelope = _store.CreateInitialStateEnvelope(typeof(TAggregate), aggregateId);
            var agg = (TAggregate) _store.CreateAggregate(typeof(TAggregate), initialStateEnvelope);

            using (var reader = _store.TransitionStore.CreateStreamReader(aggregateId))
            {
                agg.Replay(reader.Read().SelectMany(t => t.EventEnvelopes));
            }

            return agg;


            // Spooler will "spool" our events based on initial state of spooler 
            // (that in our case is just newly created state)
            var spooler = new StateSpooler(null, null);

            // Reading transitions and "spooling" of events to receive final state
            IStateEnvelope finalStateEnvelope = null;
            using (var reader = _store.TransitionStore.CreateStreamReader(aggregateId))
            {
                finalStateEnvelope = spooler.Spool(reader.Read().SelectMany(t => t.EventEnvelopes));
            }

            // Create aggregate, initialized with final state that we just "spooled"
            return (TAggregate)_store.CreateAggregate(typeof(TAggregate), finalStateEnvelope);
        }

        public void SaveChanges()
        {
            
        }

        public void Dispose()
        {
            
        }
    }
}