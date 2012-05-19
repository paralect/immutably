using System;
using Escolar.Transitions;
using Machine.Specifications;

namespace Escolar.Tests.Aggs
{
    public class when_loading_aggregate_from_not_available_stream : InMemoryAggregateStore
    {
        Because of = () =>
        {
            using (var session = AggregateStore.OpenSession(Guid.Empty))
            {
                exception = Catch.Exception(() => session.LoadAggregate<MyAggregate>());
            }
        };

        It should_be_thrown_exception = () =>
            exception.ShouldBeOfType<TransitionStreamNotExistsException>();

        static Exception exception;
    }

    public class when_aggregate_saved : InMemoryAggregateStore
    {
        Because of = () =>
        {
            using (var session = AggregateStore.OpenSession(Guid.Empty))
            {
                var agg = session.CreateAggregate<MyAggregate>();
                agg.ChangeName(default(Guid), "dfdf)");
                session.SaveChanges();
            }
        };        
    }
}