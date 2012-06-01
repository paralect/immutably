using Immutably.Aggregates;

namespace Immutably.Tests.Specs.aggregate_store.empty_aggregate_store.simplest_possible_aggregates
{
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