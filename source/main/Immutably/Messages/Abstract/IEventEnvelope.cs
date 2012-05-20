namespace Escolar.Messages
{
    public interface IEventEnvelope<TId> : IMessageEnvelope
    {
        IEvent Event { get; }
        new IEventMetadata<TId> Metadata { get; }
    }
}