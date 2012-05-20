using Machine.Specifications;

namespace Immutably.Tests.Aggregates
{
    public class when_one_event_applied_via_generic : AggregateContext
    {
        Because of = () =>
        {
            name = "Some name";
            aggregate = new MyAggregate();
            aggregate.Apply<MyAggregateCreatedEvent>(e =>
            {
                e.Name = name;
            });
        };

        Behaves_like<AggregateWithOneEventBehaviors> behavior;

        protected static MyAggregate aggregate;
        protected static string name;
    }
}