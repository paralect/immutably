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
        private readonly ITransitionStore _transitionStore;

        public ITransitionStore TransitionStore
        {
            get { return _transitionStore; }
        }

        public AggregateStore(ITransitionStore transitionStore)
        {
            _transitionStore = transitionStore;
        }

        public IAggregateSession OpenSession()
        {
            return new AggregateSession(this);
        }

        public Type GetAggregateStateType(Type aggregateType)
        {
            var genericArgs = aggregateType.GetGenericArguments();
            var stateType = genericArgs[0];
            return stateType;
        }

        public IStateEnvelope CreateInitialStateEnvelope(Type aggregateType, Guid aggregateId)
        {
            var stateType = GetAggregateStateType(aggregateType);

            var state = (IState) Activator.CreateInstance(stateType);
            var stateMetadata = new StateMetadata(aggregateId, 0);
            var stateEnvelope = new StateEnvelope(state, stateMetadata);

            return stateEnvelope;
        }

        public IAggregate CreateAggregate(Type aggregateType, IStateEnvelope state)
        {
            var aggregate = (IAggregate) Activator.CreateInstance(aggregateType);
            aggregate.Initialize(state);
            return aggregate;
        }
    }
}