using System;
using System.Collections.Generic;
using System.Linq;
using Escolar.Messages;
using Escolar.Transitions;
using Machine.Specifications;

namespace Escolar.Tests.Specs
{
    [Subject("Transition Store")]
    public class when_one_transition_with_one_event_is_written : InMemoryTransitionStoreContext
    {
        Because of = () =>
        {
            WriteTransition(evnt.Id, 5, evnt);
            transitions = ReadAllTransitions();
        };

        It should_contain_one_transition = () =>
            transitions.Count.ShouldEqual(1);

        It should_contain_the_same_event_in_this_transition_using_EventEnvelope_property = () =>
            transitions[0].EventEnvelopes[0].Event.ShouldEqual(evnt);

        It should_contain_the_same_event_in_this_transition_using_Events_property = () =>
            transitions[0].Events[0].ShouldEqual(evnt);

        It should_have_correct_stream_id = () =>
            transitions[0].StreamId.ShouldEqual(evnt.Id);

        It should_have_correct_stream_sequence = () =>
            transitions[0].StreamSequence.ShouldEqual(5);

        It should_have_increased_transition_sequence = () =>
            transitions[0].TransitionSequence.ShouldEqual(2);
    }

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