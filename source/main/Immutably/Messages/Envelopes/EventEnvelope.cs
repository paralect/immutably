using System;

namespace Immutably.Messages
{
    public class EventEnvelope : MessageEnvelope, IEventEnvelope
    {
        public new IEventMetadata Metadata
        {
            get { return (IEventMetadata) base.Metadata; }
        }

        public Object Event
        {
            get { return Message; }
        }

        public EventEnvelope(Object message, IEventMetadata metadata)
            : base(message, metadata)
        {
        }
    }
}