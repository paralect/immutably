using Machine.Specifications;

namespace Immutably.Tests.Specs.aggregates.simple_aggregates
{
    public class when_aggregate_created
    {
        Because of = () =>
            aggregate = new SimpleStatefullAggregate();

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

        private static SimpleStatefullAggregate aggregate;
    }
}