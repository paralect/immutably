using System;
using Immutably.Data;
using Immutably.Serialization.Abstract;
using Lokad.Cqrs.TapeStorage;

namespace Immutably.Aggregates
{
    public class AggregateStore : IAggregateStore
    {
        private readonly ITransitionStore _transitionStore;
        private readonly IDataFactory _dataFactory;
        private readonly ISerializer _serializer;
        private readonly AggregateRegistry _aggregateRegistry;

        public AggregateStore(ITransitionStore transitionStore, IDataFactory dataFactory, ISerializer serializer)
        {
            _transitionStore = transitionStore;
            _dataFactory = dataFactory;
            _serializer = serializer;
            _aggregateRegistry = new AggregateRegistry();
        }

        public IAggregateSession OpenSession()
        {
            return new AggregateSession(this, _transitionStore, _dataFactory, _serializer, _aggregateRegistry);
        }

        public Object CreateState(Type stateType)
        {
            var state = Activator.CreateInstance(stateType);
            return state;
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