using System;
using Escolar.Aggregates;
using Escolar.States;
using Escolar.Transitions;
using Paralect.Machine.Processes;

namespace Escolar.Aggregates
{
    public interface IAggregateStore
    {
        ITransitionStore TransitionStore { get; }

        IAggregateSession<TAggregateId> OpenSession<TAggregateId>(TAggregateId aggregateId);
        IAggregateSession<TAggregateId> OpenStatelessSession<TAggregateId>(TAggregateId aggregateId);
        
        Type GetAggregateStateType(Type aggregateType);
        IStateEnvelope<TAggregateId> CreateStateEnvelope<TAggregateId>(Type aggregateType, TAggregateId aggregateId);
        IAggregate<TAggregateId> CreateAggregate<TAggregateId>(Type aggregateType, IStateEnvelope<TAggregateId> state);
    }
}