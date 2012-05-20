using System;
using System.Collections.Generic;
using Immutably.Data;
using Immutably.Messages;

namespace Immutably.Aggregates
{
    public class Aggregate<TId, TState> : IAggregate<TId>
        where TState : IState
    {
        /// <summary>
        /// Current aggregate state
        /// </summary>
        private TState _state;

        /// <summary>
        /// Aggregate id
        /// </summary>
        private TId _id;

        /// <summary>
        /// DataFactory, used to create data types, such as state and messages (events, commands)
        /// </summary>
        private IDataFactory _dataFactory;

        /// <summary>
        /// Initial version 
        /// </summary>
        private Int32 _initialVersion;
        
        private readonly List<IEvent> _changes = new List<IEvent>();

        /// <summary>
        /// Current aggregate state
        /// </summary>
        IState IAggregate<TId>.State 
        {
            get { return State; }
            set { State = (TState) value; }
        }

        /// <summary>
        /// Current aggregate state
        /// </summary>
        public TState State
        {
            get
            {
                if (EqualityComparer<TState>.Default.Equals(_state, default(TState)))
                    _state = Create<TState>();

                return _state;                
            }
            set
            {
                if (Changed)
                    throw new AggregateContextModificationDeniedException("Modification of State forbidden, because of already applied changes to Aggregate");

                _state = value;
            }
        }

        public int CurrentVersion
        {
            get
            {
                return Changed ? _initialVersion + 1 : _initialVersion;
            }
            set
            {
                if (Changed)
                    throw new AggregateContextModificationDeniedException("Modification of State forbidden, because of already applied changes to Aggregate");

                _initialVersion = value;
            }
        }

        public int InitialVersion
        {
            get { return _initialVersion; } 
        }

        public TId Id
        {
            get { return _id; }
            set
            {
                if (Changed)
                    throw new AggregateContextModificationDeniedException("Modification of Id forbidden, because of already applied changes to Aggregate");

                _id = value;
            }
        }

        public IDataFactory DataFactory
        {
            get { return _dataFactory; }
            set
            {
                if (Changed)
                    throw new AggregateContextModificationDeniedException("Modification of DataFactory forbidden, because of already applied changes to Aggregate");

                _dataFactory = value;
            }
        }

        public Boolean Changed
        {
            get 
            {   
                return _changes != null 
                    && _changes.Count > 0; 
            }
        }

        public IList<IEvent> Changes
        {
            get { return _changes.AsReadOnly(); }
        }

        public Aggregate()
        {

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
            _changes.Add(evnt);
            ExecuteStateEventHandler(evnt);
        }

        private void ExecuteStateEventHandler(IEvent evnt)
        {
            ((dynamic) State).On((dynamic) evnt);
        }

        protected TData Create<TData>()
        {
            if (_dataFactory == null)
                return Activator.CreateInstance<TData>();

            return _dataFactory.Create<TData>();
        }
    }
}