using System;

namespace Immutably.Messages
{
    public interface IMessageMetadata
    {
        /// <summary>
        /// Unique Message identity
        /// </summary>
        Guid MessageId { get; set; }

        /// <summary>
        /// ID of message that was a stimulus to produce this message.
        /// If there is no stimulus for this message, then TriggerMessageId should return Guid.Empty.
        /// TriggerMessageId allows partially to restore causality of messages.
        /// </summary>
        Guid TriggerMessageId { get; set; }

        /// <summary>
        /// Message tag identifies message type.
        /// </summary>
        Guid MessageTag { get; set; }

        /// <summary>
        /// UTC time of message creation
        /// </summary>
        DateTime CreatedUtc { get; set; }

        /// <summary>
        /// Copy message metadata to another object, that implements IMessageMetadata
        /// </summary>
        void Copy(IMessageMetadata to);
    }
}