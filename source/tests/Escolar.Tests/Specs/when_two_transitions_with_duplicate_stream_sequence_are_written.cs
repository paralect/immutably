using System;
using Escolar.Transitions;
using Machine.Specifications;

namespace Escolar.Tests.Specs
{
    [Subject("Transition Store")]
    public class when_two_transitions_with_duplicate_stream_sequence_are_written : InMemoryTransitionStoreContext
    {
        static Exception exception;

        Because of = () =>
        {
            WriteTransition(evnt.Id, 3, evnt);
            exception = Catch.Exception(() => WriteTransition(evnt.Id, 3, evnt));
            transitions = ReadAllTransitions();
        };

        It should_fail = () =>
            exception.ShouldBeOfType<TransitionAlreadyExistException>();

        It should_contain_one_transition = () =>
            transitions.Count.ShouldEqual(1);

        It should_has_one_event = () =>
            transitions[0].EventEnvelopes.Count.ShouldEqual(1);

        It should_have_correct_stream_id = () =>
            transitions[0].StreamId.ShouldEqual(evnt.Id);

        It should_have_correct_stream_sequence = () =>
            transitions[0].StreamSequence.ShouldEqual(3);

        It should_have_increased_transition_sequence = () =>
            transitions[0].TransitionSequence.ShouldEqual(2);
    }
}