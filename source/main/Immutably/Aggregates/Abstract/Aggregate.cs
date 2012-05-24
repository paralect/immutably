using System;
using System.Collections.Generic;
using Immutably.Data;
using Immutably.Messages;

namespace Immutably.Aggregates
{
    public class Aggregate : IStatelessAggregate
    {
        /// <summary>
        /// Current aggregate state
        /// </summary>
        protected IAggregateContext _context;

        public IAggregateContext Context
        {
            get
            {
                if (_context == null)
                    _context = new StatelessAggregateContext("temporary_id", 0);

                return _context;
            }
        }

        public int CurrentVersion
        {
            get { return Context.CurrentVersion; }
        }

        public int InitialVersion
        {
            get { return Context.InitialVersion; } 
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

        protected TData Create<TData>()
        {
            return Context.Create<TData>();
        }

        public void EstablishContext(IStatelessAggregateContext context)
        {
            if (_context != null)
                throw new AggregateContextModificationForbiddenException(GetType());

            _context = context;
        }

/*        public void EstablishContext(Action<AggregateContextBuilder<TState>> contextBuilder)
        {
            if (_context != null)
                throw new AggregateContextModificationForbiddenException(GetType());

            var builder = new AggregateContextBuilder<TState>();
            contextBuilder(builder);
            _context = builder.Build();
        }*/
    }

    public class Aggregate<TState> : Aggregate, IStatefullAggregate
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
            Context.Reply(evnt);
        }

        public void Reply(IEnumerable<IEvent> events)
        {
            Context.Reply(events);
        }

        /// <summary>
        /// Current aggregate state
        /// </summary>
        public TState State
        {
            get { return (TState) Context.State; }
        }

        public IStatefullAggregateContext Context
        {
            get
            {
                if (_context == null)
                    _context = new StatefullAggregateContext(Activator.CreateInstance<TState>(), "temporary_id", 0);

                return (IStatefullAggregateContext)_context;
            }
        }

        public void EstablishContext(IStatefullAggregateContext context)
        {
            if (_context != null)
                throw new AggregateContextModificationForbiddenException(GetType());

            _context = (IStatefullAggregateContext)context;
        }
    }
}