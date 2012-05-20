using System;

namespace Immutably.Messages
{
    public interface IStateMetadata<TId>
    {
        Int32 Version { get; set; }
        TId EntityId { get; set; }
    }
}