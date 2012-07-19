using System;
using Immutably.Aggregates;
using Immutably.Data;
using ProtoBuf;

namespace Immutably.Tests.Specs.aggregate_store.empty_aggregate_store.simple_aggregates
{
    /// <summary>
    /// Stateless aggregate
    /// </summary>
    [Aggregate("simple-stateless-aggregate")]
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
    [Aggregate("simple-statefull-aggregate")]
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
    [DataContract("{EC02091B-2DED-43F5-8D5F-9F74E44CE197}")]
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
    [DataContract("{C8E283F5-0580-4852-A834-A0827B57F3A9}"), ProtoContract]
    public class SimpleStatelessAggregateCreated
    {
        public String Id { get; set; }
        public String Name { get; set; }
        public Int32 Year { get; set; }
    }

    [DataContract("{99573C95-CC16-4468-8429-6F7356D98549}"), ProtoContract]
    public class SimpleStatelessAggregateNameChanged
    {
        public String Id { get; set; }
        public String Name { get; set; }
    }
}