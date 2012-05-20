using System;
using System.Collections.Generic;
using Immutably.Messages;

namespace Immutably.Transitions
{
    public class TransitionBuilder<TStreamId> : ITransitionBuilder<TStreamId>
    {
        /// <summary>
        /// Event envelops, in order (by transition sequence)
        /// </summary>
        private readonly List<IEventEnvelope<TStreamId>> _eventEnvelopes = new List<IEventEnvelope<TStreamId>>();

        /// <summary>
        /// ID of stream, this transition belongs to
        /// </summary>
        private readonly TStreamId _streamId;

        /// <summary>
        /// Serial number of this transition inside stream
        /// </summary>
        private readonly int _streamSequence;

        /// <summary>
        /// Timestamp when transition was saved to the Store
        /// (or more accurately - current datetime that was set to Transition _before_ storing it to the Store)
        /// </summary>
        private readonly DateTime _timestamp;

        /// <summary>
        /// Next transition sequence for event (unique only inside this transition)
        /// </summary>
        private int _transitionSequence = 1;

        /// <summary>
        /// Creates transition builder
        /// </summary>
        public TransitionBuilder(TStreamId streamId, Int32 streamSequence, DateTime timestamp)
        {
            _streamId = streamId;
            _streamSequence = streamSequence;
            _timestamp = timestamp;
        }

        /// <summary>
        /// Adds event to transition.
        /// Event metadata will be automatically created 
        /// (based on this transition's StreamId, StreamSequence and next available TransitionSequence)
        /// </summary>
        public ITransitionBuilder<TStreamId> AddEvent(IEvent evnt)
        {
            var metadata = new EventMetadata<TStreamId>()
            {
                SenderId = _streamId,
                StreamSequence = _streamSequence,
                TransitionSequence = _transitionSequence
            };

            _eventEnvelopes.Add(new EventEnvelope<TStreamId>(evnt, metadata));
            _transitionSequence++;
            return this;
        }

        /// <summary>
        /// Adds event and corresponding event metadata to this transition
        /// Event metadata should has correct StreamId, StreamSequence and TransitionSequence.
        /// </summary>
        public ITransitionBuilder<TStreamId> AddEvent(IEvent evnt, IEventMetadata<TStreamId> metadata)
        {
            ValidateEventMetadata(metadata);
            _eventEnvelopes.Add(new EventEnvelope<TStreamId>(evnt, metadata));
            _transitionSequence = metadata.TransitionSequence + 1;
            return this;
        }

        /// <summary>
        /// Adds event envelope to this transition
        /// Event metadata should has correct StreamId, StreamSequence and TransitionSequence.
        /// </summary>
        public ITransitionBuilder<TStreamId> AddEvent(IEventEnvelope<TStreamId> envelope)
        {
            ValidateEventMetadata(envelope.Metadata);
            _eventEnvelopes.Add(envelope);
            _transitionSequence = envelope.Metadata.TransitionSequence + 1;
            return this;
        }

        /// <summary>
        /// Build Transition
        /// </summary>
        public ITransition<TStreamId> Build()
        {
            var transition = new Transition<TStreamId>(_streamId, _streamSequence, _timestamp, _eventEnvelopes, true);
            return transition;
        }

        /// <summary>
        /// Performs validation of event metadata
        /// </summary>
        private void ValidateEventMetadata(IEventMetadata<TStreamId> metadata)
        {
            if (!EqualityComparer<TStreamId>.Default.Equals(metadata.SenderId, _streamId))
                throw new Exception("Invalid stream ID");

            if (metadata.StreamSequence != _streamSequence)
                throw new Exception("Invalid stream sequence");

            if (metadata.TransitionSequence <= _transitionSequence)
                throw new Exception("Invalid transition sequence");
        }
    }
}