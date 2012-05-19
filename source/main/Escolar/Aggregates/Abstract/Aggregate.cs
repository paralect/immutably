using System;
using System.Collections.Generic;
using Escolar.Data;
using Escolar.Messages;
using Escolar.States;
using Escolar.Transitions;
using Paralect.Machine.Processes;

namespace Escolar.Aggregates
{
    public class Aggregate<TId, TState> : IAggregate<TId>
        where TState : IState
    {
        /// <summary>
        /// Current aggregate state
        /// </summary>
        private TState _state;

        private IDataFactory _dataFactory;

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
                if (_state == null)
                    _state = Create<TState>();

                return _state;                
            }
            set { _state = value; }
        }

        public int CurrentVersion
        {
            get { return Changed ? _initialVersion + 1 : _initialVersion;  }
        }

        public int InitialVersion
        {
            get { return _initialVersion; } 
            set { _initialVersion = value; }
        }

        public TId Id { get; set; }

        public IDataFactory DataFactory
        {
            get { return _dataFactory; }
            set { _dataFactory = value; }
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

        private TData Create<TData>()
        {
            if (_dataFactory == null)
                return Activator.CreateInstance<TData>();

            return _dataFactory.Create<TData>();
        }

        public void Initialize(IAggregateContext<TId> factory)
        {
            throw new NotImplementedException();
        }
    }
}