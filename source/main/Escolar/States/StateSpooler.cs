using System;
using System.Collections.Generic;
using Escolar.Messages;
using Paralect.Machine.Processes;

namespace Escolar.States
{
    public class StateSpooler : IStateSpooler
    {
        private readonly IState _state;

        /// <summary>
        /// Aggregate ID
        /// </summary>
        private readonly Guid _id;

        /// <summary>
        /// StreamSequence of Aggregate Root.
        /// Can be used to support commutativity of events.
        /// </summary>
        private readonly Int32 _version;

        public StateSpooler(IStateEnvelope initialStateEnvelope)
        {
            _state = initialStateEnvelope.State;
            _id = initialStateEnvelope.Metadata.EntityId;
            _version = initialStateEnvelope.Metadata.Version;
        }

        /// <summary>
        /// Replay specified events to restore state of IState.
        /// </summary>
        public IState Spool(IEnumerable<IEventEnvelope> events)
        {
            foreach (var evnt in events)
            {
                var id = evnt.Metadata.SenderId;
                var version = evnt.Metadata.StreamSequence;

                if (_id == Guid.Empty)
                    throw new NullReferenceException("Id of state cannot be null");

                if (id != _id)
                    throw new Exception("State restoration failed because of different Process ID in the events");

                if (version <= _version)
                    throw new Exception("State restoration failed because of wrong version sequence.");

                _state.Apply(evnt.Event);
            }

            return _state;
        }         
    }
}