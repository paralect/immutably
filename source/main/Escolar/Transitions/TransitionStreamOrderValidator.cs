using System;
using System.Collections.Generic;

namespace Escolar.Transitions
{
    public class TransitionStreamOrderValidator<TStreamId>
    {
        private readonly IEnumerable<ITransition<TStreamId>> _transitions;
        private readonly TStreamId _streamId;

        public TransitionStreamOrderValidator(TStreamId streamId, IEnumerable<ITransition<TStreamId>> transitions)
        {
            _streamId = streamId;
            _transitions = transitions;
        }

        public IEnumerable<ITransition<TStreamId>> Read()
        {
            var streamSequence = 0;

            var comparer = EqualityComparer<TStreamId>.Default;

            foreach (var transition in _transitions)
            {
                

                if (comparer.Equals(transition.StreamId, default(TStreamId)))
                    throw new NullReferenceException("Id of transition cannot be null for reference types and default(TStreamId) for value type");

                if (!comparer.Equals(transition.StreamId, _streamId))
                    throw new Exception("Invalid transitions stream because of wrong stream ID");

                if (transition.StreamSequence <= streamSequence)
                    throw new Exception("State restoration failed because of out of order stream sequence.");

                var transitionSequence = 0;

                foreach (var eventEnvelope in transition.EventEnvelopes)
                {
                    var metadata = eventEnvelope.Metadata;

                    if (EqualityComparer<TStreamId>.Default.Equals(metadata.SenderId, default(TStreamId)))
                        throw new NullReferenceException("SenderId in event metadata cannot be null for reference type, or default(T) for value type");

                    if (!EqualityComparer<TStreamId>.Default.Equals(metadata.SenderId, _streamId))
                        throw new Exception("Invalid transitions stream because of different stream IDs in the event metadata");

                    if (metadata.TransitionSequence <= transitionSequence)
                        throw new Exception("Invalid transitions stream because of incorrect order of transition sequence");

                    transitionSequence = metadata.TransitionSequence;
                }

                streamSequence = transition.StreamSequence;
                
                yield return transition;
            }
        }
    }
}