using System;
using System.Collections.Generic;

namespace Escolar.Messages
{
    public class MessageMetadata : IMessageMetadata
    {
        /// <summary>
        /// Unique Message identity
        /// </summary>
        public Guid MessageId { get; set; }

        /// <summary>
        /// ID of message that was a stimulus to produce this message.
        /// If there is no stimulus for this message, then TriggerMessageId should return Guid.Empty.
        /// TriggerMessageId allows partially to restore causality of messages.
        /// </summary>
        public Guid TriggerMessageId { get; set; }

        /// <summary>
        /// Message tag identifies message type.
        /// </summary>
        public Guid MessageTag { get; set; }

        /// <summary>
        /// UTC time of message creation
        /// </summary>
        public DateTime CreatedUtc { get; set; }
    }
}