using System;
using Immutably.Messages;
using Immutably.Transitions;

namespace Immutably.Aggregates
{
    public interface IAggregateStore
    {
        ITransitionStore TransitionStore { get; }

        IAggregateSession<TAggregateId> OpenSession<TAggregateId>(TAggregateId aggregateId);
        IAggregateSession<TAggregateId> OpenStatelessSession<TAggregateId>(TAggregateId aggregateId);
        
        Type GetAggregateStateType(Type aggregateType);
        IState CreateStateForAggregate(Type aggregateType);
        TAggregate CreateAggregate<TAggregate>();
    }
}