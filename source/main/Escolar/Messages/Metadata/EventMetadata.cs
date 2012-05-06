using System;

namespace Escolar.Messages
{
    
    public class EventMetadata : MessageMetadata, IEventMetadata
    {
        /// <summary>
        /// Id of Aggregate Root, Service or Process that emits this events.
        /// </summary>
        public Guid SenderId { get; set; }

        /// <summary>
        /// StreamSequence of Aggregate Root, Service, Saga or Process at the moment event was emitted.
        /// Emitting party should increment version and next event should have version incremented by one.
        /// Can be used to preserve ordering of messages inside Aggregate boundary.
        /// </summary>
        public int StreamSequence { get; set; }

        public int TransitionSequence { get; set; }
    }
}