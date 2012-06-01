﻿using System;
using Machine.Specifications;

namespace Immutably.Tests.Specs.aggregate_store.empty_aggregate_store.simple_aggregates
{
    public class when_two_changes_was_applied_to_statefull_aggregate : _empty_aggregate_store_context
    {
        Because of = () =>
        {
            using (var session = aggregateStore.OpenSession())
            {
                var aggregate = session.Create<SimpleStatefullAggregate>(id);
                aggregate.Create("Bill", 45);
                aggregate.ChangeName("Tom");
                session.SaveChanges();
            }

            using (var session = aggregateStore.OpenSession())
            {
                aggregate = session.Load<SimpleStatefullAggregate>(id);
            }
        };

        It should_have_correct_state = () =>
        {
            aggregate.State.Name.ShouldEqual("Tom");
            aggregate.State.Year.ShouldEqual(45);
        };

        It should_have_correct_version = () =>
            aggregate.CurrentVersion.ShouldEqual(1);

        static String id = "aggregate/01";
        static SimpleStatefullAggregate aggregate;         
    }
}