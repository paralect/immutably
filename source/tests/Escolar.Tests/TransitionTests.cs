using System;
using System.Linq;
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

            var envelope = evnt.ToEnvelope(evnt.Id, 1);

            using (var writer = store.CreateStreamWriter(evnt.Id))
            {
                writer.Write(new Transition(evnt.ToEnvelope(evnt.Id, 1)));
                writer.Write(new Transition(evnt.ToEnvelope(evnt.Id, 3)));
                writer.Write(new Transition(evnt.ToEnvelope(evnt.Id, 4)));

                writer.Write(5, builder => builder
                    .AddEvent(evnt)
                    .AddEvent(evnt)
                    .AddEvent(evnt)
                );
            }

            using (var reader = store.CreateStreamReader(evnt.Id))
            {
                foreach(var transition in reader.Read())
                {

                }
            }

/*            using (var session = store.OpenStream(evnt.Id))
            {
                var result = session.LoadTransitions();

                Assert.That(((SimpleEvent)result[0].EventEnvelopes[0].Event).Year, Is.EqualTo(54545));
            }
 * */

        }

        public void simple_2()
        {
            var evnt = new SimpleEvent()
            {
                Id = Guid.NewGuid(),
                Year = 54545,
                Name = "Lenin"
            };

            var metadata = new EventMetadata()
            {
                SenderId = evnt.Id,
                StreamSequence = 1
            };

            var envelope = new EventEnvelope(evnt, metadata);

            var transition = new Transition(envelope);

            // or:

            var transitionAnother = new Transition(
                new EventEnvelope(
                    new SimpleEvent()
                    {
                        Id = Guid.NewGuid(),
                        Year = 54545,
                        Name = "Lenin"
                    }, 
                    new EventMetadata()
                    {
                        SenderId = evnt.Id,
                        StreamSequence = 1
                    }
                )
            );

        }
    }

    public class SimpleEvent : IEvent
    {
        public Guid Id { get; set; }
        public Int32 Year { get; set; }
        public String Name { get; set; }
    }
}