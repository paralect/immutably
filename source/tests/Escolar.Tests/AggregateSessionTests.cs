using System;
using Escolar.Aggregates;
using NUnit.Framework;
using Paralect.Machine.Processes;

namespace Escolar.Tests
{
    [TestFixture]
    public class AggregateSessionTests
    {
        [Test]
        public void Simple()
        {
            /*
            var factory = new DefaultFactory();

            var transitionStore = factory.CreateTransitionStore();
            var aggregateStore = factory.CreateAggregateStore(transitionStore);

            using (var session = aggregateStore.OpenSession())
            {
                var agg = session.Load<Agg>(Guid.Empty);
                session.SaveChanges();
            }*/
        }
        
        [Test]
        public void Simple2()
        {
            
        }
    }

/*    public class Agg : IAggregate
    {
        public void Initialize(IStateEnvelope stateEnvelope)
        {
            
        }
    }*/
}