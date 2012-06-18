using System;
using Immutably.Data;
using Immutably.Transitions;

namespace Immutably.Aggregates
{
    public class AggregateStore : IAggregateStore
    {
        private readonly IEscolarFactory _factory;
        private readonly IDataFactory _dataFactory;
        private readonly ITransitionStore _transitionStore;
        private readonly AggregateRegistry _aggregateRegistry;

        public ITransitionStore TransitionStore
        {
            get { return _transitionStore; }
        }

        public AggregateStore(IEscolarFactory factory, IDataFactory dataFactory, ITransitionStore transitionStore)
        {
            _factory = factory;
            _dataFactory = dataFactory;
            _transitionStore = transitionStore;
            _aggregateRegistry = new AggregateRegistry();
        }

        public IAggregateSession OpenSession()
        {
            return new AggregateSession(this, _dataFactory, _aggregateRegistry);
        }

        public Type GetAggregateStateType(Type aggregateType)
        {
            if (aggregateType.BaseType == null 
                || aggregateType.BaseType.IsGenericType == false
                || aggregateType.BaseType.GetGenericTypeDefinition() != typeof(StatefullAggregate<>))
                throw new Exception(String.Format("We cannot find state type for [{0}] aggregate", aggregateType.FullName));

            var genericArgs = aggregateType.BaseType.GetGenericArguments();
            var stateType = genericArgs[0];
            return stateType;
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