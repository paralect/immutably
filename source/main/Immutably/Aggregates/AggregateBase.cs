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
        public IAggregateContext Context 
        {
            get
            {
                if (_context == null)
                    _context = CreateAggregateContext();

                return _context;
            } 
        }

        /// <summary>
        /// Template method to create aggregate context
        /// </summary>
        protected abstract IAggregateContext CreateAggregateContext();

        /// <summary>
        /// Current version of aggregate
        /// </summary>
        public int CurrentVersion
        {
            get { return Context.CurrentVersion; }
        }

        /// <summary>
        /// Initial version of aggregate
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
        /// DataFactory, used to create data types, such as state and messages (events, commands)
        /// </summary>
        public IDataFactory DataFactory
        {
            get { return Context.DataFactory; }
        }

        /// <summary>
        /// Returns true, if aggregate was changed (new events was applied)
        /// </summary>
        public Boolean Changed
        {
            get { return Context.Changed; }
        }

        /// <summary>
        /// Gets indexed enumerable of changes (applied events)
        /// </summary>
        public IIndexedEnumerable<IEvent> Changes
        {
            get { return Context.Changes; }
        }

        /// <summary>
        /// Apply event to list of changes and execute state event handler
        /// </summary>        
        public void Apply(IEvent evnt)
        {
            Context.Apply(evnt);
        }

        /// <summary>
        /// Apply event factory to list of changes and execute state event handler.
        /// Event will be created immediately, so you shouldn't worry about 
        /// possible change of closure members. 
        /// </summary>
        public void Apply<TEvent>(Action<TEvent> evntBuilder)
            where TEvent : IEvent
        {
            Context.Apply(evntBuilder);
        }

        /// <summary>
        /// Creates TData with the help of IDataFactory (if available)
        /// and via Activator (if data factory not available)
        /// </summary>
        protected TData Create<TData>()
        {
            return Context.Create<TData>();
        }
    }
}