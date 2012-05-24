namespace Immutably.Messages
{
    public interface IEventEnvelope : IMessageEnvelope
    {
        IEvent Event { get; }
        new IEventMetadata Metadata { get; }
    }
}