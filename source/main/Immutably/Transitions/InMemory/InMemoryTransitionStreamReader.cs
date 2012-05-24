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
        public IEnumerable<ITransition> Read()
        {
            var transitions = _store.LoadStreamTransitions(_streamId);
            return new TransitionStreamOrderValidator(_streamId, transitions).Read();
        }

        IEnumerable<ITransition> ITransitionStreamReader.Read()
        {
            return Read();
        }

        public void Dispose()
        {
            // nothing to dispose
        }
    }
}