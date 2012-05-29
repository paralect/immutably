using System;
using Immutably.Aggregates;
using Immutably.Messages;

namespace Immutably.Tests.Environments.SimpleStatelessAggregate
{
    public class SimpleStatelessAggregate : StatelessAggregate
    {
        public void Create(String name, Int32 year)
        {
            Apply<SimpleStatelessAggregateCreated>(evnt =>
            {
                evnt.Id = Id;
                evnt.Name = name;
                evnt.Year = year;
            });
        }

        public void ChangeName(String newName)
        {
            Apply<SimpleStatelessAggregateNameChanged>(evnt =>
            {
                evnt.Id = Id;
                evnt.Name = newName;
            });
        }
    }

    public class SimpleStatelessAggregateCreated : IEvent
    {
        public String Id { get; set; }
        public String Name { get; set; }
        public Int32 Year { get; set; }
    }

    public class SimpleStatelessAggregateNameChanged : IEvent
    {
        public String Id { get; set; }
        public String Name { get; set; }
    }
}