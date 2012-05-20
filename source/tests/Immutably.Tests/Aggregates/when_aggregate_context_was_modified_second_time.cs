﻿using System;
using Immutably.Aggregates;
using Machine.Specifications;

namespace Immutably.Tests.Aggregates
{
    public class when_aggregate_context_was_initialized_second_time : AggregateContext
    {
        Establish context = () =>
        {
            aggregate = new MyAggregate();
            aggregate.EstablishContext(new AggregateContext<Guid, MyState>(Guid.NewGuid()));
        };

        Because of = () =>
            exception = Catch.Exception(() => aggregate.EstablishContext(new AggregateContext<Guid, MyState>(Guid.NewGuid())));

        It should_throw_exception = () =>
            exception.ShouldBeOfType<AggregateContextModificationForbiddenException>();

        static MyAggregate aggregate;
        static Exception exception;
    }
}