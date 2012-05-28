using System;
using Immutably.Data;

namespace Immutably.Aggregates
{
    public class StatelessAggregateContext : AggregateContextBase, IStatelessAggregateContext
    {
        public StatelessAggregateContext(String aggregateId, Int32 version, IDataFactory dataFactory) 
            : base(aggregateId, version, dataFactory)
        {
        }        
    }
}