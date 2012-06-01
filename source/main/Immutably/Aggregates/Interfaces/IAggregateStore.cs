using System;
using Immutably.Data;
using Immutably.Transitions;

namespace Immutably.Aggregates
{
    public interface IAggregateStore
    {
        ITransitionStore TransitionStore { get; }

        IAggregateSession OpenSession();
        
        Type GetAggregateStateType(Type aggregateType);
        Object CreateState(Type stateType);
    }
}