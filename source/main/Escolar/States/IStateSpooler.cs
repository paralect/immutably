using System.Collections.Generic;
using Escolar.Messages;
using Paralect.Machine.Processes;

namespace Escolar.States
{
    public interface IStateSpooler<TStreamId>
    {
        /// <summary>
        /// Replay specified events to restore state of IState.
        /// </summary>
        void Spool(IEnumerable<IEventEnvelope<TStreamId>> events);

        IStateEnvelope<TStreamId> StateEnvelope { get; }
    }
}