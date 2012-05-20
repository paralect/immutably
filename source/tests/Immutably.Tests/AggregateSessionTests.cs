using NUnit.Framework;

namespace Immutably.Tests
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
                var agg = session.LoadAggregate<Agg>(Guid.Empty);
                session.SaveChanges();
            }*/
        }
        
        [Test]
        public void Simple2()
        {
            
        }
    }
}