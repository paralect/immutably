using System;
using System.Collections.Generic;
using System.Linq;
using Immutably.Messages;
using Immutably.Transitions;
using Machine.Specifications;

namespace Immutably.Tests.Transitions
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
                Id = Guid.NewGuid().ToString(),
                Year = 54545,
                Name = "Lenin"
            };

            store = new InMemoryTransitionStore();
        };

        public static List<ITransition> LoadAllStreamTransitions()
        {
            using (var reader = store.CreateStreamReader(evnt.Id))
            {
                return reader.ReadAll().ToList();
            }
        }

        public static void WriteTransition(String streamId, Int32 streamSequence, IEvent newEvent)
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