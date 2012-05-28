using System;

namespace Immutably.Aggregates
{
    public interface IAggregateSession : IDisposable
    {
        /// <summary>
        /// Load Aggregate of specified type
        /// </summary>
        IAggregate LoadAggregate(Type aggregateType, String aggregateId);

        /// <summary>
        /// Create Aggregate of specified type
        /// </summary>
        IAggregate CreateAggregate(Type aggregateType, String aggregateId);

        /// <summary>
        /// If aggregate exists - it will be loaded.
        /// If aggregate doesn't exist - it will be created.
        /// </summary>
        IAggregate LoadOrCreateAggregate(Type aggregateType, String aggregateId);

        /// <summary>
        /// Commit your changes (if any) to aggregate Store.
        /// </summary>
        void SaveChanges();

        TAggregate CreateAggregate<TAggregate>(String aggregateId)
            where TAggregate : IAggregate;

        TAggregate LoadAggregate<TAggregate>(String aggregateId)
            where TAggregate : IAggregate;

        TAggregate LoadOrCreateAggregate<TAggregate>(String aggregateId)
            where TAggregate : IAggregate;
    }
}