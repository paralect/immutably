using System;
using Immutably.Data;
using Immutably.Messages;
using Immutably.Transitions;

namespace Immutably.Aggregates
{
    public interface IAggregateStore
    {
        ITransitionStore TransitionStore { get; }

        IAggregateSession<TAggregateId> OpenSession<TAggregateId>(TAggregateId aggregateId);
        IAggregateSession OpenSession(Object aggregateId);

        IAggregateSession<TAggregateId> OpenStatelessSession<TAggregateId>(TAggregateId aggregateId);
        
        Type GetAggregateStateType(Type aggregateType);
        IState CreateState(Type stateType);
        TAggregate CreateAggregate<TAggregate>();
        IAggregate CreateAggregate(Type aggregateType);
        IAggregateContext CreateAggregateContext(Type idType, Type stateType, Object aggregateId, Int32 version, Object state, IDataFactory dataFactory);
    }
}