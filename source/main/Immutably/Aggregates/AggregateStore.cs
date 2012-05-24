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

        public IAggregateSession OpenSession(String aggregateId)
        {
            // We don't allow null (in case this is a reference type).
            // If this is a value type, this check will be ignored.
            if (aggregateId == null)
                throw new NullAggregateIdException();

            return new AggregateSession(this, aggregateId);
        }

        public IAggregateSession OpenStatelessSession(String aggregateId) 
        {
            throw new NotImplementedException();
        }

        public Type GetAggregateStateType(Type aggregateType)
        {
            if (aggregateType.BaseType == null 
                || aggregateType.BaseType.IsGenericType == false
                || aggregateType.BaseType.GetGenericTypeDefinition() != typeof(Aggregate<>))
                throw new Exception(String.Format("We cannot find state type for [{0}] aggregate", aggregateType.FullName));

            var genericArgs = aggregateType.BaseType.GetGenericArguments();
            var stateType = genericArgs[0];
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

        public IStatefullAggregate CreateAggregate(Type aggregateType)
        {
            return (IStatefullAggregate) Activator.CreateInstance(aggregateType);
        }

        public IAggregateContext CreateAggregateContext(String aggregateId, Int32 version, Object state, IDataFactory dataFactory)
        {
            return new StatefullAggregateContext(state, aggregateId, version, dataFactory);
        }        
        
        public IAggregateSession CreateAggregateSession(Type idType, Object aggregateId)
        {
            var type = typeof (AggregateSession);
            var sessionType = type.MakeGenericType(idType);
            return (IAggregateSession) Activator.CreateInstance(sessionType, new[] { aggregateId });
        }
    }
}