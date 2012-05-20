using System;
using Escolar.Tests.Aggs;
using Machine.Specifications;

namespace Escolar.Tests.Aggregates
{
    public class when_aggregate_created : AggregateContext
    {
        Because of = () =>
            aggregate = new MyAggregate();

        It state_should_be_not_null = () =>
            aggregate.State.ShouldNotBeNull();

        It current_version_should_be_zero = () =>
            aggregate.CurrentVersion.ShouldEqual(0); 

        It initial_version_should_be_zero = () =>
            aggregate.InitialVersion.ShouldEqual(0);

        It data_factory_should_be_null = () =>
            aggregate.DataFactory.ShouldBeNull();

        It should_be_not_changed = () =>
            aggregate.Changed.ShouldBeFalse();

        It changes_should_be_initialized_but_empty = () =>
            aggregate.Changes.Count.ShouldEqual(0);

        private static MyAggregate aggregate;
    }
}