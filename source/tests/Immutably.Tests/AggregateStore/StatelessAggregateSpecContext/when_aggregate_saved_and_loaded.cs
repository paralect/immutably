using System;
using Machine.Specifications;

namespace Immutably.Tests.AggregateStore.StatelessAggregateSpecContext
{
    public class when_aggregate_saved_and_loaded : StatelessAggregateSpecContext
    {
        Because of = () =>
        {
/*            using (var session = aggregateStore.OpenSession())
            {
                var agg = session.CreateAggregate<MyStatelessAggregate>(id);
                agg.ChangeName(Guid.Empty, "hello");
                session.SaveChanges();
            }

            using (var session = aggregateStore.OpenSession())
            {
                var agg = new MyAggregate();
                agg.ChangeName(Guid.Empty, "hello");
                session.SaveChanges();
            }
            
            using (var session = aggregateStore.OpenSession(id))
            {
                var agg = session.LoadAggregate<MyStatelessAggregate>();
                agg.ChangeName(Guid.Empty, "hello");
                session.SaveChanges();
            }


            using (var session = aggregateStore.OpenSession(id))
            {
                session.LoadAggregate<MyStatelessAggregate>(agg => 
                {
                    agg.ChangeName(Guid.Empty, "hello");
                });
                
                session.SaveChanges();
            }

            using (var session = aggregateStore.OpenSession(id))
            {
                session.CreateAggregate<MyStatelessAggregate>(agg =>
                {
                    agg.ChangeName(Guid.Empty, "hello");
                });

                session.SaveChanges();
            }

            using (var agg = store.CreateAggregate<MyAggregate>(id))
            {
                agg.ChangeName(Guid.Empty, "hello");
                agg.SaveChanges();
            }

            using (var agg = store.LoadAggregate<MyAggregate>(id))
            {
                agg.ChangeName(Guid.Empty, "hello");
                agg.SaveChanges();
            }

            using (var session = aggregateStore.CreateAggregateSession(id))
            {
                session.Aggregate.ChangeName(Guid.Empty, "hello");
                session.SaveChanges();
            }*/
        };

        static String id = Guid.NewGuid().ToString();
    }
}