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
        private readonly IFactory _factory;
        private readonly ITransitionStore _store;
        private readonly IAggregateHelper _aggregateHelper;

        public AggregateSession(IFactory factory, ITransitionStore store)
        {
            _factory = factory;
            _store = store;
            _aggregateHelper = _factory.CreateAggregateHelper();
        }

        public TAggregate Load<TAggregate>(Guid id) 
            where TAggregate : IAggregate
        {
            /*
            var transitions = _store.GetById(id);

            var stateEnvelope = _aggregateHelper.CreateInitialStateEnvelope(typeof (TAggregate), id);
            var stateSpooler = _factory.CreateStateSpooler(stateEnvelope);
            var result = stateSpooler.Spool(transitions.SelectMany(t => t.EventEnvelopes));

            var aggregate = (TAggregate) _factory.CreateAggregate(typeof(TAggregate));
            aggregate.Initialize(result);

            return aggregate;
             * */

            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            
        }

        public void Dispose()
        {
            
        }
    }
}