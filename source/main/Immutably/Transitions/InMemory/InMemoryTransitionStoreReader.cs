using System.Collections.Generic;

namespace Immutably.Transitions
{
    /// <summary>
    /// Reads all transitions in store in chronological order
    /// </summary>
    public class InMemoryTransitionStoreReader<TStreamId> : ITransitionStoreReader<TStreamId>
    {
        /// <summary>
        /// Transition store
        /// </summary>
        private readonly InMemoryTransitionStore _store;

        /// <summary>
        /// Creates InMemoryTransitionStoreReader
        /// </summary>
        public InMemoryTransitionStoreReader(InMemoryTransitionStore store)
        {
            _store = store;
        }

        /// <summary>
        /// Reads stream by portions (default portion size 1000)
        /// </summary>
        public IEnumerable<ITransition<TStreamId>> Read()
        {
            return _store.LoadStoreTransitions<TStreamId>();
        }

        public void Dispose()
        {
            // nothing to dispose
        }

        IEnumerable<ITransition> ITransitionStoreReader.Read()
        {
            return _store.LoadStoreTransitions<TStreamId>();
        }
    }
}