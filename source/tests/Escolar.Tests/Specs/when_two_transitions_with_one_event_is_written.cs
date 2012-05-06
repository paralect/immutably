using Machine.Specifications;

namespace Escolar.Tests.Specs
{
    [Subject("Transition Store")]
    public class when_two_transitions_with_one_event_is_written : InMemoryTransitionStoreContext
    {
        Because of = () =>
        {
            WriteTransition(evnt.Id, 1, evnt);
            WriteTransition(evnt.Id, 2, evnt);
            transitions = ReadAllTransitions();
        };

        It should_contain_two_transition = () =>
            transitions.Count.ShouldEqual(2);

        It should_has_one_event_in_each_transition = () =>
        {
            transitions[0].EventEnvelopes.Count.ShouldEqual(1);
            transitions[1].EventEnvelopes.Count.ShouldEqual(1);
        };

        It should_contain_the_same_event_in_every_transition_using_EventEnvelope_property = () =>
        {
            transitions[0].EventEnvelopes[0].Event.ShouldEqual(evnt);
            transitions[1].EventEnvelopes[0].Event.ShouldEqual(evnt);
        };

        It should_contain_the_same_event_in_this_transition_using_Events_property = () =>
        {
            transitions[0].Events[0].ShouldEqual(evnt);
            transitions[1].Events[0].ShouldEqual(evnt);
        };

        It should_have_correct_stream_id_in_every_transition = () =>
        {
            transitions[0].StreamId.ShouldEqual(evnt.Id);
            transitions[1].StreamId.ShouldEqual(evnt.Id);
        };

        It should_have_correct_stream_sequence = () =>
        {
            transitions[0].StreamSequence.ShouldEqual(1);
            transitions[1].StreamSequence.ShouldEqual(2);
        };

        It should_have_increased_transition_sequence = () =>
        {
            transitions[0].TransitionSequence.ShouldEqual(2);
            transitions[1].TransitionSequence.ShouldEqual(2);
        };
    }
}