using System;
using Immutably.Aggregates;
using Machine.Specifications;

namespace Immutably.Tests.Specs.aggregates.simple_aggregates
{
    public class when_aggregate_context_was_initialized_second_time
    {
        Establish context = () =>
        {
            aggregate = new SimpleStatefullAggregate();
            aggregate.EstablishContext(new StatefullAggregateContext(new SimpleState(), "1", 0, null));
        };

        Because of = () =>
            exception = Catch.Exception(() => aggregate.EstablishContext(
                new StatefullAggregateContext(new SimpleState(), Guid.NewGuid().ToString(), 0, null)));

        It should_throw_exception = () =>
            exception.ShouldBeOfType<AggregateContextModificationForbiddenException>();

        static SimpleStatefullAggregate aggregate;
        static Exception exception;
    }
}