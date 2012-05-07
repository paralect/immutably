using Machine.Specifications;

namespace Escolar.Tests.Specs
{
    [Subject("Transition Store")]
    public class when_one_transition_with_two_events_is_written : InMemoryTransitionStoreContext
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
            transitions[0].EventEnvelopes.Count.ShouldEqual(2);

        It should_have_correct_stream_id = () =>
            transitions[0].StreamId.ShouldEqual(evnt.Id);

        It should_have_correct_stream_sequence = () =>
            transitions[0].StreamSequence.ShouldEqual(1);
    }
}