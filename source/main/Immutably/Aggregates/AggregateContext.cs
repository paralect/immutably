using System;
using System.Collections.Generic;
using Immutably.Data;
using Immutably.Messages;

namespace Immutably.Aggregates
{
    public abstract class AggregateContext : IAggregateContext
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
        /// Initial version 
        /// </summary>
        private Int32 _aggregateInitialVersion;
        
        private readonly List<IEvent> _aggregateChanges = new List<IEvent>();

        public AggregateContext()
        {
            Initialize("temporary_id", 0, null);
        }

        public AggregateContext(String aggregateId)
        {
            Initialize(aggregateId, 0, null);
        }

        public AggregateContext(String aggregateId, Int32 version)
        {
            Initialize(aggregateId, version, null);
        }

        public AggregateContext(String aggregateId, Int32 version, IDataFactory dataFactory)
        {
            Initialize(aggregateId, version, dataFactory);
        }

        private void Initialize(string aggregateId, int version, IDataFactory dataFactory)
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

    public class StatelessAggregateContext : AggregateContext, IStatelessAggregateContext
    {
        public StatelessAggregateContext() : base()
        {
        }

        public StatelessAggregateContext(String aggregateId) : base(aggregateId)
        {
        }

        public StatelessAggregateContext(String aggregateId, Int32 version) : base(aggregateId, version)
        {
        }

        public StatelessAggregateContext(String aggregateId, Int32 version, IDataFactory dataFactory) : base(aggregateId, version, dataFactory)
        {
        }        
    }

    public class StatefullAggregateContext : AggregateContext, IStatefullAggregateContext
    {
        /// <summary>
        /// Current aggregate state
        /// </summary>
        private Object _aggregateState;

        /// <summary>
        /// Current aggregate state
        /// </summary>
        public Object State
        {
            get { return _aggregateState; }
        }

        public StatefullAggregateContext(Object state)
        {
            Initialize(state);
        }

        public StatefullAggregateContext(Object state, String aggregateId) : base(aggregateId)
        {
            Initialize(state);
        }

        public StatefullAggregateContext(Object state, String aggregateId, Int32 version) : base(aggregateId, version)
        {
            Initialize(state);
        }

        public StatefullAggregateContext(Object state, String aggregateId, Int32 version, IDataFactory dataFactory) : base(aggregateId, version, dataFactory)
        {
            Initialize(state);
        }

        private void Initialize(Object state)
        {
            if (state == null)
                throw new NullAggregateStateException();

            _aggregateState = state;
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

        public void Reply(IEvent evnt)
        {
            ExecuteStateEventHandler(evnt);
        }

        public void Reply(IEnumerable<IEvent> events)
        {
            foreach (var evnt in events)
                ExecuteStateEventHandler(evnt);
        }

        protected override void ApplyInternal(IEvent evnt)
        {
            base.ApplyInternal(evnt);
            ExecuteStateEventHandler(evnt);
        }
    }
}