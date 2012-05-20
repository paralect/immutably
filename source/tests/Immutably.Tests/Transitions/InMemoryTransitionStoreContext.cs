using System;
using System.Collections.Generic;
using System.Linq;
using Escolar.Messages;
using Escolar.Transitions;
using Machine.Specifications;

namespace Escolar.Tests.Specs
{
    public class InMemoryTransitionStoreContext
    {
        public static SimpleEvent evnt;
        public static InMemoryTransitionStore store;
        public static List<ITransition<Guid>> transitions;

        Establish context = () =>
        {
            evnt = new SimpleEvent()
            {
                Id = Guid.NewGuid(),
                Year = 54545,
                Name = "Lenin"
            };

            store = new InMemoryTransitionStore();
        };

        public static List<ITransition<Guid>> LoadAllStreamTransitions()
        {
            using (var reader = store.CreateStreamReader(evnt.Id))
            {
                return reader.Read().ToList();
            }
        }

        public static void WriteTransition(Guid streamId, Int32 streamSequence, IEvent newEvent)
        {
            using (var writer = store.CreateStreamWriter(streamId))
            {
                writer.Write(streamSequence, builder => builder
                    .AddEvent(newEvent)
                );
            }            
        }
    }
}