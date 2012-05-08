using System;
using System.Collections.Generic;
using Escolar.Messages;
using Escolar.States;
using Paralect.Machine.Processes;

namespace Escolar.Aggregates
{
    public class Aggregate<TState> : IAggregate
        where TState : IState
    {
        /// <summary>
        /// Current aggregate state
        /// </summary>
        protected IState _state;

        /// <summary>
        /// Current state metadata
        /// </summary>
        protected IStateMetadata _stateMetadata;

        /// <summary>
        /// Current aggregate state
        /// </summary>
        public TState State
        {
            get { return (TState)_state; }
        }

        /// <summary>
        /// Current state metadata
        /// </summary>
        public IStateMetadata StateMetadata
        {
            get { return _stateMetadata; }
        }

        /// <summary>
        /// Envelope of state and state metadata 
        /// </summary>
        public IStateEnvelope StateEnvelope
        {
            get { return new StateEnvelope(_state, _stateMetadata); }
        }


        /// <summary>
        /// Aggregate changes changes
        /// </summary>
        private readonly Action<IEventEnvelope> _changes;

        public void Initialize(IStateEnvelope stateEnvelope)
        {
            _state = stateEnvelope.State;
            _stateMetadata = stateEnvelope.Metadata;
        }

        public void Apply(IEventEnvelope eventEnvelope)
        {
            _changes(eventEnvelope);
            ExecuteEventHandler(eventEnvelope);
        }

        public void Replay(IEnumerable<IEventEnvelope> eventEnvelopes)
        {
            if (_state == null || _stateMetadata == null)
                throw new Exception("Aggregate initial state wasn't specified");

            foreach (var eventEnvelope in eventEnvelopes)
            {
                ExecuteEventHandler(eventEnvelope);

                // Metadata now has updated stream sequence
                _stateMetadata.Version = eventEnvelope.Metadata.StreamSequence;
            }
        }

        public void Apply(IEvent evnt)
        {
            var eventMetadata = new EventMetadata
            {
                SenderId = _stateMetadata.EntityId,
                StreamSequence = _stateMetadata.Version + 1
            };

            Action<IEventMetadata> metadata = m =>
            {
                m.SenderId = _stateMetadata.EntityId;
                m.StreamSequence = _stateMetadata.Version + 1;
            };

            var metadata2 = Create<IEventMetadata>(m =>
            {
                m.SenderId = _stateMetadata.EntityId;
                m.StreamSequence = _stateMetadata.Version + 1;
            });

            var metadata3 = Create<IEventMetadata>(null);
            metadata3.SenderId = _stateMetadata.EntityId;
            metadata3.StreamSequence = _stateMetadata.Version + 1;

            var eventEnvelope = new EventEnvelope(evnt, eventMetadata);

            Apply(eventEnvelope);
        }

        private void ExecuteEventHandler(IEventEnvelope eventEnvelope)
        {
            ((dynamic)this).On((dynamic) eventEnvelope.Event);
        }

        private TObj Create<TObj>(Action<TObj> modifier)
        {

            return (TObj) (Object) this;
        }
    }
}