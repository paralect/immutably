using System;
using Immutably.Data;
using Immutably.Messages;
using Immutably.Transitions;

namespace Immutably.Aggregates
{
    public class AggregateStore : IAggregateStore
    {
        private readonly IEscolarFactory _factory;
        private readonly ITransitionStore _transitionStore;

        public ITransitionStore TransitionStore
        {
            get { return _transitionStore; }
        }

        public AggregateStore(IEscolarFactory factory, ITransitionStore transitionStore)
        {
            _factory = factory;
            _transitionStore = transitionStore;
        }

        public IAggregateSession<TAggregateId> OpenSession<TAggregateId>(TAggregateId aggregateId)
        {
            return new AggregateSession<TAggregateId>(this, aggregateId);
        }

        public IAggregateSession<TAggregateId> OpenStatelessSession<TAggregateId>(TAggregateId aggregateId) 
        {
            throw new NotImplementedException();
        }

        public Type GetAggregateStateType(Type aggregateType)
        {
            if (aggregateType.BaseType == null 
                || aggregateType.BaseType.IsGenericType == false
                || aggregateType.BaseType.GetGenericTypeDefinition() != typeof(Aggregate<,>))
                throw new Exception(String.Format("We cannot find state type for [{0}] aggregate", aggregateType.FullName));

            var genericArgs = aggregateType.BaseType.GetGenericArguments();
            var stateType = genericArgs[1];
            return stateType;
        }

        public IState CreateState(Type stateType)
        {
            var state = (IState) Activator.CreateInstance(stateType);
            return state;
        }

        public TAggregate CreateAggregate<TAggregate>()
        {
            return Activator.CreateInstance<TAggregate>();
        }

        public IAggregateContext CreateAggregateContext(Type idType, Type stateType, Object aggregateId, Int32 version, Object state, IDataFactory dataFactory)
        {
            var type = typeof (AggregateContext<,>);
            var contextType = type.MakeGenericType(idType, stateType);

            return (IAggregateContext) Activator.CreateInstance(contextType, new[] { aggregateId, version, state, dataFactory });
        }
    }
}