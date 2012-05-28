using System;
using System.Collections.Generic;
using Immutably.Data;
using Immutably.Messages;

namespace Immutably.Aggregates
{
    public class StatefullAggregateContext : AggregateContextBase, IStatefullAggregateContext
    {
        /// <summary>
        /// Current aggregate state
        /// </summary>
        private Object _aggregateState;

        /// <summary>
        /// Current aggregate state
        /// </summary>
        public Object State
        {
            get { return _aggregateState; }
        }

        public StatefullAggregateContext(Object state, String aggregateId, Int32 version, IDataFactory dataFactory) : base(aggregateId, version, dataFactory)
        {
            if (state == null)
                throw new NullAggregateStateException();

            _aggregateState = state;
        }

        private void ExecuteStateEventHandler(IEvent evnt)
        {
            if (evnt == null)
                return;

            var methodInfo = State.GetType().GetMethod("On", new[] { evnt.GetType() });

            if (methodInfo == null)
                return;

            methodInfo.Invoke(State, new object[] { evnt });
            //
            //            ((dynamic) State).On((dynamic) evnt);
        }

        public void Reply(IEvent evnt)
        {
            ExecuteStateEventHandler(evnt);
        }

        public void Reply(IEnumerable<IEvent> events)
        {
            foreach (var evnt in events)
                ExecuteStateEventHandler(evnt);
        }

        protected override void ApplyInternal(IEvent evnt)
        {
            base.ApplyInternal(evnt);
            ExecuteStateEventHandler(evnt);
        }
    }
}