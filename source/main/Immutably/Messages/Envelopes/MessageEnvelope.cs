namespace Immutably.Messages
{
    public class MessageEnvelope : IMessageEnvelope
    {
        public object Message { get; private set; }
        public IMessageMetadata Metadata { get; private set; }

        public MessageEnvelope(object message, IMessageMetadata metadata)
        {
            Metadata = metadata;
            Message = message;
        }
    }
}