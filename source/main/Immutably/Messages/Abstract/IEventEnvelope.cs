using System;

namespace Immutably.Messages
{
    public interface IEventEnvelope : IMessageEnvelope
    {
        Object Event { get; }
        new IEventMetadata Metadata { get; }
    }
}