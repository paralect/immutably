namespace Escolar.Messages
{
    public class EventEnvelope : MessageEnvelope, IEventEnvelope
    {
        public new IEventMetadata Metadata
        {
            get { return (IEventMetadata) base.Metadata; }
        }

        public IEvent Event
        {
            get { return (IEvent)Message; }
        }

        public EventEnvelope(IEvent message, IEventMetadata metadata) : base(message, metadata)
        {
        }
    }
}