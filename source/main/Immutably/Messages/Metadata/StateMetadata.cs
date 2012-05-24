using System;

namespace Immutably.Messages
{
    public class StateMetadata : IStateMetadata
    {
        public int Version { get; set; }
        public String EntityId { get; set; }

        public StateMetadata(String entityId, int version)
        {
            EntityId = entityId;
            Version = version;
        }
    }
}