using System;
using Immutably.Data;
using Immutably.Messages;
using Immutably.Utilities;

namespace Immutably.Aggregates
{
    /// <summary>
    /// Represents the base class for all aggregates
    /// </summary>
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
        public IIndexedEnumerable<IEvent> Changes
        {
            get { return Context.Changes.AsIndexedEnumerable(); }
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
}