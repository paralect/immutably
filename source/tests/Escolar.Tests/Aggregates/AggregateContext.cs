using System;
using Escolar.Aggregates;
using Escolar.Messages;
using Paralect.Machine.Processes;

namespace Escolar.Tests.Aggregates
{
    public class AggregateContext
    {
         
    }

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

    public class MyAggregate : Aggregate<Guid, MyState>
    {
        public void Create(Guid id, String name, Int32 year)
        {
            if (State.Name == null)
                return;

            Apply<Aggs.MyAggregateCreatedEvent>(evnt =>
            {
                evnt.Id = id;
                evnt.Name = name;
                evnt.Year = year;
            });
        }

        public void ChangeName(Guid id, String newName)
        {
            Apply<Aggs.MyAggregateNameChangedEvent>(evnt =>
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
}