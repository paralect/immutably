using System;
using System.Collections.Generic;
using Immutably.Data;
using Immutably.Messages;

namespace Immutably.Transitions
{
    public class TransitionBuilder : ITransitionBuilder
    {
        /// <summary>
        /// Event envelops, in order (by transition sequence)
        /// </summary>
        private readonly List<IEventEnvelope> _eventEnvelopes = new List<IEventEnvelope>();

        private readonly IDataFactory _dataFactory;

        /// <summary>
        /// ID of stream, this transition belongs to
        /// </summary>
        private readonly String _streamId;

        /// <summary>
        /// Serial number of this transition inside stream
        /// </summary>
        private readonly int _streamVersion;

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
        public TransitionBuilder(IDataFactory dataFactory, String streamId, Int32 streamVersion, DateTime timestamp)
        {
            _dataFactory = dataFactory;
            _streamId = streamId;
            _streamVersion = streamVersion;
            _timestamp = timestamp;
        }

        /// <summary>
        /// Adds event to transition.
        /// Event metadata will be automatically created 
        /// (based on this transition's StreamId, StreamVersion and next available TransitionSequence)
        /// </summary>
        public ITransitionBuilder AddEvent(IEvent evnt)
        {
            var metadata = _dataFactory.Create<EventMetadata>(m =>
            {
                m.SenderId = _streamId;
                m.StreamVersion = _streamVersion;
                m.TransitionSequence = _transitionSequence;
            });

            _eventEnvelopes.Add(new EventEnvelope(evnt, metadata));
            _transitionSequence++;
            return this;
        }

        /// <summary>
        /// Adds event and corresponding event metadata to this transition
        /// Event metadata should has correct StreamId, StreamVersion and TransitionSequence.
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
        /// Event metadata should has correct StreamId, StreamVersion and TransitionSequence.
        /// </summary>
        public ITransitionBuilder AddEvent(IEventEnvelope envelope)
        {
            ValidateEventMetadata(envelope.Metadata);
            _eventEnvelopes.Add(envelope);
            _transitionSequence = envelope.Metadata.TransitionSequence + 1;
            return this;
        }

        /// <summary>
        /// Adds events to transition.
        /// Event metadata will be automatically created 
        /// (based on this transition's StreamId, StreamVersion and next available TransitionSequence)
        /// </summary>
        public ITransitionBuilder AddEvents(IEnumerable<IEvent> events)
        {
            foreach (var evnt in events)
                AddEvent(evnt);

            return this;            
        }

        /// <summary>
        /// Build Transition
        /// </summary>
        public ITransition Build()
        {
            var transition = new Transition(_streamId, _streamVersion, _timestamp, _eventEnvelopes, true);
            return transition;
        }

        /// <summary>
        /// Performs validation of event metadata
        /// </summary>
        private void ValidateEventMetadata(IEventMetadata metadata)
        {
            if (metadata.SenderId != _streamId)
                throw new Exception("Invalid stream ID");

            if (metadata.StreamVersion != _streamVersion)
                throw new Exception("Invalid stream version");

            if (metadata.TransitionSequence <= _transitionSequence)
                throw new Exception("Invalid transition sequence");
        }
    }
}