using System;
using System.Collections.Generic;

namespace Immutably.Transitions
{
    /// <summary>
    /// Reads transitions stream
    /// </summary>
    public class InMemoryTransitionStreamReader : ITransitionStreamReader
    {
        /// <summary>
        /// Transitions store
        /// </summary>
        private readonly InMemoryTransitionStore _store;

        /// <summary>
        /// Stream ID
        /// </summary>
        private readonly String _streamId;

        /// <summary>
        /// Creates DefaultTransitionStreamReader with default portion size - 1000.
        /// </summary>
        public InMemoryTransitionStreamReader(InMemoryTransitionStore store, String streamId)
        {
            _store = store;
            _streamId = streamId;
        }

        /// <summary>
        /// Reads stream by portions (default portion size 1000)
        /// </summary>
        public IEnumerable<ITransition> ReadAll()
        {
            var transitions = _store.LoadStreamTransitions(_streamId);
            return new TransitionStreamOrderValidator(_streamId, transitions).Read();
        }

        /// <summary>
        /// Load last transition in the stream
        /// </summary>
        public ITransition ReadLast()
        {
            return _store.LoadLastStreamTransition(_streamId);
        }

        IEnumerable<ITransition> ITransitionStreamReader.ReadAll()
        {
            return ReadAll();
        }

        public void Dispose()
        {
            // nothing to dispose
        }
    }
}