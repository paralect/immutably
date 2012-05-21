using System;

namespace Immutably.Aggregates
{
    public interface IAggregateSession : IDisposable
    {
        IAggregate LoadAggregate(Type aggregateType);
        IAggregate CreateAggregate(Type aggregateType);
        IAggregate LoadOrCreateAggregate(Type aggregateType);
        void SaveChanges();
    }

    public interface IAggregateSession<TId> : IAggregateSession
    {
        TAggregate LoadAggregate<TAggregate>()
            where TAggregate : IAggregate<TId>;

        TAggregate CreateAggregate<TAggregate>()
            where TAggregate : IAggregate<TId>;

        TAggregate LoadOrCreateAggregate<TAggregate>()
            where TAggregate : IAggregate<TId>;
    }
}