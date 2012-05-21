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
        public static Immutably.Aggregates.AggregateStore AggregateStore;
        public static List<ITransition<Guid>> transitions;

        Establish context = () =>
        {
            evnt = new SimpleEvent()
            {
                Id = Guid.NewGuid(),
                Year = 54545,
                Name = "Lenin"
            };

            store = new InMemoryTransitionStore();

            AggregateStore = new Immutably.Aggregates.AggregateStore(new EscolarFactory(), store);
        };
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

    public class MyAggregate : Aggregate<Guid, MyState>
    {
        public MyAggregate()
        {
            //_context = context;
        }

        public void Create(Guid id, String name, Int32 year)
        {
            if (State.Name == null)
                return;

            Apply<MyAggregateCreatedEvent>(evnt =>
            {
                evnt.Id = id;
                evnt.Name = name;
                evnt.Year = year;
            });
        }

        public void ChangeName(Guid id, String newName)
        {
            Apply<MyAggregateNameChangedEvent>(evnt =>
            {
                evnt.Id = id;
                evnt.Name = newName;
            });
        }
    }

    public class MyAggregateCreatedEvent : IEvent
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public Int32 Year { get; set; }
    }

    public class MyAggregateNameChangedEvent : IEvent
    {
        public Guid Id { get; set; }
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