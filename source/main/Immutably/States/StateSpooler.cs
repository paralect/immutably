using System;
using System.Collections.Generic;
using Immutably.Messages;

namespace Immutably.States
{
    public class StateSpooler<TStreamId> : IStateSpooler<TStreamId>
    {
        /// <summary>
        /// State envelope
        /// </summary>
        private IStateEnvelope<TStreamId> _stateEnvelope;

        /// <summary>
        /// State envelope
        /// </summary>
        public IStateEnvelope<TStreamId> StateEnvelope
        {
            get { return _stateEnvelope; }
        }

        /// <summary>
        /// Creates StateSpooler initialized with initial state
        /// </summary>
        public StateSpooler(IStateEnvelope<TStreamId> initialStateEnvelope)
        {
            _stateEnvelope = initialStateEnvelope;
        }

        /// <summary>
        /// Replay specified events to restore state of IState.
        /// </summary>
        public void Spool(IEnumerable<IEventEnvelope<TStreamId>> events)
        {
            foreach (var evnt in events)
            {
                if (EqualityComparer<TStreamId>.Default.Equals(evnt.Metadata.SenderId, default(TStreamId)))
                    throw new NullReferenceException("Id of state cannot be null or default(T) for value types");

                if (!EqualityComparer<TStreamId>.Default.Equals(evnt.Metadata.SenderId, _stateEnvelope.Metadata.EntityId))
                    throw new Exception("State restoration failed because of different Stream ID in the events");

                if (evnt.Metadata.StreamSequence < _stateEnvelope.Metadata.Version)
                    throw new Exception("State restoration failed because of wrong version sequence.");

                ((dynamic)_stateEnvelope.State).On((dynamic) evnt.Event);

                _stateEnvelope.Metadata.Version = evnt.Metadata.StreamSequence;
            }
        }
    }
}