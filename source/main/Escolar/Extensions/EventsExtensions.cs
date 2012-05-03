using System;
using Escolar.Messages;

namespace Escolar
{
    public static class EventsExtensions
    {
        public static IEventEnvelope ToEnvelope(this IEvent evnt, Guid entityId)
        {
            var eventMetadata = new EventMetadata()
            {
                SenderId = entityId,
                SenderVersion = 1
            };

            return new EventEnvelope(evnt, eventMetadata);
        }
    }
}