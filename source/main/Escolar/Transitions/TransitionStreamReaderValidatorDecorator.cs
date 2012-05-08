using System;
using System.Collections.Generic;

namespace Escolar.Transitions
{
    public class TransitionStreamReaderValidatorDecorator : ITransitionStreamReader
    {
        private readonly ITransitionStreamReader _reader;
        private readonly Guid _streamId;

        public TransitionStreamReaderValidatorDecorator(ITransitionStreamReader reader, Guid streamId)
        {
            _reader = reader;
            _streamId = streamId;
        }

        public IEnumerable<ITransition> Read()
        {
            var streamSequence = 0;

            foreach (var transition in _reader.Read())
            {
                if (transition.StreamId == Guid.Empty)
                    throw new NullReferenceException("Id of transition cannot be null");

                if (transition.StreamId != _streamId)
                    throw new Exception("Invalid transitions stream because of wrong stream ID");

                if (transition.StreamSequence <= streamSequence)
                    throw new Exception("State restoration failed because of out of order stream sequence.");

                var transitionSequence = 0;

                foreach (var eventEnvelope in transition.EventEnvelopes)
                {
                    var metadata = eventEnvelope.Metadata;

                    if (metadata.SenderId == Guid.Empty)
                        throw new NullReferenceException("SenderId in event metadata cannot be null");

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

        public void Dispose()
        {
            if (_reader != null)
                _reader.Dispose();
        }
    }
}