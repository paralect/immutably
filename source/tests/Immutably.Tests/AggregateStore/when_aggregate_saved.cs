using System;
using System.Collections.Generic;
using Immutably.Aggregates;
using Machine.Specifications;

namespace Immutably.Tests.AggregateStore
{
    public class when_aggregate_saved : InMemoryAggregateStore
    {
        static Dictionary<String, String> ddd = new Dictionary<string, string>();

        Because of = () =>
        {
            using (var session = aggregateStore.OpenSession())
            {
                var agg = session.CreateAggregate<MyAggregate>(Guid.Empty.ToString());
                agg.ChangeName(default(Guid), "dfdf");
                session.SaveChanges();
            }
        };

        It should_be_aga = () =>
            true.ShouldBeTrue();
    }
}