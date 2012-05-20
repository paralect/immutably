namespace Escolar.Messages
{
    public class EventEnvelope<TId> : MessageEnvelope, IEventEnvelope<TId>
    {
        public new IEventMetadata<TId> Metadata
        {
            get { return (IEventMetadata<TId>) base.Metadata; }
        }

        public IEvent Event
        {
            get { return (IEvent)Message; }
        }

        public EventEnvelope(IEvent message, IEventMetadata<TId> metadata) : base(message, metadata)
        {
        }
    }
}