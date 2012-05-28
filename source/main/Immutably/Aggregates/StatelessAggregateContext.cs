using System;
using Immutably.Data;

namespace Immutably.Aggregates
{
    /// <summary>
    /// Represents context for stateless aggregates.
    /// </summary>
    public class StatelessAggregateContext : AggregateContextBase, IStatelessAggregateContext
    {
        public StatelessAggregateContext(String aggregateId, Int32 version, IDataFactory dataFactory) 
            : base(aggregateId, version, dataFactory)
        {
        }        
    }
}