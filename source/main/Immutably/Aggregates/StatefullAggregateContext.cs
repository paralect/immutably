using System;
using System.Collections.Generic;
using Immutably.Data;
using Immutably.States;

namespace Immutably.Aggregates
{
    /// <summary>
    /// Represents context for statefull aggregates.
    /// </summary>
    public class StatefullAggregateContext : AggregateContextBase, IStatefullAggregateContext
    {
        /// <summary>
        /// Aggregate state
        /// </summary>
        private readonly Object _aggregateState;

        /// <summary>
        /// Current aggregate state
        /// </summary>
        public Object State
        {
            get { return _aggregateState; }
        }

        /// <summary>
        /// Creates StatefullAggregateContext
        /// </summary>
        public StatefullAggregateContext(Object state, String aggregateId, Int32 version, IDataFactory dataFactory) 
            : base(aggregateId, version, dataFactory)
        {
            if (state == null)
                throw new NullAggregateStateException();

            _aggregateState = state;
        }

        /// <summary>
        /// Reply event without tracking it in list of changes.
        /// After reply aggregate version and id will be the same as before reply.
        /// </summary>
        public void Replay(Object evnt)
        {
            ExecuteStateEventHandler(evnt);
        }

        /// <summary>
        /// Reply events without tracking them in list of changes. 
        /// After reply aggregate version and id will be the same as before reply.
        /// </summary>
        public void Replay(IEnumerable<Object> events)
        {
            foreach (var evnt in events)
                ExecuteStateEventHandler(evnt);
        }

        /// <summary>
        /// Override to exececute state event handlers
        /// </summary>
        protected override void ApplyInternal(Object evnt)
        {
            base.ApplyInternal(evnt);
            ExecuteStateEventHandler(evnt);
        }

        /// <summary>
        /// Executes state event handler for specified event
        /// </summary>
        private void ExecuteStateEventHandler(Object evnt)
        {
            new StateSpooler(State).Spool(evnt);
        }
    }
}