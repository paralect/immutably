namespace Escolar.Messages
{
    public class MessageEnvelope : IMessageEnvelope
    {
        public IMessage Message { get; private set; }
        public IMessageMetadata Metadata { get; private set; }

        public MessageEnvelope(IMessage message, IMessageMetadata metadata)
        {
            Metadata = metadata;
            Message = message;
        }
    }
}