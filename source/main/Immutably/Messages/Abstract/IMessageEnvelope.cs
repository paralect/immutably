namespace Immutably.Messages
{
    public interface IMessageEnvelope
    {
        object Message { get; }
        IMessageMetadata Metadata { get; }
    }
}