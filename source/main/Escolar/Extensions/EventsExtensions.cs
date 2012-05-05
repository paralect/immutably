using System;
using Escolar.Messages;

namespace Escolar
{
    public static class EventsExtensions
    {
        public static IEventEnvelope ToEnvelope(this IEvent evnt, Guid entityId, Int32 streamSequence)
        {
            var eventMetadata = new EventMetadata()
            {
                SenderId = entityId,
                StreamSequence = streamSequence
            };

            return new EventEnvelope(evnt, eventMetadata);
        }
    }
}