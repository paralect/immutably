using System;
using System.Collections.Generic;
using Immutably.Data;
using Immutably.Messages;

namespace Immutably.Aggregates
{
    public abstract class AggregateBase : IAggregate
    {
        /// <summary>
        /// Aggregate context
        /// </summary>
        protected IAggregateContext _context;

        /// <summary>
        /// Aggregate context
        /// </summary>
        public abstract IAggregateContext Context { get; }

        /// <summary>
        /// Current version of aggregate
        /// </summary>
        public int CurrentVersion
        {
            get { return Context.CurrentVersion; }
        }

        /// <summary>
        /// Initial version
        /// </summary>
        public int InitialVersion
        {
            get { return Context.InitialVersion; } 
        }

        /// <summary>
        /// Aggregate Id
        /// </summary>
        public String Id
        {
            get { return Context.Id; }
        }

        /// <summary>
        /// Data factory
        /// </summary>
        public IDataFactory DataFactory
        {
            get { return Context.DataFactory; }
        }

        /// <summary>
        /// Was any changes to this aggregates?
        /// </summary>
        public Boolean Changed
        {
            get { return Context.Changed; }
        }

        /// <summary>
        /// Changes to this aggregate
        /// </summary>
        public IList<IEvent> Changes
        {
            get { return Context.Changes; }
        }

        /// <summary>
        /// Apply event to aggregates
        /// </summary>
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
    }

    /// <summary>
    /// Stateless aggregate
    /// </summary>
    public abstract class Aggregate : AggregateBase, IStatelessAggregate
    {
        public override IAggregateContext Context
        {
            get
            {
                if (_context == null)
                    _context = new StatelessAggregateContext("temporary_id", 0);

                return _context;
            }
        }

        public void EstablishContext(IStatelessAggregateContext context)
        {
            if (_context != null)
                throw new AggregateContextModificationForbiddenException(GetType());

            _context = context;
        }

        public void EstablishContext(Action<AggregateContextBuilder> contextBuilder)
        {
            if (_context != null)
                throw new AggregateContextModificationForbiddenException(GetType());

            var builder = new AggregateContextBuilder();
            contextBuilder(builder);
            _context = builder.Build();
        }

    }

    /// <summary>
    /// Statefull aggregate
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    public class Aggregate<TState> : AggregateBase, IStatefullAggregate
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