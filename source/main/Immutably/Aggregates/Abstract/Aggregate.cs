using System;
using System.Collections.Generic;
using Immutably.Data;
using Immutably.Messages;

namespace Immutably.Aggregates
{
    public class Aggregate<TState> : IAggregate
        where TState : IState
    {
        /// <summary>
        /// Current aggregate state
        /// </summary>
        private AggregateContext _context;

        public AggregateContext Context
        {
            get
            {
                if (_context == null)
                    _context = new AggregateContext(Activator.CreateInstance<TState>(), "temporary_id", 0);

                return _context;
            }
        }

        /// <summary>
        /// Current aggregate state
        /// </summary>
        IState IAggregate.State 
        {
            get { return State; }
        }

        /// <summary>
        /// Current aggregate state
        /// </summary>
        public TState State
        {
            get { return (TState) Context.State; }
        }

        public int CurrentVersion
        {
            get { return Context.CurrentVersion; }
        }

        public int InitialVersion
        {
            get { return Context.AggregateInitialVersion; } 
        }

        public String Id
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

            _context = (AggregateContext) context;
        }

        public void EstablishContext(Action<AggregateContextBuilder<TState>> contextBuilder)
        {
            if (_context != null)
                throw new AggregateContextModificationForbiddenException(GetType());

            var builder = new AggregateContextBuilder<TState>();
            contextBuilder(builder);
            _context = builder.Build();
        }
    }
}