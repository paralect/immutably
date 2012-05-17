using System;
using Escolar.Tests.Aggs;
using Machine.Specifications;

namespace Escolar.Tests.Aggregates
{
    public class when_aggregate_created
    {
        Because of = () =>
        {
/*            var agg = new MyAggregate();

            agg.Apply<MyAggregateCreatedEvent>(evnt =>
            {
                evnt.Id = Guid.Empty;
                evnt.Name = "Hello";
                evnt.Year = 2012;
            });*/
        };
    }

    public class zzz : when_aggregate_created
    {
        public zzz()
        {
            
        }
    }
}