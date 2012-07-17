using System;
using Immutably.Data;

namespace Immutably.Aggregates
{
    public class AggregateStore : IAggregateStore
    {
        private readonly IEscolarFactory _factory;
        private readonly IDataFactory _dataFactory;
        private readonly AggregateRegistry _aggregateRegistry;

        public AggregateStore(IEscolarFactory factory, IDataFactory dataFactory)
        {
            _factory = factory;
            _dataFactory = dataFactory;
            _aggregateRegistry = new AggregateRegistry();
        }

        public IAggregateSession OpenSession()
        {
            return new AggregateSession(this, _dataFactory, _aggregateRegistry);
        }

        public Object CreateState(Type stateType)
        {
            var state = Activator.CreateInstance(stateType);
            return state;
        }

        public TAggregate CreateAggregate<TAggregate>()
        {
            return Activator.CreateInstance<TAggregate>();
        }

        public IStatefullAggregate CreateStatefullAggregate(Type aggregateType)
        {
            return (IStatefullAggregate) Activator.CreateInstance(aggregateType);
        }

        public IStatelessAggregate CreateStatelessAggregate(Type aggregateType)
        {
            return (IStatelessAggregate) Activator.CreateInstance(aggregateType);
        }
    }
}