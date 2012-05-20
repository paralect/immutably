using System;
using Paralect.Machine.Processes;

namespace Escolar.Messages
{
    public class StateEnvelope<TId> : IStateEnvelope<TId>
    {
        public IState State { get; private set; }
        public IStateMetadata<TId> Metadata { get; private set; }

        public StateEnvelope(IState state, IStateMetadata<TId> metadata)
        {
            State = state;
            Metadata = metadata;
        }
    }
}