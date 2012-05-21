namespace Immutably.Messages
{
    public interface IEventEnvelope : IMessageEnvelope
    {
        
    }

    public interface IEventEnvelope<TId> : IEventEnvelope
    {
        IEvent Event { get; }
        new IEventMetadata<TId> Metadata { get; }
    }
}