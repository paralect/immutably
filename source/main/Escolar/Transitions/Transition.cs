using System;
using System.Collections.Generic;
using System.Linq;
using Escolar.Messages;
using Escolar.Transitions;

namespace Escolar.Transitions
{
    /// <summary>
    /// Transition - is a way to group a number of modifications (events) 
    /// for **one** Stream (usually Aggregate Root) in one atomic package, 
    /// that can be either canceled or persisted by Event Store.
    /// </summary>  
    public class Transition : ITransition, ITransitionBuilder
    {
        private readonly List<IEventEnvelope> _eventEnvelopes = new List<IEventEnvelope>();
        private readonly List<IEvent> _events = new List<IEvent>();


        /// <summary>
        /// Stream ID
        /// </summary>
        private readonly Guid _streamId;

        /// <summary>
        /// Stream sequence for this transition 
        /// </summary>
        private readonly int _streamSequence;

        /// <summary>
        /// Next transition sequence for event (unique only inside this transition)
        /// </summary>
        private int _transitionSequence = 1;


        public IList<IEventEnvelope> EventEnvelopes
        {
            get
            {
                return _eventEnvelopes.AsReadOnly();
            }
        }

        public IList<IEvent> Events
        {
            get 
            { 
                return _eventEnvelopes
                    .Select(envelope => envelope.Event)
                    .ToList()
                    .AsReadOnly(); 
            }
        }

        public int StreamSequence
        {
            get { return _streamSequence; }
        }

        public int TransitionSequence
        {
            get { return _transitionSequence; }
        }

        public Guid StreamId
        {
            get { return _streamId; }
        }

        public Transition(Guid streamId, Int32 streamSequence)
        {
            _streamId = streamId;
            _streamSequence = streamSequence;
        }

        public Transition(List<IEventEnvelope> eventEnvelopes, Boolean validate = true)
        {
            _eventEnvelopes = eventEnvelopes;
            _streamSequence = eventEnvelopes.Last().Metadata.StreamSequence;
            _streamId = eventEnvelopes.Last().Metadata.SenderId;

            if (validate)
            {
                foreach (var eventEnvelope in eventEnvelopes)
                {
                    if (eventEnvelope.Metadata.SenderId != _streamId)
                        throw new Exception("Invalid transition, because events are for different streams");
                }
            }
        }

        public Transition(params IEventEnvelope[] eventEnvelope) : this(eventEnvelope.ToList())
        {
        }

        public ITransitionBuilder AddEvent(IEvent evnt)
        {
            var metadata = new EventMetadata()
            {
                SenderId = _streamId,
                StreamSequence = _streamSequence,
                TransitionSequence = _transitionSequence
            };

            var envelope = new EventEnvelope(evnt, metadata);

            _eventEnvelopes.Add(envelope);

            _transitionSequence++;

            return this;
        }

        public ITransitionBuilder AddEvent(IEvent evnt, IEventMetadata metadata)
        {
            ValidateEventMetadata(metadata);
            _eventEnvelopes.Add(new EventEnvelope(evnt, metadata));
            _transitionSequence = metadata.TransitionSequence + 1;
            return this;
        }

        public ITransitionBuilder AddEvent(IEventEnvelope envelope)
        {
            ValidateEventMetadata(envelope.Metadata);
            _eventEnvelopes.Add(envelope);
            _transitionSequence = envelope.Metadata.TransitionSequence + 1;
            return this;
        }

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