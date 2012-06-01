using Machine.Specifications;

namespace Immutably.Tests.Specs.transitions.empty_transition_store.simple_events
{
    [Subject("Transition Store")]
    public class when_one_transition_with_one_event_is_written : empty_in_memory_transition_store_context
    {
        Because of = () =>
        {
            using (var writer = store.CreateStreamWriter(evnt.Id))
            {
                writer.Write(5, builder => builder
                    .AddEvent(evnt)
                );
            }   

            transitions = LoadAllStreamTransitions();
        };

        It should_contain_one_transition = () =>
            transitions.Count.ShouldEqual(1);

        It should_contain_the_same_event_in_this_transition_using_EventEnvelope_property = () =>
            transitions[0].EventsWithMetadata[0].Event.ShouldEqual(evnt);

        It should_contain_the_same_event_in_this_transition_using_Events_property = () =>
            transitions[0].Events[0].ShouldEqual((object) evnt);

        It should_have_correct_stream_id = () =>
            transitions[0].StreamId.ShouldEqual(evnt.Id);

        It should_have_correct_stream_sequence = () =>
            transitions[0].StreamVersion.ShouldEqual(5);
    }
}