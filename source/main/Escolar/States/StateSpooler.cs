using System;
using System.Collections.Generic;
using Escolar.Aggregates;
using Escolar.Messages;
using Paralect.Machine.Processes;

namespace Escolar.States
{
    public class StateSpooler : IStateSpooler
    {
        /// <summary>
        /// State envelope
        /// </summary>
        private IStateEnvelope _stateEnvelope;

        /// <summary>
        /// State envelope
        /// </summary>
        public IStateEnvelope StateEnvelope
        {
            get { return _stateEnvelope; }
        }

        /// <summary>
        /// Creates StateSpooler initialized with initial state
        /// </summary>
        public StateSpooler(IStateEnvelope initialStateEnvelope)
        {
            _stateEnvelope = initialStateEnvelope;
        }

        /// <summary>
        /// Replay specified events to restore state of IState.
        /// </summary>
        public void Spool(IEnumerable<IEventEnvelope> events)
        {
            foreach (var evnt in events)
            {
                if (evnt.Metadata.SenderId == Guid.Empty)
                    throw new NullReferenceException("Id of state cannot be null");

                if (evnt.Metadata.SenderId != _stateEnvelope.Metadata.EntityId)
                    throw new Exception("State restoration failed because of different Stream ID in the events");

                if (evnt.Metadata.StreamSequence < _stateEnvelope.Metadata.Version)
                    throw new Exception("State restoration failed because of wrong version sequence.");

                ((dynamic)_stateEnvelope.State).On((dynamic) evnt.Event);

                _stateEnvelope.Metadata.Version = evnt.Metadata.StreamSequence;
            }
        }
    }
}