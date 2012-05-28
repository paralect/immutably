using System;
using Immutably.Aggregates;
using Machine.Specifications;

namespace Immutably.Tests.Aggregates
{
    public class when_aggregate_context_was_initialized_second_time : AggregateMachineContext
    {
        Establish context = () =>
        {
            aggregate = new MyAggregate();
            aggregate.EstablishContext(new StatefullAggregateContext(new MyState(), "1", 0, null));
        };

        Because of = () =>
            exception = Catch.Exception(() => aggregate.EstablishContext(
                new StatefullAggregateContext(new MyState(), Guid.NewGuid().ToString(), 0, null)));

        It should_throw_exception = () =>
            exception.ShouldBeOfType<AggregateContextModificationForbiddenException>();

        static MyAggregate aggregate;
        static Exception exception;
    }
}