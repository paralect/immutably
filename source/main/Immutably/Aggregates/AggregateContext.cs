using System;
using System.Collections.Generic;
using Immutably.Data;
using Immutably.Messages;

namespace Immutably.Aggregates
{
    public class AggregateContext<TId, TState> : IAggregateContext
    {
        /// <summary>
        /// Current aggregate state
        /// </summary>
        private TState _aggregateState;

        /// <summary>
        /// Aggregate id
        /// </summary>
        private TId _aggregateId;

        /// <summary>
        /// DataFactory, used to create data types, such as state and messages (events, commands)
        /// </summary>
        private IDataFactory _dataFactory;

        /// <summary>
        /// Initial version 
        /// </summary>
        private Int32 _aggregateInitialVersion;
        
        private readonly List<IEvent> _aggregateChanges = new List<IEvent>();

        public AggregateContext()
        {
            Initialize(default(TId), 0, default(TState), null);
        }

        public AggregateContext(TState state)
        {
            Initialize(default(TId), 0, state, null);
        }

        public AggregateContext(TId aggregateId)
        {
            Initialize(aggregateId, 0, default(TState), null);
        }

        public AggregateContext(Int32 version)
        {
            Initialize(default(TId), version, default(TState), null);
        }

        public AggregateContext(TId aggregateId, Int32 version)
        {
            Initialize(aggregateId, version, default(TState), null);
        }

        public AggregateContext(TId aggregateId, Int32 version, TState state)
        {
            Initialize(aggregateId, version, state, null);
        }

        public AggregateContext(TId aggregateId, Int32 version, TState state, IDataFactory dataFactory)
        {
            Initialize(aggregateId, version, state, dataFactory);
        }

        private void Initialize(TId aggregateId, Int32 version, TState state, IDataFactory dataFactory)
        {
            if (version < 0)
                throw new InvalidAggregateVersionException(version);

            _dataFactory = dataFactory;
            _aggregateInitialVersion = version;

            // If TId is a value type, comparizon will be ignored. 
            // If TId is a reference type that points to null - new id will be created
            _aggregateId = (aggregateId == null) ? Activator.CreateInstance<TId>() : aggregateId;

            // If TState is a value type, comparizon will be ignored. 
            // If TState is a reference type that points to null - new state will be created
            _aggregateState = state == null ? Create<TState>() : state;
        }

        Object IAggregateContext.State
        {
            get { return State; }
        }

        /// <summary>
        /// Current aggregate state
        /// </summary>
        public TState State
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

        public TId Id
        {
            get { return _aggregateId; }
        }

        Object IAggregateContext.Id
        {
            get { return Id; }
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