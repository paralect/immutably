using System.Collections.Generic;

namespace Immutably.Transitions
{
    /// <summary>
    /// Reads transitions stream
    /// </summary>
    public class InMemoryTransitionStreamReader<TStreamId> : ITransitionStreamReader<TStreamId>
    {
        /// <summary>
        /// Transitions store
        /// </summary>
        private readonly InMemoryTransitionStore _store;

        /// <summary>
        /// Stream ID
        /// </summary>
        private readonly TStreamId _streamId;

        /// <summary>
        /// Creates DefaultTransitionStreamReader with default portion size - 1000.
        /// </summary>
        public InMemoryTransitionStreamReader(InMemoryTransitionStore store, TStreamId streamId)
        {
            _store = store;
            _streamId = streamId;
        }

        /// <summary>
        /// Reads stream by portions (default portion size 1000)
        /// </summary>
        public IEnumerable<ITransition<TStreamId>> Read()
        {
            var transitions = _store.LoadStreamTransitions<TStreamId>(_streamId);
            return new TransitionStreamOrderValidator<TStreamId>(_streamId, transitions).Read();
        }

        public void Dispose()
        {
            // nothing to dispose
        }
    }
}