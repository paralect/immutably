using System;
using Immutably.Aggregates;
using Immutably.Data;
using Immutably.Serialization;
using Immutably.Tests.Specs.aggregate_store.empty_aggregate_store.simple_aggregates;
using Lokad.Cqrs.TapeStorage;
using Machine.Specifications;

namespace Immutably.Tests.Specs.aggregate_store.empty_aggregate_store
{
    public class _empty_aggregate_store_context
    {
        Establish context = () =>
        {
            var dataContext = DataContext.Create(builder => builder
                .AddContract<SimpleStatelessAggregateCreated>()
                .AddContract<SimpleStatelessAggregateNameChanged>()
            );

            transitionStore = new FileTransitionStore(@"c:\tmp\store");
            aggregateStore = new AggregateStore(transitionStore, new DefaultDataFactory(), new ProtobufSerializer(dataContext));
        };

        public static FileTransitionStore transitionStore;
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