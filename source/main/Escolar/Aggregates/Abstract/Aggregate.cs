using System;
using System.Collections.Generic;
using Escolar.Messages;
using Escolar.States;
using Escolar.Transitions;
using Paralect.Machine.Processes;

namespace Escolar.Aggregates
{
    public class Aggregate<TState> : IAggregate
        where TState : IState
    {
        /// <summary>
        /// Aggregate context
        /// </summary>
        protected IAggregateContext _context;

        /// <summary>
        /// Current aggregate state
        /// </summary>
        public TState State
        {
            get { return (TState) _context.State; }
        }

        /// <summary>
        /// Current state metadata
        /// </summary>
        public IStateMetadata StateMetadata
        {
            get { return _context.StateMetadata; }
        }

        /// <summary>
        /// Aggregate initialization
        /// </summary>
        public void Initialize(IAggregateContext context)
        {
            _context = context;
        }

        public void Apply(IEvent evnt)
        {
            _context.Apply(evnt);
        }

        public void Apply<TEvent>(Action<TEvent> evntBuilder)
        {
            
        }
    }
}