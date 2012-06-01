using System;
using System.Collections.Generic;
using Immutably.Data;
using Immutably.Utilities;

namespace Immutably.Aggregates
{
    /// <summary>
    /// Represents the base class for aggregate contexts
    /// </summary>
    public abstract class AggregateContextBase : IAggregateContext
    {
        /// <summary>
        /// Aggregate ID
        /// </summary>
        private readonly String _aggregateId;

        /// <summary>
        /// DataFactory, used to create data types, such as state and messages (events, commands)
        /// </summary>
        private readonly IDataFactory _dataFactory;

        /// <summary>
        /// Initial version of Aggregate before any operation executed 
        /// </summary>
        private readonly Int32 _aggregateInitialVersion;
        
        /// <summary>
        /// List of changes (applied events)
        /// </summary>
        private readonly List<Object> _aggregateChanges = new List<Object>();

        /// <summary>
        /// Current version of Aggregate
        /// </summary>
        public int CurrentVersion
        {
            get { return Changed ? _aggregateInitialVersion + 1 : _aggregateInitialVersion; }
        }

        /// <summary>
        /// Initial version of aggregate
        /// </summary>
        public int InitialVersion
        {
            get { return _aggregateInitialVersion; } 
        }

        /// <summary>
        /// Aggregate ID
        /// </summary>
        public String Id
        {
            get { return _aggregateId; }
        }

        /// <summary>
        /// DataFactory, used to create data types, such as state and messages (events, commands)
        /// </summary>
        public IDataFactory DataFactory
        {
            get { return _dataFactory; }
        }

        /// <summary>
        /// Returns true, if aggregate was changed (new events was applied)
        /// </summary>
        public Boolean Changed
        {
            get 
            {   
                return _aggregateChanges != null 
                    && _aggregateChanges.Count > 0; 
            }
        }

        /// <summary>
        /// Gets indexed enumerable of changes (applied events)
        /// </summary>
        public IIndexedEnumerable<Object> Changes
        {
            get { return _aggregateChanges.AsIndexedEnumerable(); }
        }

        /// <summary>
        /// Initializes AggregateContextBase
        /// </summary>
        protected AggregateContextBase(String aggregateId, Int32 version, IDataFactory dataFactory)
        {
            if (version < 0)
                throw new InvalidAggregateVersionException(version);

            _dataFactory = dataFactory;
            _aggregateInitialVersion = version;

            if (aggregateId == null)
                throw new NullAggregateIdException();

            _aggregateId = aggregateId;
        }

        /// <summary>
        /// Apply event to list of changes
        /// </summary>
        /// <param name="evnt"></param>
        public void Apply(Object evnt)
        {
            ApplyInternal(evnt);
        }

        /// <summary>
        /// Apply event factory to list of changes.
        /// Event will be created immediately, so you shouldn't worry about possible change of 
        /// closure members. 
        /// </summary>
        public void Apply<TEvent>(Action<TEvent> evntBuilder)
        {
            var evnt = Create<TEvent>();
            evntBuilder(evnt);
            ApplyInternal(evnt);
        }

        /// <summary>
        /// Can be ovewritten in delivered types if you need special logic that handle
        /// new applied event.
        /// </summary>
        protected virtual void ApplyInternal(Object evnt)
        {
            _aggregateChanges.Add(evnt);
        }

        /// <summary>
        /// Creates TData with the help of IDataFactory (if available)
        /// and via Activator (if data factory not available)
        /// </summary>
        public TData Create<TData>()
        {
            if (_dataFactory == null)
                return Activator.CreateInstance<TData>();

            return _dataFactory.Create<TData>();
        }
    }
}