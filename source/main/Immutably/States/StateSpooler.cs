using System;
using System.Collections.Generic;
using Immutably.Messages;

namespace Immutably.States
{
    public class StateSpooler
    {
        /// <summary>
        /// State envelope
        /// </summary>
        private IState _state;

        private Int32 _version;

        public IState State
        {
            get { return _state; }
        }

        public int Version
        {
            get { return _version; }
        }

        /// <summary>
        /// Creates StateSpooler initialized with initial state
        /// </summary>
        public StateSpooler(IState initialState)
        {
            _state = initialState;
        }

        /// <summary>
        /// Replay specified events to restore state of IState.
        /// </summary>
        public void Spool(Int32 version, IEnumerable<IEvent> events)
        {
            if (version < _version)
                throw new InvalidOperationException("Version cannot be lower than existing");

            foreach (var evnt in events)
            {
                ExecuteStateEventHandler(evnt);
            }

            _version = version;
        }

        private void ExecuteStateEventHandler(IEvent evnt)
        {
            if (evnt == null)
                return;

            var methodInfo = State.GetType().GetMethod("On", new[] { evnt.GetType() });

            if (methodInfo == null)
                return;

            methodInfo.Invoke(State, new object[] { evnt });
        }
    }
}