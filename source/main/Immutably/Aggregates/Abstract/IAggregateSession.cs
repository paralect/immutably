using System;

namespace Immutably.Aggregates
{
    public interface IAggregateSession : IDisposable
    {
        /// <summary>
        /// Load Aggregate of specified type
        /// </summary>
        IAggregate LoadAggregate(Type aggregateType);

        /// <summary>
        /// Create Aggregate of specified type
        /// </summary>
        IAggregate CreateAggregate(Type aggregateType);

        /// <summary>
        /// If aggregate exists - it will be loaded.
        /// If aggregate doesn't exist - it will be created.
        /// </summary>
        IAggregate LoadOrCreateAggregate(Type aggregateType);

        /// <summary>
        /// Commit your changes (if any) to aggregate Store.
        /// </summary>
        void SaveChanges();
    }

    public interface IAggregateSession<TId> : IAggregateSession
    {
        /// <summary>
        /// Load Aggregate of specified type
        /// </summary>
        TAggregate LoadAggregate<TAggregate>()
            where TAggregate : IAggregate<TId>;

        /// <summary>
        /// Create Aggregate of specified type
        /// </summary>
        TAggregate CreateAggregate<TAggregate>()
            where TAggregate : IAggregate<TId>;

        /// <summary>
        /// If aggregate exists - it will be loaded.
        /// If aggregate doesn't exist - it will be created.
        /// </summary>
        TAggregate LoadOrCreateAggregate<TAggregate>()
            where TAggregate : IAggregate<TId>;
    }
}