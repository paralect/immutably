using System;
using System.Collections.Generic;

namespace Immutably.Aggregates
{
    /// <summary>
    /// Aggregate with state
    /// </summary>
    public class StatefullAggregate<TState> : AggregateBase, IStatefullAggregate
    {
        /// <summary>
        /// Current aggregate state
        /// </summary>
        public TState State
        {
            get { return (TState)Context.State; }
        }


        /// <summary>
        /// Current aggregate state 
        /// (explicit interface implementation)
        /// </summary>
        Object IStatefullAggregate.State
        {
            get { return State; }
        }

        /// <summary>
        /// Aggregate context
        /// </summary>
        public new IStatefullAggregateContext Context
        {
            get { return (IStatefullAggregateContext)base.Context; }
        }
        
        /// <summary>
        /// Reply events without tracking them in list of changes. 
        /// After reply aggregate version and id will be the same as before reply.
        /// </summary>
        public void Replay(Object evnt)
        {
            Context.Replay(evnt);
        }

        /// <summary>
        /// Reply events without tracking them in list of changes. 
        /// After reply aggregate version and id will be the same as before reply.
        /// </summary>
        public void Replay(IEnumerable<Object> events)
        {
            Context.Replay(events);
        }

        /// <summary>
        /// Template method override to create statefull aggregate context
        /// </summary>
        protected override IAggregateContext CreateAggregateContext()
        {
            return new StatefullAggregateContext(Activator.CreateInstance<TState>(), "temporary_id", 0, null);
        }

        /// <summary>
        /// Establish context for this statefull aggregate
        /// You can establish context only one time. Subsequent calls to this method will 
        /// lead to AggregateContextModificationForbiddenException exception.
        /// </summary>
        public void EstablishContext(IStatefullAggregateContext context)
        {
            if (_context != null)
                throw new AggregateContextModificationForbiddenException(GetType());

            _context = context;
        }

        /// <summary>
        /// Establish context for this statefull aggregate via context builder
        /// You can establish context only one time. Subsequent calls to this method will 
        /// lead to AggregateContextModificationForbiddenException exception.
        /// </summary>
        public void EstablishContext(Action<StatefullAggregateContextBuilder<TState>> contextBuilder)
        {
            if (_context != null)
                throw new AggregateContextModificationForbiddenException(GetType());

            var builder = new StatefullAggregateContextBuilder<TState>();
            contextBuilder(builder);
            _context = builder.Build();
        }
    }
}