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
        private readonly ITransitionStore _store;

        public AggregateSession(ITransitionStore store)
        {
            _store = store;
        }

        public TAggregate Load<TAggregate>(Guid id) 
            where TAggregate : IAggregate
        {
            var genericArgs = typeof (TAggregate).GetGenericArguments();
            var stateType = genericArgs[0];
            var state = (IState) Activator.CreateInstance(stateType);

            var transitions = _store.GetById(id);
            var stateMetadata = new StateMetadata(transitions.Last().EntityId, transitions.Last().Version);s
            var stateEnvelope = new StateEnvelope(state, stateMetadata);

            var stateSpooler = new StateSpooler(stateEnvelope);
            var result = stateSpooler.Spool(transitions.SelectMany(t => t.EventEnvelopes));

            var aggregate = Activator.CreateInstance<TAggregate>();
            aggregate.Initialize(result);

            return aggregate;
        }

        public void SaveChanges()
        {
            
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}