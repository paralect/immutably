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
                aggregate = session.CreateAggregate<MyStatefullAggregate>(id);
                aggregate.Create("Bill", 45);
                aggregate.ChangeName("John");
                
                session.SaveChanges();
            }

            using (var session = aggregateStore.OpenSession())
            {
                aggregate = session.LoadAggregate<MyStatefullAggregate>(id);
                aggregate.ChangeName("Mishka");

                session.SaveChanges();
            }

            aggregate = null;

            using (var session = aggregateStore.OpenSession())
            {
                aggregate = session.LoadAggregate<MyStatefullAggregate>(id);
            }

        };

        It state_should_be_correct = () =>
            aggregate.State.Name.ShouldEqual("Mishka");

        static String id = "aggregate/01";
        static MyStatefullAggregate aggregate;
    }
}