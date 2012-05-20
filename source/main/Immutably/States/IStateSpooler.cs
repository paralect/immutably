using System.Collections.Generic;
using Immutably.Messages;

namespace Immutably.States
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