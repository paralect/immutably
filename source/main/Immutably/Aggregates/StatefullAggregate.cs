using System;
using System.Collections.Generic;
using Immutably.Messages;

namespace Immutably.Aggregates
{
    /// <summary>
    /// Statefull aggregate
    /// </summary>
    /// <typeparam name="TState"></typeparam>
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

        public void Reply(IEvent evnt)
        {
            ((IStatefullAggregateContext)Context).Reply(evnt);
        }

        public void Reply(IEnumerable<IEvent> events)
        {
            ((IStatefullAggregateContext)Context).Reply(events);
        }

        /// <summary>
        /// Current aggregate state
        /// </summary>
        public TState State
        {
            get { return (TState) ((IStatefullAggregateContext)Context).State; }
        }

        public override IAggregateContext Context
        {
            get
            {
                if (_context == null)
                    _context = new StatefullAggregateContext(Activator.CreateInstance<TState>(), "temporary_id", 0, null);

                return (IStatefullAggregateContext)_context;
            }
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