using System;
using Immutably.Data;

namespace Immutably.Aggregates
{
    public interface IAggregateStore
    {
        IAggregateSession OpenSession();

        Object CreateState(Type stateType);
    }
}