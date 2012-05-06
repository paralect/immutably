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
        };

        public static List<ITransition> ReadAllTransitions()
        {
            List<ITransition> list = null;
            using (var reader = store.CreateStreamReader(evnt.Id))
            {
                list = reader.Read().ToList();
            }

            return list;
        }

        public static void WriteTransition(Guid streamId, Int32 streamSequence, IEvent evntt)
        {
            using (var writer = store.CreateStreamWriter(streamId))
            {
                writer.Write(streamSequence, builder => builder
                    .AddEvent(evntt)
                );
            }            
        }
    }
}