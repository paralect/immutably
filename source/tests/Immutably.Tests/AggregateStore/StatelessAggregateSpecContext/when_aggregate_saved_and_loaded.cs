using System;
using Machine.Specifications;

namespace Immutably.Tests.AggregateStore.StatelessAggregateSpecContext
{
    public class when_aggregate_saved_and_loaded : StatelessAggregateSpecContext
    {
        Because of = () =>
        {
            using (var session = aggregateStore.OpenSession())
            {
                aggregate = session.CreateAggregate<MyStatelessAggregate>(id);
                aggregate.Create("John", 67);
                aggregate.ChangeName("hello");
                session.SaveChanges();
            }

            using (var session = aggregateStore.OpenSession())
            {
                aggregate = session.LoadAggregate<MyStatelessAggregate>(id);
                aggregate.ChangeName("hello");
                aggregate.ChangeName("hello");
                session.SaveChanges();                
            }

        };

        It should_be_of_version_2 = () =>
            aggregate.CurrentVersion.ShouldEqual(2);

        static String id = "aggregate/01";
        static MyStatelessAggregate aggregate;
    }
}