using System;
using Immutably.Data;
using Immutably.Messages;
using Immutably.Transitions;

namespace Immutably.Aggregates
{
    public interface IAggregateStore
    {
        ITransitionStore TransitionStore { get; }

        IAggregateSession OpenSession();
        
        Type GetAggregateStateType(Type aggregateType);
        IState CreateState(Type stateType);
    }
}