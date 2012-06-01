using System;
using Machine.Specifications;

namespace Immutably.Tests.Specs.aggregate_store.empty_aggregate_store.simple_aggregates
{
    public class when_stateless_aggregate_created_and_saved : _empty_aggregate_store_context
    {
        Because of = () =>
        {
            using (var session = aggregateStore.OpenSession())
            {
                var aggregate = session.Create<SimpleStatelessAggregate>(id);
                aggregate.Create("Bill", 45);
                session.SaveChanges();
            }

            using (var session = aggregateStore.OpenSession())
            {
                aggregate = session.Load<SimpleStatelessAggregate>(id);
            }
        };

        It should_have_correct_version = () =>
            aggregate.CurrentVersion.ShouldEqual(1);

        static String id = "aggregate/01";
        static SimpleStatelessAggregate aggregate;
    }
}