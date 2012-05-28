using System;
using System.Collections.Generic;
using Immutably.Data;
using Immutably.Messages;
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
        public void Replay(IEvent evnt)
        {
            ExecuteStateEventHandler(evnt);
        }

        /// <summary>
        /// Reply events without tracking them in list of changes. 
        /// After reply aggregate version and id will be the same as before reply.
        /// </summary>
        public void Replay(IEnumerable<IEvent> events)
        {
            foreach (var evnt in events)
                ExecuteStateEventHandler(evnt);
        }

        /// <summary>
        /// Override to exececute state event handlers
        /// </summary>
        protected override void ApplyInternal(IEvent evnt)
        {
            base.ApplyInternal(evnt);
            ExecuteStateEventHandler(evnt);
        }

        /// <summary>
        /// Executes state event handler for specified event
        /// </summary>
        private void ExecuteStateEventHandler(IEvent evnt)
        {
            new StateSpooler(State).Spool(evnt);
        }
    }
}