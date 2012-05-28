using System;
using Immutably.Aggregates;
using Immutably.Data;
using Immutably.Messages;

namespace Immutably.Tests.Aggregates
{
    public class AggregateMachineContext
    {
         
    }

    public class MyAggregate : StatefullAggregate<MyState>
    {
        public void Create(Guid id, String name, Int32 year)
        {
            if (State.Name == null)
                return;

            Apply<AggregateStore.MyAggregateCreatedEvent>(evnt =>
            {
                evnt.Id = id;
                evnt.Name = name;
                evnt.Year = year;
            });
        }

        public void ChangeName(Guid id, String newName)
        {
            Apply<AggregateStore.MyAggregateNameChangedEvent>(evnt =>
            {
                evnt.Id = id;
                evnt.Name = newName;
            });
        }
    }

    [DataContract("{A3E32124-0FCD-468A-A849-EEE53C3555E6}")]
    public class MyState : IState
    {
        public Guid Id { get; set; }
        public String Name { get; set; }

        public void On(MyAggregateCreatedEvent evnt)
        {
            Id = evnt.Id;
            Name = evnt.Name;
        }

        public void On(MyAggregateNameChangedEvent changed)
        {
            Name = changed.Name;
        }
    }

    [DataContract("{87DFD638-A798-4D63-911E-E54359C105AC}")]
    public class MyAggregateCreatedEvent : IEvent
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public Int32 Year { get; set; }
    }

    [DataContract("{6B4BEA1A-F5E1-4A5A-9771-A96A76A1DB03}")]
    public class MyAggregateNameChangedEvent : IEvent
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
    }
}