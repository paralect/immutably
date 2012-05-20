using Machine.Specifications;

namespace Escolar.Tests.Aggregates
{
    [Behaviors]
    public class AggregateWithOneEventBehaviors
    {
        It state_should_be_not_null = () =>
            aggregate.State.ShouldNotBeNull();

        It state_should_has_correct_data = () =>
            aggregate.State.Name.ShouldEqual(name);

        It current_version_should_be_one = () =>
            aggregate.CurrentVersion.ShouldEqual(1);

        It initial_version_should_be_zero = () =>
            aggregate.InitialVersion.ShouldEqual(0);

        It data_factory_should_be_null = () =>
            aggregate.DataFactory.ShouldBeNull();

        It should_be_changed = () =>
            aggregate.Changed.ShouldBeTrue();

        It changes_should_contain_one_event = () =>
            aggregate.Changes.Count.ShouldEqual(1);

        protected static MyAggregate aggregate;
        protected static string name;
    }
}