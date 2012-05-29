using Machine.Specifications;

namespace Immutably.Tests.Specs.transitions.empty_transition_store.simple_events
{
    [Subject("Transition Store")]
    public class when_two_events_are_written : empty_in_memory_transition_store_context
    {
        private Because of = () =>
        {
            using (var writer = store.CreateStreamWriter(evnt.Id))
            {
                writer.Write(1, builder => builder
                    .AddEvent(evnt)
                    .AddEvent(evnt)
                );
            }

            transitions = LoadAllStreamTransitions();
        };

        It should_contain_one_transition = () =>
            transitions.Count.ShouldEqual(1);

        It should_has_two_event = () =>
            transitions[0].EventsWithMetadata.Count.ShouldEqual(2);

        It should_have_correct_stream_id = () =>
            transitions[0].StreamId.ShouldEqual(evnt.Id);

        It should_have_correct_stream_sequence = () =>
            transitions[0].StreamVersion.ShouldEqual(1);
    }

}