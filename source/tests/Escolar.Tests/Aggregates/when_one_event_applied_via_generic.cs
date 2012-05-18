using Machine.Specifications;

namespace Escolar.Tests.Aggregates
{
    public class when_one_event_applied_via_generic : AggregateContext
    {
        Because of = () =>
        {
            aggregate = new MyAggregate();
            aggregate.Apply<MyAggregateCreatedEvent>(e => {});
        };

        Behaves_like<AggregateWithOneEventBehaviors> behavior;

        protected static MyAggregate aggregate;
    }
}