using System;
using Escolar.Messages;
using Escolar.Transitions;
using NUnit.Framework;

namespace Escolar.Tests
{
    [TestFixture]
    public class TransitionTests
    {
        [Test]
        public void simple_test()
        {
            var store = new InMemoryTransitionStore();

            var evnt = new SimpleEvent()
            {
                Id = Guid.NewGuid(),
                Year = 54545,
                Name = "Lenin"
            };

            var envelope = evnt.ToEnvelope(evnt.Id);

            using (var session = store.OpenSession())
            {
                session.Append(envelope);
                session.SaveChanges();
            }


            using (var session = store.OpenSession())
            {
                var result = session.LoadTransitions(evnt.Id);

                Assert.That(((SimpleEvent)result[0].EventEnvelopes[0].Event).Year, Is.EqualTo(54545));
            }

        }
    }

    public class SimpleEvent : IEvent
    {
        public Guid Id { get; set; }
        public Int32 Year { get; set; }
        public String Name { get; set; }
    }
}