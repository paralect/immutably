using System;
using Escolar.Aggregates;
using Escolar.Messages;
using Escolar.States;
using Escolar.Transitions;
using Paralect.Machine.Processes;

namespace Escolar.Aggregates
{
    public class AggregateStore<TAggregateId> : IAggregateStore<TAggregateId>
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

        public IAggregateSession<TAggregateId, TAggregate> OpenSession<TAggregate>(TAggregateId aggregateId)
            where TAggregate : IAggregate<TAggregateId>
        {
            return new AggregateSession<TAggregate, TAggregateId>(this, aggregateId);
        }

        public IAggregateSession<TAggregateId, TAggregate> OpenStatelessSession<TAggregate>(TAggregateId aggregateId) 
            where TAggregate : IAggregate<TAggregateId>
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

        public IStateEnvelope<TAggregateId> CreateStateEnvelope(Type aggregateType, TAggregateId aggregateId)
        {
            var stateType = GetAggregateStateType(aggregateType);

            var state = (IState) Activator.CreateInstance(stateType);
            var stateMetadata = new StateMetadata<TAggregateId>(aggregateId, 0);
            var stateEnvelope = new StateEnvelope<TAggregateId>(state, stateMetadata);

            return stateEnvelope;
        }

        public IAggregate<TAggregateId> CreateAggregate(Type aggregateType, IStateEnvelope<TAggregateId> state)
        {
            var context = new AggregateContext<TAggregateId>(_factory, state);

            var aggregate = (IAggregate<TAggregateId>) Activator.CreateInstance(aggregateType);
            aggregate.Initialize(context);

            return aggregate;
        }
    }
}