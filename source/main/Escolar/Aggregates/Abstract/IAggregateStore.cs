using System;
using Escolar.Aggregates;
using Escolar.States;
using Escolar.Transitions;
using Paralect.Machine.Processes;

namespace Escolar.Aggregates
{
    public interface IAggregateStore<TAggregateId>
    {
        ITransitionStore<TAggregateId> TransitionStore { get; }

        IAggregateSession<TAggregateId, TAggregate> OpenSession<TAggregate>(TAggregateId aggregateId)
            where TAggregate : IAggregate<TAggregateId>;

        IAggregateSession<TAggregateId, TAggregate> OpenStatelessSession<TAggregate>(TAggregateId aggregateId)
            where TAggregate : IAggregate<TAggregateId>;
        
        Type GetAggregateStateType(Type aggregateType);
        IStateEnvelope<TAggregateId> CreateStateEnvelope(Type aggregateType, TAggregateId aggregateId);
        IAggregate<TAggregateId> CreateAggregate(Type aggregateType, IStateEnvelope<TAggregateId> state);
    }
}