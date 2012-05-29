using System;
using System.Collections.Generic;
using System.Linq;
using Immutably.Messages;
using Immutably.Utilities;

namespace Immutably.Transitions
{
    /// <summary>
    /// Transition - is a way to group a number of modifications (events) 
    /// for **one** Stream (usually Aggregate Root) in one atomic package, 
    /// that can be either canceled or persisted by Event Store.
    /// </summary>  
    public class Transition : ITransition
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
        private readonly int _streamVersion;

        /// <summary>
        /// Timestamp when transition was saved to the Store
        /// (or more accurately - current datetime that was set to Transition _before_ storing it to the Store)
        /// </summary>
        private readonly DateTime _timestamp;

        /// <summary>
        /// ID of stream, this transition belongs to
        /// </summary>
        public String StreamId
        {
            get { return _streamId; }
        }

        /// <summary>
        /// Serial number of this transition inside stream
        /// </summary>
        public int StreamVersion
        {
            get { return _streamVersion; }
        }

        /// <summary>
        /// Timestamp when transition was saved to the Store
        /// (or more accurately - current datetime that was set to Transition _before_ storing it to the Store)
        /// </summary>
        public DateTime Timestamp
        {
            get { return _timestamp; }
        }

        /// <summary>
        /// Readonly collection of Event envelopes, in order (by transition sequence)
        /// </summary>
        public IIndexedEnumerable<IEventEnvelope> EventsWithMetadata
        {
            get { return _eventEnvelopes.AsIndexedEnumerable(); }
        }

        /// <summary>
        /// Readonly collection of Events, in order (by transition sequence)
        /// </summary>
        public IIndexedEnumerable<IEvent> Events
        {
            get 
            { 
                return _eventEnvelopes
                    .Select(envelope => envelope.Event)
                    .ToList()
                    .AsIndexedEnumerable(); 
            }
        }

        /// <summary>
        /// Creates transition
        /// </summary>
        /// <param name="validate">
        /// Specifies, should we validate envelopes that they belongs 
        /// to specified <param name="streamId" /> and they all have specified <param name="streamSequence" />
        /// </param>
        public Transition(String streamId, Int32 streamVersion, DateTime timestamp, List<IEventEnvelope> eventEnvelopes, Boolean validate = true)
        {
            _streamId = streamId;
            _streamVersion = streamVersion;
            _timestamp = timestamp;
            _eventEnvelopes = eventEnvelopes;

            if (validate)
            {
                Int32 _transitionSequence = 0;

                foreach (var eventEnvelope in eventEnvelopes)
                {
                    if (eventEnvelope.Metadata.TransitionSequence == 0)
                        throw new Exception("Transition sequence cannot be less or equal to zero.");

                    if (eventEnvelope.Metadata.TransitionSequence <= _transitionSequence)
                        throw new Exception("Invalid transition sequence. Events aren't in order, or ");

                    if (eventEnvelope.Metadata.SenderId != _streamId)
                        throw new Exception("Invalid transition, because events are for different streams");

                    if (eventEnvelope.Metadata.StreamVersion != _streamVersion)
                        throw new Exception("Invalid transition, because events have different stream sequence");

                    _transitionSequence = eventEnvelope.Metadata.TransitionSequence;
                }
            }
        }
    }

}