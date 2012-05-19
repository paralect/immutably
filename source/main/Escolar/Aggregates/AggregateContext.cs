using System;
using System.Collections.Generic;
using Escolar.Messages;
using Paralect.Machine.Processes;

namespace Escolar.Aggregates
{
    public class AggregateContext<TAggregateId> : IAggregateContext<TAggregateId>
    {
        /// <summary>
        /// Factory
        /// </summary>
        private readonly IEscolarFactory _factory;
        
        /// <summary>
        /// Current aggregate state
        /// </summary>
        private readonly IStateEnvelope<TAggregateId> _stateEnvelope;

        /// <summary>
        /// List of applied events
        /// </summary>
        private readonly List<IEventEnvelope<TAggregateId>> _changes = new List<IEventEnvelope<TAggregateId>>();

        /// <summary>
        /// Current aggregate state
        /// </summary>
        public IState State
        {
            get { return _stateEnvelope.State; }
        }

        /// <summary>
        /// Current aggregate state metadata
        /// </summary>
        public IStateMetadata<TAggregateId> StateMetadata
        {
            get { return _stateEnvelope.Metadata;  }
        }

        /// <summary>
        /// List applied changes
        /// </summary>
        public IList<IEventEnvelope<TAggregateId>> Changes
        {
            get { return _changes.AsReadOnly(); }
        }

        /// <summary>
        /// Escolar factory
        /// </summary>
        public IEscolarFactory Factory
        {
            get { return _factory; }
        }

        public AggregateContext(IEscolarFactory factory, IStateEnvelope<TAggregateId> stateEnvelope)
        {
            _factory = factory;
            _stateEnvelope = stateEnvelope;
        }

        public void Apply(IEvent evnt)
        {
            var env = new EventEnvelope<TAggregateId>(evnt, _factory.Create<IEventMetadata<TAggregateId>>(m =>
            {
                m.SenderId = default(TAggregateId);
            }));

            _changes.Add(env);
        }
    }
}