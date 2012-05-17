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

        IAggregateSession<TAggregate> OpenSession<TAggregate>(Guid aggregateId)
            where TAggregate : IAggregate;

        IAggregateSession<TAggregate> OpenStatelessSession<TAggregate>(Guid aggregateId)
            where TAggregate : IAggregate;

        
        
        Type GetAggregateStateType(Type aggregateType);
        IStateEnvelope CreateStateEnvelope(Type aggregateType, Guid aggregateId);
        IAggregate CreateAggregate(Type aggregateType, IStateEnvelope state);
    }
}