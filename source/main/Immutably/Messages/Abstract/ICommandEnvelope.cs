using System;

namespace Immutably.Messages
{
    public interface ICommandEnvelope : IMessageEnvelope
    {
        Object Command { get; }
        new ICommandMetadata Metadata { get; }
    }
}