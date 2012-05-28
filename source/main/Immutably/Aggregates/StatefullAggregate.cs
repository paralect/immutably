using System;
using System.Collections.Generic;
using Immutably.Messages;

namespace Immutably.Aggregates
{
    /// <summary>
    /// Aggregate with state
    /// </summary>
    public class StatefullAggregate<TState> : AggregateBase, IStatefullAggregate
        where TState : IState
    {
        /// <summary>
        /// Current aggregate state
        /// </summary>
        IState IStatefullAggregate.State
        {
            get { return State; }
        }

        /// <summary>
        /// Current aggregate state
        /// </summary>
        public TState State
        {
            get { return (TState)((IStatefullAggregateContext)Context).State; }
        }

        public void Reply(IEvent evnt)
        {
            ((IStatefullAggregateContext)Context).Reply(evnt);
        }

        public void Reply(IEnumerable<IEvent> events)
        {
            ((IStatefullAggregateContext)Context).Reply(events);
        }

        public new IStatefullAggregateContext Context
        {
            get
            {
                return (IStatefullAggregateContext) base.Context;
            }
        }

        protected override IAggregateContext CreateAggregateContext()
        {
            return new StatefullAggregateContext(Activator.CreateInstance<TState>(), "temporary_id", 0, null);
        }

        public void EstablishContext(IStatefullAggregateContext context)
        {
            if (_context != null)
                throw new AggregateContextModificationForbiddenException(GetType());

            _context = (IStatefullAggregateContext)context;
        }

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