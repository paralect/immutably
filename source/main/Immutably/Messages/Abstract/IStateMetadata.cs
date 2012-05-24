using System;

namespace Immutably.Messages
{
    public interface IStateMetadata
    {
        Int32 Version { get; set; }
        String EntityId { get; set; }
    }
}