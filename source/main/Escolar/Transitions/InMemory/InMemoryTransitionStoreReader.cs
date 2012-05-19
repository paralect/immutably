using System;
using System.Collections.Generic;

namespace Escolar.Transitions
{
    /// <summary>
    /// Reads all transitions in store in chronological order
    /// </summary>
    public class InMemoryTransitionStoreReader<TStreamId> : ITransitionStoreReader<TStreamId>
    {
        /// <summary>
        /// Transition store
        /// </summary>
        private readonly InMemoryTransitionStore<TStreamId> _store;

        /// <summary>
        /// Creates InMemoryTransitionStoreReader
        /// </summary>
        public InMemoryTransitionStoreReader(InMemoryTransitionStore<TStreamId> store)
        {
            _store = store;
        }

        /// <summary>
        /// Reads stream by portions (default portion size 1000)
        /// </summary>
        public IEnumerable<ITransition<TStreamId>> Read()
        {
            return _store.LoadStoreTransitions();
        }

        public void Dispose()
        {
            // nothing to dispose
        }
    }
}