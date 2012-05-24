using System;
using Machine.Specifications;

namespace Immutably.Tests.Transitions
{
    [Subject("Transition Store")]
    public class when_dima_is_playing : InMemoryTransitionStoreContext
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
            transitions[0].StreamSequence.ShouldEqual(1);
    }

}