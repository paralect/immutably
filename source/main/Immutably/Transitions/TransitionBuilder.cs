using System;
using System.Collections.Generic;
using Immutably.Messages;

namespace Immutably.Transitions
{
    public class TransitionBuilder : ITransitionBuilder
    {
        /// <summary>
        /// Event envelops, in order (by transition sequence)
        /// </summary>
        private readonly List<IEventEnvelope> _eventEnvelopes = new List<IEventEnvelope>();

        /// <summary>
        /// ID of stream, this transition belongs to
        /// </summary>
        private readonly String _streamId;

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
        public TransitionBuilder(String streamId, Int32 streamSequence, DateTime timestamp)
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
        public ITransitionBuilder AddEvent(IEvent evnt)
        {
            var metadata = new EventMetadata()
            {
                SenderId = _streamId,
                StreamSequence = _streamSequence,
                TransitionSequence = _transitionSequence
            };

            _eventEnvelopes.Add(new EventEnvelope(evnt, metadata));
            _transitionSequence++;
            return this;
        }

        /// <summary>
        /// Adds event and corresponding event metadata to this transition
        /// Event metadata should has correct StreamId, StreamSequence and TransitionSequence.
        /// </summary>
        public ITransitionBuilder AddEvent(IEvent evnt, IEventMetadata metadata)
        {
            ValidateEventMetadata(metadata);
            _eventEnvelopes.Add(new EventEnvelope(evnt, metadata));
            _transitionSequence = metadata.TransitionSequence + 1;
            return this;
        }

        /// <summary>
        /// Adds event envelope to this transition
        /// Event metadata should has correct StreamId, StreamSequence and TransitionSequence.
        /// </summary>
        public ITransitionBuilder AddEvent(IEventEnvelope envelope)
        {
            ValidateEventMetadata(envelope.Metadata);
            _eventEnvelopes.Add(envelope);
            _transitionSequence = envelope.Metadata.TransitionSequence + 1;
            return this;
        }

        /// <summary>
        /// Build Transition
        /// </summary>
        public ITransition Build()
        {
            var transition = new Transition(_streamId, _streamSequence, _timestamp, _eventEnvelopes, true);
            return transition;
        }

        ITransitionBuilder ITransitionBuilder.AddEvent(IEvent evnt)
        {
            return AddEvent(evnt);
        }

        ITransitionBuilder ITransitionBuilder.AddEvent(IEvent evnt, IEventMetadata metadata)
        {
            return AddEvent(evnt, (IEventMetadata) metadata);
        }

        ITransitionBuilder ITransitionBuilder.AddEvent(IEventEnvelope envelope)
        {
            return AddEvent((IEventEnvelope) envelope);
        }

        ITransition ITransitionBuilder.Build()
        {
            return Build();
        }

        /// <summary>
        /// Performs validation of event metadata
        /// </summary>
        private void ValidateEventMetadata(IEventMetadata metadata)
        {
            if (metadata.SenderId != _streamId)
                throw new Exception("Invalid stream ID");

            if (metadata.StreamSequence != _streamSequence)
                throw new Exception("Invalid stream sequence");

            if (metadata.TransitionSequence <= _transitionSequence)
                throw new Exception("Invalid transition sequence");
        }
    }
}