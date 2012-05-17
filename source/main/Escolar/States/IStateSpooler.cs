using System.Collections.Generic;
using Escolar.Messages;
using Paralect.Machine.Processes;

namespace Escolar.States
{
    public interface IStateSpooler
    {
        /// <summary>
        /// Replay specified events to restore state of IState.
        /// </summary>
        void Spool(IEnumerable<IEventEnvelope> events);

        IStateEnvelope StateEnvelope { get; }
    }
}