using System;
using System.Collections.Generic;
using Immutably.Data;
using Immutably.Messages;

namespace Immutably.Aggregates
{
    public class AggregateContext : IAggregateContext
    {
        /// <summary>
        /// Current aggregate state
        /// </summary>
        private Object _aggregateState;

        /// <summary>
        /// Aggregate id
        /// </summary>
        private String _aggregateId;

        /// <summary>
        /// DataFactory, used to create data types, such as state and messages (events, commands)
        /// </summary>
        private IDataFactory _dataFactory;

        /// <summary>
        /// Initial version 
        /// </summary>
        private Int32 _aggregateInitialVersion;
        
        private readonly List<IEvent> _aggregateChanges = new List<IEvent>();

        public AggregateContext(Object state)
        {
            Initialize(state, "temporary_id", 0, null);
        }

        public AggregateContext(Object state, String aggregateId)
        {
            Initialize(state, aggregateId, 0, null);
        }

        public AggregateContext(Object state, String aggregateId, Int32 version)
        {
            Initialize(state, aggregateId, version, null);
        }

        public AggregateContext(Object state, String aggregateId, Int32 version, IDataFactory dataFactory)
        {
            Initialize(state, aggregateId, version, dataFactory);
        }

        private void Initialize(Object state, string aggregateId, int version, IDataFactory dataFactory)
        {
            if (version < 0)
                throw new InvalidAggregateVersionException(version);

            _dataFactory = dataFactory;
            _aggregateInitialVersion = version;

            if (aggregateId == null)
                throw new NullAggregateIdException();

            _aggregateId = aggregateId;

            if (state == null)
                throw new NullAggregateStateException();

            _aggregateState = state;
        }

        /// <summary>
        /// Current aggregate state
        /// </summary>
        public Object State
        {
            get { return _aggregateState; }
        }

        public int CurrentVersion
        {
            get { return Changed ? _aggregateInitialVersion + 1 : _aggregateInitialVersion; }
        }

        public int AggregateInitialVersion
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

        public void Reply(IEvent evnt)
        {
            ExecuteStateEventHandler(evnt);
        }

        public void Reply(IEnumerable<IEvent> events)
        {
            foreach (var evnt in events)
                ExecuteStateEventHandler(evnt);
        }

        private void ApplyInternal(IEvent evnt)
        {
            _aggregateChanges.Add(evnt);
            ExecuteStateEventHandler(evnt);
        }

        private void ExecuteStateEventHandler(IEvent evnt)
        {
            if (evnt == null)
                return;

            var methodInfo = State.GetType().GetMethod("On", new[] { evnt.GetType() });

            if (methodInfo == null)
                return;

            methodInfo.Invoke(State, new object[] { evnt });
//
//            ((dynamic) State).On((dynamic) evnt);
        }

        public TData Create<TData>()
        {
            if (_dataFactory == null)
                return Activator.CreateInstance<TData>();

            return _dataFactory.Create<TData>();
        }
    }
}