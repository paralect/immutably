using System;
using Immutably.Transitions;
using Machine.Specifications;

namespace Immutably.Tests.AggregateStore
{
    public class when_loading_aggregate_from_not_available_stream : InMemoryAggregateStore
    {
        Because of = () =>
        {
            using (var session = AggregateStore.OpenSession(Guid.Empty.ToString()))
            {
                exception = Catch.Exception(() => session.LoadAggregate<MyAggregate>());
            }
        };

        It should_be_thrown_exception = () =>
            exception.ShouldBeOfType<TransitionStreamNotExistsException>();

        static Exception exception;
    }
}