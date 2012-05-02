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
        IState Spool(IEnumerable<IEventEnvelope> events);
    }
}