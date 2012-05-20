namespace Escolar.Messages
{
    public interface IMessageEnvelope
    {
        IMessage Message { get; }
        IMessageMetadata Metadata { get; }
    }
}