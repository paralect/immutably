using System;
using System.Collections.Generic;
using Immutably.Data;
using Immutably.Messages;

namespace Immutably.Aggregates
{
    /// <summary>
    /// Represents the base class for aggregate contexts
    /// </summary>
    public abstract class AggregateContextBase : IAggregateContext
    {
        /// <summary>
        /// Aggregate id
        /// </summary>
        private String _aggregateId;

        /// <summary>
        /// DataFactory, used to create data types, such as state and messages (events, commands)
        /// </summary>
        private IDataFactory _dataFactory;

        /// <summary>
        /// Initial version of Aggregate before any operation executed 
        /// </summary>
        private Int32 _aggregateInitialVersion;
        
        /// <summary>
        /// List of changes (applied events)
        /// </summary>
        private readonly List<IEvent> _aggregateChanges = new List<IEvent>();

        public AggregateContextBase(String aggregateId, Int32 version, IDataFactory dataFactory)
        {
            if (version < 0)
                throw new InvalidAggregateVersionException(version);

            _dataFactory = dataFactory;
            _aggregateInitialVersion = version;

            if (aggregateId == null)
                throw new NullAggregateIdException();

            _aggregateId = aggregateId;
        }

        public int CurrentVersion
        {
            get { return Changed ? _aggregateInitialVersion + 1 : _aggregateInitialVersion; }
        }

        public int InitialVersion
        {
            get { return _aggregateInitialVersion; } 
        }

        public String Id
        {
            get { return _aggregateId; }
        }

        public IDataFactory DataFactory
        {
            get { return _dataFactory; }
        }

        public Boolean Changed
        {
            get 
            {   
                return _aggregateChanges != null 
                    && _aggregateChanges.Count > 0; 
            }
        }

        public IList<IEvent> Changes
        {
            get { return _aggregateChanges.AsReadOnly(); }
        }

        public void Apply(IEvent evnt)
        {
            ApplyInternal(evnt);
        }

        public void Apply<TEvent>(Action<TEvent> evntBuilder)
            where TEvent : IEvent
        {
            var evnt = Create<TEvent>();
            evntBuilder(evnt);
            ApplyInternal(evnt);
        }

        protected virtual void ApplyInternal(IEvent evnt)
        {
            _aggregateChanges.Add(evnt);
        }

        public TData Create<TData>()
        {
            if (_dataFactory == null)
                return Activator.CreateInstance<TData>();

            return _dataFactory.Create<TData>();
        }
    }
}