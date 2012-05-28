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

        TAggregate CreateAggregate<TAggregate>()
            where TAggregate : IAggregate;

        TAggregate LoadAggregate<TAggregate>()
            where TAggregate : IAggregate;

        TAggregate LoadOrCreateAggregate<TAggregate>()
            where TAggregate : IAggregate;
    }
}