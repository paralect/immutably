using System;

namespace Paralect.Machine.Processes
{
    public interface IStateMetadata
    {
        Int32 Version { get; set; }
        Guid EntityId { get; set; }
    }
}