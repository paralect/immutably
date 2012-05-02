using System;
using Paralect.Machine.Processes;

namespace Escolar.Messages
{
    public class StateMetadata : IStateMetadata
    {
        public int Version { get; set; }
        public Guid EntityId { get; set; }
    }
}