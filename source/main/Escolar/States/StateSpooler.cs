using System;
using System.Collections.Generic;
using Escolar.Aggregates;
using Escolar.Messages;
using Paralect.Machine.Processes;

namespace Escolar.States
{
    public class StateSpooler : IStateSpooler
    {
        private readonly IAggregate _aggregate;
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

        public StateSpooler(IAggregate aggregate, IStateEnvelope initialStateEnvelope)
        {
            _aggregate = aggregate;
            _state = initialStateEnvelope.State;
            _id = initialStateEnvelope.Metadata.EntityId;
            _version = initialStateEnvelope.Metadata.Version;
        }

        /// <summary>
        /// Replay specified events to restore state of IState.
        /// </summary>
        public IStateEnvelope Spool(IEnumerable<IEventEnvelope> events)
        {
            Int32 lastEventVersion = 0;

            foreach (var evnt in events)
            {
                var id = evnt.Metadata.SenderId;
                var version = evnt.Metadata.StreamSequence;
                lastEventVersion = version;

                if (id == Guid.Empty)
                    throw new NullReferenceException("Id of state cannot be null");

                if (id != _id)
                    throw new Exception("State restoration failed because of different Process ID in the events");

                if (version <= _version)
                    throw new Exception("State restoration failed because of wrong version sequence.");

                //_aggregate.On(evnt.Event);
            }

            var stateMetadata = new StateMetadata(_id, lastEventVersion);
            var stateEnvelope = new StateEnvelope(_state, stateMetadata);

            return stateEnvelope;
        }

        /*
        public void Replay(IEnumerable<IEventEnvelope> eventEnvelopes)
        {
            if (_state == null || _stateMetadata == null)
                throw new Exception("Aggregate initial state wasn't specified");

            var currentId = _stateMetadata.EntityId;
            var currentStreamSequence = _stateMetadata.Version;

            foreach (var eventEnvelope in eventEnvelopes)
            {
                var id = eventEnvelope.Metadata.SenderId;
                var streamSequence = eventEnvelope.Metadata.StreamSequence;

                if (id == Guid.Empty)
                    throw new NullReferenceException("Id of state cannot be null");

                if (id != currentId)
                    throw new Exception("State restoration failed because of different Entity ID in the event metadata");

                if (streamSequence < currentStreamSequence)
                    throw new Exception("State restoration failed because of out of order stream sequence.");

                ((dynamic)this).On((dynamic)eventEnvelope.Event);

                currentStreamSequence = streamSequence;
            }

            // Metadata now has updated version
            _stateMetadata = new StateMetadata(currentId, currentStreamSequence);
        }*/
    }
}