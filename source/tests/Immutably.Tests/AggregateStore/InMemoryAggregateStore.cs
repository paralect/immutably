using System;
using System.Collections.Generic;
using Immutably.Aggregates;
using Immutably.Messages;
using Immutably.Transitions;
using Machine.Specifications;

namespace Immutably.Tests.AggregateStore
{
    public class InMemoryAggregateStore
    {
        public static SimpleEvent evnt;
        public static InMemoryTransitionStore store;
        public static Immutably.Aggregates.AggregateStore aggregateStore;
        public static List<ITransition> transitions;

        Establish context = () =>
        {
            evnt = new SimpleEvent()
            {
                Id = Guid.NewGuid().ToString(),
                Year = 54545,
                Name = "Lenin"
            };

            store = new InMemoryTransitionStore();

            aggregateStore = new Immutably.Aggregates.AggregateStore(new EscolarFactory(), null, store);
        };
    }

    public class MyState : IState
    {
        public String Id { get; set; }
        public String Name { get; set; }

        public void On(MyStatefullAggregateNameChangedEvent evnt)
        {
            Name = evnt.Name;
        }

        public void On(MyStatefullAggregateCreatedEvent evnt)
        {
            Id = evnt.Id;
            Name = evnt.Name;
        }
    }

    public class MyStatefullAggregate : StatefullAggregate<MyState>
    {
        public void Create(String name, Int32 year)
        {
            if (State.Name == null)
                return;

            Apply<MyStatefullAggregateCreatedEvent>(evnt =>
            {
                evnt.Id = Id;
                evnt.Name = name;
                evnt.Year = year;
            });
        }

        public void ChangeName(String newName)
        {
            Apply<MyStatefullAggregateNameChangedEvent>(evnt =>
            {
                evnt.Id = Id;
                evnt.Name = newName;
            });
        }
    }

    public class MyStatefullAggregateCreatedEvent : IEvent
    {
        public String Id { get; set; }
        public String Name { get; set; }
        public Int32 Year { get; set; }
    }

    public class MyStatefullAggregateNameChangedEvent : IEvent
    {
        public String Id { get; set; }
        public String Name { get; set; }
    }


/*
    public class when_someone_something_doing : InMemoryAggregateStore
    {
        Because of = () =>
        {
            using (var session = AggregateStore.OpenSession())
            {
                session.LoadAggregate<>()
            }
        };
    }

    public class state : State
    {
        public void Apply(IEvent events)
        {
            throw new NotImplementedException();
        }
    }

    public class agg : Aggregate
    {
        
    }

    public class MyAttribute : Attribute
    {
        
    }

    [MyAttribute]
    interface IInterface
    {
        [MyAttribute]
        Int32 GetInt();
    }*/
}