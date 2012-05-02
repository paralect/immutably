using System;

namespace Escolar.Messages
{
    public class MessageEnvelopeFactory
    {
        /// <summary>
        /// Boring code that enforce correct message and metadata pairs for events, commands and ordinary messages
        /// </summary>
        public static IMessageEnvelope CreateEnvelope(IMessage message, IMessageMetadata messageMetadata)
        {
            if (message == null) throw new ArgumentNullException("message");

            if (message is IEvent)
            {
                messageMetadata = messageMetadata ?? new EventMetadata();

                if (!(messageMetadata is IEventMetadata))
                    throw new Exception("Event message doesn't have EventMetadata.");

                return new EventEnvelope((IEvent) message, (IEventMetadata) messageMetadata);
            }
                
            
            if (message is ICommand)
            {
                messageMetadata = messageMetadata ?? new CommandMetadata();

                if (!(messageMetadata is ICommandMetadata))
                    throw new Exception("Command message doesn't have CommandMetadata.");

                return new CommandEnvelope((ICommand) message, (ICommandMetadata) messageMetadata);
            }
                
            return new MessageEnvelope(message, messageMetadata ?? new MessageMetadata());
        }

        /// <summary>
        /// Creates envelope with default and empty metadata
        /// </summary>
        public static IMessageEnvelope CreateEnvelope(IMessage message)
        {
            return CreateEnvelope(message, null);
        }
    }
}