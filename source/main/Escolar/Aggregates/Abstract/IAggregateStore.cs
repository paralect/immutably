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
        IAggregateSession OpenSession();
        Type GetAggregateStateType(Type aggregateType);
        IStateEnvelope CreateInitialStateEnvelope(Type aggregateType, Guid aggregateId);
        IAggregate CreateAggregate(Type aggregateType, IStateEnvelope state);
    }
}