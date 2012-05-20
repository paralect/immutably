using Machine.Specifications;

namespace Immutably.Tests.Aggregates
{
    public class when_one_event_applied : AggregateContext
    {
        Because of = () =>
        {
            name = "Some name";
            aggregate = new MyAggregate();
            aggregate.Apply(new MyAggregateCreatedEvent
            {
                Name = name
            });
        };
        

        Behaves_like<AggregateWithOneEventBehaviors> behavior;

        protected static MyAggregate aggregate;
        protected static string name;
    }
}