using System;
using Immutably.Aggregates;
using Immutably.Data;
using Machine.Specifications;

namespace Immutably.Tests.Specs.aggregate_store.empty_aggregate_store
{
    public class _empty_aggregate_store_context
    {
        Establish context = () =>
        {
            //transitionStore = new InMemoryTransitionStore(new DefaultDataFactory());
            aggregateStore = new AggregateStore(new EscolarFactory(), null);
        };

        //public static InMemoryTransitionStore transitionStore;
        public static AggregateStore aggregateStore;
    }

    /// <summary>
    /// Statefull aggregate
    /// </summary>
    public class MyState
    { }
    public class MyStatefullAggregate : StatefullAggregate<MyState> { }

    /// <summary>
    /// Stateless aggregate
    /// </summary>
    public class MyStatelessAggregate : StatelessAggregate { }
}