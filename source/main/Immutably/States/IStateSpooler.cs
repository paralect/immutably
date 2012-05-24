using System.Collections.Generic;
using Immutably.Messages;

namespace Immutably.States
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