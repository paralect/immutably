using System;
using System.Collections.Generic;

namespace Immutably.Transitions
{
    public class TransitionStreamOrderValidator
    {
        private readonly IEnumerable<ITransition> _transitions;
        private readonly String _streamId;

        public TransitionStreamOrderValidator(String streamId, IEnumerable<ITransition> transitions)
        {
            _streamId = streamId;
            _transitions = transitions;
        }

        public IEnumerable<ITransition> Read()
        {
            var streamSequence = 0;

            foreach (var transition in _transitions)
            {
                

                if (transition.StreamId == null)
                    throw new NullReferenceException("Id of transition cannot be null for reference types and default(TStreamId) for value type");

                if (transition.StreamId != _streamId)
                    throw new Exception("Invalid transitions stream because of wrong stream ID");

                if (transition.StreamSequence <= streamSequence)
                    throw new Exception("State restoration failed because of out of order stream sequence.");

                var transitionSequence = 0;

                foreach (var eventEnvelope in transition.EventsWithMetadata)
                {
                    var metadata = eventEnvelope.Metadata;

                    if (metadata.SenderId == null)
                        throw new NullReferenceException("SenderId in event metadata cannot be null for reference type, or default(T) for value type");

                    if (metadata.SenderId != _streamId)
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