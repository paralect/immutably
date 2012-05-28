using System;

namespace Immutably.Aggregates
{
    public interface IAggregateSession : IDisposable
    {
        /// <summary>
        /// Load Aggregate of specified type
        /// </summary>
        IStatefullAggregate LoadAggregate(Type aggregateType);

        /// <summary>
        /// Create Aggregate of specified type
        /// </summary>
        IStatefullAggregate CreateAggregate(Type aggregateType);

        /// <summary>
        /// If aggregate exists - it will be loaded.
        /// If aggregate doesn't exist - it will be created.
        /// </summary>
        IStatefullAggregate LoadOrCreateAggregate(Type aggregateType);

        /// <summary>
        /// Commit your changes (if any) to aggregate Store.
        /// </summary>
        void SaveChanges();

        TAggregate CreateAggregate<TAggregate>()
            where TAggregate : IStatefullAggregate;

        TAggregate LoadAggregate<TAggregate>()
            where TAggregate : IStatefullAggregate;

        TAggregate LoadOrCreateAggregate<TAggregate>()
            where TAggregate : IStatefullAggregate;
    }
}