using System;
using Immutably.Aggregates;
using Machine.Specifications;

namespace Immutably.Tests.Specs.aggregates.simple_aggregates
{
    public class when_aggregate_context_was_initialized_via_builder_second_time
    {
        Establish context = () =>
        {
            aggregate = new SimpleStatefullAggregate();
            aggregate.EstablishContext(context => context
                .SetId(Guid.NewGuid().ToString())
                .SetVersion(34)
            );
        };

        Because of = () =>
            exception = Catch.Exception(() => 
                aggregate.EstablishContext(context => context
                    .SetId(Guid.NewGuid().ToString())
                    .SetVersion(34)
            ));

        It should_throw_exception = () =>
            exception.ShouldBeOfType<AggregateContextModificationForbiddenException>();

        static SimpleStatefullAggregate aggregate;
        static Exception exception;
    }
}