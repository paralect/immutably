using System;
using System.Collections.Generic;
using System.Linq;
using Escolar.Aggregates;
using Escolar.Messages;
using Escolar.Transitions;
using Machine.Specifications;
using Paralect.Machine.Processes;

namespace Escolar.Tests.Aggs
{
    public class InMemoryAggregateStore
    {
        public static SimpleEvent evnt;
        public static InMemoryTransitionStore store;
        public static AggregateStore AggregateStore;
        public static List<ITransition> transitions;

        Establish context = () =>
        {
            evnt = new SimpleEvent()
            {
                Id = Guid.NewGuid(),
                Year = 54545,
                Name = "Lenin"
            };

            store = new InMemoryTransitionStore();

            AggregateStore = new AggregateStore(store);
        };
    }
/*
    public class when_someone_something_doing : InMemoryAggregateStore
    {
        Because of = () =>
        {
            using (var session = AggregateStore.OpenSession())
            {
                session.Load<>()
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