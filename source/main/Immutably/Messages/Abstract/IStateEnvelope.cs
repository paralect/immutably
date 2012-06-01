using System;

namespace Immutably.Messages
{
    public interface IStateEnvelope
    {
        Object State { get; }
        IStateMetadata Metadata { get; }
    }

}