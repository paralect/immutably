using System;

namespace Immutably.Aggregates
{
    public interface IAggregateSession : IDisposable
    {
        /// <summary>
        /// Load Aggregate of specified type
        /// </summary>
        IAggregate Load(Type aggregateType, String aggregateId);

        /// <summary>
        /// Create Aggregate of specified type
        /// </summary>
        IAggregate Create(Type aggregateType, String aggregateId);

        /// <summary>
        /// If aggregate exists - it will be loaded.
        /// If aggregate doesn't exist - it will be created.
        /// </summary>
        IAggregate LoadOrCreate(Type aggregateType, String aggregateId);

        /// <summary>
        /// Commit your changes (if any) to aggregate Store.
        /// </summary>
        void SaveChanges();

        TAggregate Create<TAggregate>(String aggregateId)
            where TAggregate : IAggregate;

        TAggregate Load<TAggregate>(String aggregateId)
            where TAggregate : IAggregate;

        TAggregate LoadOrCreate<TAggregate>(String aggregateId)
            where TAggregate : IAggregate;
    }
}