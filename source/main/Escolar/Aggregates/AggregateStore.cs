using System;
using Escolar.Aggregates;
using Escolar.Messages;
using Escolar.States;
using Escolar.Transitions;
using Paralect.Machine.Processes;

namespace Escolar.Aggregates
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

        public IAggregateSession<TAggregate> OpenSession<TAggregate>(Guid aggregateId)
            where TAggregate : IAggregate
        {
            return new AggregateSession<TAggregate>(this, aggregateId);
        }

        public IAggregateSession<TAggregate> OpenStatelessSession<TAggregate>(Guid aggregateId) 
            where TAggregate : IAggregate
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

        public IStateEnvelope CreateStateEnvelope(Type aggregateType, Guid aggregateId)
        {
            var stateType = GetAggregateStateType(aggregateType);

            var state = (IState) Activator.CreateInstance(stateType);
            var stateMetadata = new StateMetadata(aggregateId, 0);
            var stateEnvelope = new StateEnvelope(state, stateMetadata);

            return stateEnvelope;
        }

        public IAggregate CreateAggregate(Type aggregateType, IStateEnvelope state)
        {
            var context = new AggregateContext(_factory, state);

            var aggregate = (IAggregate) Activator.CreateInstance(aggregateType);
            aggregate.Initialize(context);

            return aggregate;
        }
    }
}