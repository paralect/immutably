using System;
using Machine.Specifications;

namespace Escolar.Tests.Aggs
{
    public class when_aggregate_saved : InMemoryAggregateStore
    {
        Because of = () =>
        {
            using (var session = AggregateStore.OpenSession(Guid.Empty))
            {
                var agg = session.CreateAggregate<MyAggregate>();
                agg.ChangeName(default(Guid), "dfdf)");
                session.SaveChanges();
            }
        };        
    }
}