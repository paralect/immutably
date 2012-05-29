using System;
using Immutably.Aggregates;
using Immutably.Tests.Environments.SimpleStatelessAggregate;
using Machine.Specifications;

namespace Immutably.Tests.Specs.aggregate_store.empty_aggregate_store.simplest_possible_aggregates
{
    public class when_loading_stateless_aggregate : _empty_aggregate_store_context
    {
        Because of = () =>
        {
            using (var session = aggregateStore.OpenSession())
            {
                exception = Catch.Exception(() =>
                {
                    session.LoadAggregate<MyStatelessAggregate>(Guid.Empty.ToString());
                });
            }

        };

        It should_throw_exception = () =>
            exception.ShouldBeOfType<AggregateDoesntExistException>();

        static Exception exception;
    }
}