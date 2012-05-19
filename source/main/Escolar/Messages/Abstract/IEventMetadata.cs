using System;

namespace Escolar.Messages
{
    public interface IEventMetadata<TId> : IMessageMetadata
    {
        /// <summary>
        /// Id of Aggregate Root, Service, Saga or Process that emits this events.
        /// </summary>
        TId SenderId { get; set; }

        /// <summary>
        /// StreamSequence of Aggregate Root, Service, Saga or Process at the moment event was emitted.
        /// Emitting party should increment version and next event should have version incremented by one.
        /// Can be used to preserve ordering of messages inside Aggregate boundary.
        /// </summary>
        Int32 StreamSequence { get; set; }

        Int32 TransitionSequence { get; set; }
    }
}