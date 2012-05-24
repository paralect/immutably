using System;
using Immutably.Data;
using Immutably.Messages;
using Immutably.Transitions;

namespace Immutably.Aggregates
{
    public interface IAggregateStore
    {
        ITransitionStore TransitionStore { get; }

        IAggregateSession OpenSession(String aggregateId);
        IAggregateSession OpenStatelessSession(String aggregateId);
        
        Type GetAggregateStateType(Type aggregateType);
        IState CreateState(Type stateType);
        IAggregate CreateAggregate(Type aggregateType);
        IAggregateContext CreateAggregateContext(String aggregateId, Int32 version, Object state, IDataFactory dataFactory);
    }
}