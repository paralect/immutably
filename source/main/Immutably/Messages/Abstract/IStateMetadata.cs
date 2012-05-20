using System;

namespace Paralect.Machine.Processes
{
    public interface IStateMetadata<TId>
    {
        Int32 Version { get; set; }
        TId EntityId { get; set; }
    }
}