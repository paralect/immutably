using System;
using Immutably.Aggregates;
using Immutably.Messages;

namespace Immutably.Tests.Specs.aggregate_store.empty_aggregate_store.simple_aggregates
{
    /// <summary>
    /// Stateless aggregate
    /// </summary>
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

    /// <summary>
    /// Statefull aggregate
    /// </summary>
    public class SimpleStatefullAggregate : StatefullAggregate<SimpleState>
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

    /// <summary>
    /// State
    /// </summary>
    public class SimpleState
    {
        public String Id { get; set; }
        public String Name { get; set; }
        public Int32 Year { get; set; }

        public void On(SimpleStatelessAggregateCreated created)
        {
            Id = created.Id;
            Name = created.Name;
            Year = created.Year;
        }

        public void On(SimpleStatelessAggregateNameChanged changed)
        {
            Name = changed.Name;
        }
    }


    /// <summary>
    /// Events
    /// </summary>
    public class SimpleStatelessAggregateCreated : IMessage
    {
        public String Id { get; set; }
        public String Name { get; set; }
        public Int32 Year { get; set; }
    }

    public class SimpleStatelessAggregateNameChanged : IMessage
    {
        public String Id { get; set; }
        public String Name { get; set; }
    }
}