using System;
using Immutably.Aggregates;
using Immutably.Messages;
using Immutably.Transitions;
using Machine.Specifications;

namespace Immutably.Tests.Specs.aggregate_store.empty_aggregate_store
{
    public class _empty_aggregate_store_context
    {
        Establish context = () =>
        {
            transitionStore = new InMemoryTransitionStore();
            aggregateStore = new AggregateStore(new EscolarFactory(), null, transitionStore);
        };

        public static InMemoryTransitionStore transitionStore;
        public static AggregateStore aggregateStore;
    }

    /// <summary>
    /// Statefull aggregate
    /// </summary>
    public class MyState : IState { }
    public class MyStatefullAggregate : StatefullAggregate<MyState> { }

    /// <summary>
    /// Stateless aggregate
    /// </summary>
    public class MyStatelessAggregate : StatelessAggregate { }
}