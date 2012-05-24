using System;

namespace Immutably.Messages
{
    
    public class EventMetadata : MessageMetadata, IEventMetadata
    {
        /// <summary>
        /// Id of Aggregate Root, Service or Process that emits this events.
        /// </summary>
        public String SenderId { get; set; }

        /// <summary>
        /// StreamSequence of Aggregate Root, Service, Saga or Process at the moment event was emitted.
        /// Emitting party should increment version and next event should have version incremented by one.
        /// Can be used to preserve ordering of messages inside Aggregate boundary.
        /// Starts from 1.
        /// </summary>
        public int StreamSequence { get; set; }

        /// <summary>
        /// Starts from 1.
        /// </summary>
        public int TransitionSequence { get; set; }
    }
}