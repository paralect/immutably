using System;

namespace Immutably.Aggregates
{
    public interface IAggregateSession<TId> : IDisposable
    {
        TAggregate LoadAggregate<TAggregate>()
            where TAggregate : IAggregate<TId>;

        TAggregate CreateAggregate<TAggregate>()
            where TAggregate : IAggregate<TId>;

        TAggregate LoadOrCreateAggregate<TAggregate>()
            where TAggregate : IAggregate<TId>;


        void SaveChanges();
    }
}