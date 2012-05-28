using System;
using System.Collections.Generic;
using Immutably.Aggregates;
using Immutably.Messages;
using Immutably.Transitions;
using Machine.Specifications;

namespace Immutably.Tests.AggregateStore.StatelessAggregateSpecContext
{

    public class StatelessAggregateSpecContext
    {
        Establish context = () =>
        {
            evnt = new SimpleEvent()
            {
                Id = Guid.NewGuid().ToString(),
                Year = 54545,
                Name = "John"
            };

            store = new InMemoryTransitionStore();

            aggregateStore = new Immutably.Aggregates.AggregateStore(new EscolarFactory(), null, store);
        };

        public static SimpleEvent evnt;
        public static InMemoryTransitionStore store;
        public static Immutably.Aggregates.AggregateStore aggregateStore;
        public static List<ITransition> transitions;
    }

    public class MyState : IState
    {
        public Guid Id { get; set; }
        public String Name { get; set; }

        public void On(MyAggregateNameChangedEvent evnt)
        {
            Name = evnt.Name;
        }
    }

    public class MyStatelessAggregate : StatelessAggregate
    {
        public MyStatelessAggregate()
        {
            //_context = context;
        }

        public void Create(Guid id, String name, Int32 year)
        {
            Apply<MyStatelessAggregateCreatedEvent>(evnt =>
            {
                evnt.Id = id;
                evnt.Name = name;
                evnt.Year = year;
            });
        }

        public void ChangeName(Guid id, String newName)
        {
            Apply<MyStatelessAggregateNameChangedEvent>(evnt =>
            {
                evnt.Id = id;
                evnt.Name = newName;
            });
        }
    }

    public class MyStatelessAggregateCreatedEvent : IEvent
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public Int32 Year { get; set; }
    }

    public class MyStatelessAggregateNameChangedEvent : IEvent
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
    }
}