using System;
using Escolar.Messages;

namespace Escolar
{
    public static class EventsExtensions
    {
        public static IEventEnvelope ToEnvelope(this IEvent evnt, Guid entityId, Int32 version)
        {
            var eventMetadata = new EventMetadata()
            {
                SenderId = entityId,
                SenderVersion = version
            };

            return new EventEnvelope(evnt, eventMetadata);
        }
    }
}