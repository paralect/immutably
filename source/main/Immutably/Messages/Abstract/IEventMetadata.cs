using System;

namespace Immutably.Messages
{
    public interface IEventMetadata : IMessageMetadata
    {
        /// <summary>
        /// Id of Aggregate Root, Service, Saga or Process that emits this events.
        /// </summary>
        Object SenderId { get; set; }

        /// <summary>
        /// StreamSequence of Aggregate Root, Service, Saga or Process at the moment event was emitted.
        /// Emitting party should increment version and next event should have version incremented by one.
        /// Can be used to preserve ordering of messages inside Aggregate boundary.
        /// </summary>
        Int32 StreamSequence { get; set; }

        Int32 TransitionSequence { get; set; }        
    }

    public interface IEventMetadata<TId> : IEventMetadata
    {
        /// <summary>
        /// Id of Aggregate Root, Service, Saga or Process that emits this events.
        /// </summary>
        new TId SenderId { get; set; }
    }
}