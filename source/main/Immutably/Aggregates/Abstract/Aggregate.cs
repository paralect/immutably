using System;
using System.Collections.Generic;
using Immutably.Data;
using Immutably.Messages;

namespace Immutably.Aggregates
{
    public class Aggregate<TId, TState> : IAggregate<TId>
        where TState : IState
    {
        /// <summary>
        /// Current aggregate state
        /// </summary>
        private AggregateContext<TId, TState> _context;

        public AggregateContext<TId, TState> Context
        {
            get
            {
                if (_context == null)
                    _context = new AggregateContext<TId, TState>();

                return _context;
            }
        }

        /// <summary>
        /// Current aggregate state
        /// </summary>
        IState IAggregate<TId>.State 
        {
            get { return State; }
        }

        /// <summary>
        /// Current aggregate state
        /// </summary>
        public TState State
        {
            get { return Context.State; }
        }

        public int CurrentVersion
        {
            get { return Context.CurrentVersion; }
        }

        public int InitialVersion
        {
            get { return Context.AggregateInitialVersion; } 
        }

        public TId Id
        {
            get { return Context.Id; }
        }

        public IDataFactory DataFactory
        {
            get { return Context.DataFactory; }
        }

        public Boolean Changed
        {
            get { return Context.Changed; }
        }

        public IList<IEvent> Changes
        {
            get { return Context.Changes; }
        }

        public void Apply(IEvent evnt)
        {
            Context.Apply(evnt);
        }

        public void Apply<TEvent>(Action<TEvent> evntBuilder)
            where TEvent : IEvent
        {
            Context.Apply(evntBuilder);
        }

        public void Reply(IEvent evnt)
        {
            Context.Reply(evnt);
        }

        public void Reply(IEnumerable<IEvent> events)
        {
            Context.Reply(events);
        }

        protected TData Create<TData>()
        {
            return Context.Create<TData>();
        }

        public void EstablishContext(IAggregateContext context)
        {
            if (_context != null)
                throw new AggregateContextModificationForbiddenException(GetType());

            _context = (AggregateContext<TId, TState>) context;
        }

        public void EstablishContext(AggregateContext<TId, TState> context)
        {
            if (_context != null)
                throw new AggregateContextModificationForbiddenException(GetType());

            _context = context;
        }

        public void EstablishContext(Action<AggregateContextBuilder<TId, TState>> contextBuilder)
        {
            if (_context != null)
                throw new AggregateContextModificationForbiddenException(GetType());

            var builder = new AggregateContextBuilder<TId, TState>();
            contextBuilder(builder);
            _context = builder.Build();
        }
    }
}