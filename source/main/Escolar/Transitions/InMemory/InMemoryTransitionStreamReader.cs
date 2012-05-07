using System;
using System.Collections.Generic;
using System.Linq;

namespace Escolar.Transitions
{
    /// <summary>
    /// Reads stream transitions by portions (default portion size is 1000)
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
        private readonly Guid _streamId;

        /// <summary>
        /// Creates DefaultTransitionStreamReader with default portion size - 1000.
        /// </summary>
        public InMemoryTransitionStreamReader(InMemoryTransitionStore store, Guid streamId)
        {
            _store = store;
            _streamId = streamId;
        }

        /// <summary>
        /// Reads stream by portions (default portion size 1000)
        /// </summary>
        public IEnumerable<ITransition> Read()
        {
            return _store.LoadStreamTransitions(_streamId);
        }

        public void Dispose()
        {
            // nothing to dispose
        }
    }
}