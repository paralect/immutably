using System;
using System.Collections.Generic;
using Escolar.Messages;
using Paralect.Machine.Processes;

namespace Escolar.Aggregates
{
    public class AggregateContext : IAggregateContext
    {
        /// <summary>
        /// Factory
        /// </summary>
        private readonly IEscolarFactory _factory;
        
        /// <summary>
        /// Current aggregate state
        /// </summary>
        private readonly IStateEnvelope _stateEnvelope;

        /// <summary>
        /// List of applied events
        /// </summary>
        private readonly List<IEventEnvelope> _changes = new List<IEventEnvelope>();

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
        public IStateMetadata StateMetadata
        {
            get { return _stateEnvelope.Metadata;  }
        }

        /// <summary>
        /// List applied changes
        /// </summary>
        public IList<IEventEnvelope> Changes
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

        public AggregateContext(IEscolarFactory factory, IStateEnvelope stateEnvelope)
        {
            _factory = factory;
            _stateEnvelope = stateEnvelope;
        }

        public void Apply(IEvent evnt)
        {
            var env = new EventEnvelope(evnt, _factory.Create<IEventMetadata>(m =>
            {
                m.SenderId = Guid.Empty;
            }));

            _changes.Add(env);
        }
    }
}