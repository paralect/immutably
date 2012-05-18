using Machine.Specifications;

namespace Escolar.Tests.Aggregates
{
    public class when_one_event_applied : AggregateContext
    {
        Because of = () =>
        {
            aggregate = new MyAggregate();
            aggregate.Apply(new MyAggregateCreatedEvent());
        };

        Behaves_like<AggregateWithOneEventBehaviors> behavior;

        protected static MyAggregate aggregate;
    }
}