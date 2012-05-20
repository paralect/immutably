using System;
using Paralect.Machine.Processes;

namespace Escolar.Messages
{
    public class StateMetadata<TId> : IStateMetadata<TId>
    {
        public int Version { get; set; }
        public TId EntityId { get; set; }

        public StateMetadata(TId entityId, int version)
        {
            EntityId = entityId;
            Version = version;
        }
    }
}