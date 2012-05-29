using System;
using Immutably.Data;

namespace Immutably.Aggregates
{
    /// <summary>
    /// Represents the base class for aggregate repositories
    /// </summary>
    public abstract class AggregateRepositoryBase
    {
        protected readonly AggregateStore _store;
        protected readonly IDataFactory _dataFactory;
        protected readonly AggregateDefinition _definition;

        /// <summary>
        /// Constructs AggregateRepositoryBase
        /// </summary>
        protected AggregateRepositoryBase(AggregateStore store, IDataFactory dataFactory, AggregateDefinition definition)
        {
            _store = store;
            _dataFactory = dataFactory;
            _definition = definition;
        }

        /// <summary>
        /// Creates and returns aggregate of specified type and id
        /// </summary>
        public abstract IAggregate CreateAggregate(Type aggregateType, String aggregateId);

        /// <summary>
        /// Returns aggregate or null, if it wasn't find
        /// </summary>
        public abstract IAggregate LoadAggregate(Type aggregateType, String aggregateId);
    }
}