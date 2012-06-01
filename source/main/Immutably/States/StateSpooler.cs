using System;
using System.Collections.Generic;

namespace Immutably.States
{
    /// <summary>
    /// Replays events to modify state in-place
    /// </summary>
    public class StateSpooler
    {
        /// <summary>
        /// State object
        /// </summary>
        private Object _state;

        /// <summary>
        /// User-defined object that can be assigned by each call to Spool method
        /// </summary>
        private Object _data;

        /// <summary>
        /// State object
        /// </summary>
        public Object State
        {
            get { return _state; }
        }

        /// <summary>
        /// User-defined object that can be assigned by each call to Spool method
        /// </summary>
        public object Data
        {
            get { return _data; }
        }

        /// <summary>
        /// Creates StateSpooler initialized with initial state
        /// </summary>
        public StateSpooler(Object initialState)
        {
            _state = initialState;
        }

        /// <summary>
        /// Replay specified event to restore state.
        /// </summary>
        public void Spool(Object evnt)
        {
            Spool(evnt, null);
        }

        /// <summary>
        /// Replay specified event to restore state and assign user-defined object that 
        /// can be accessed via Data property of this spooler.
        /// </summary>
        public void Spool(Object evnt, Object data)
        {
            ExecuteStateEventHandler(evnt);
            _data = data;
        }

        /// <summary>
        /// Replay specified events to restore state.
        /// </summary>
        public void Spool(IEnumerable<Object> events)
        {
            Spool(events, null);
        }

        /// <summary>
        /// Replay specified events to restore state and assign user-defined object that 
        /// can be accessed via Data property of this spooler.
        /// </summary>
        public void Spool(IEnumerable<Object> events, Object data)
        {
            foreach (var evnt in events)
                ExecuteStateEventHandler(evnt);

            _data = data;
        }

        /// <summary>
        /// Executes state event handler for specified event
        /// </summary>
        private void ExecuteStateEventHandler(Object evnt)
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