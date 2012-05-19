using System;
using System.Collections.Generic;
using System.Linq;

namespace Escolar.Transitions
{
    /// <summary>
    /// Reads stream transitions by portions (default portion size is 1000)
    /// </summary>
    public class InMemoryTransitionStreamReader<TStreamId> : ITransitionStreamReader<TStreamId>
    {
        /// <summary>
        /// Transitions store
        /// </summary>
        private readonly InMemoryTransitionStore<TStreamId> _store;

        /// <summary>
        /// Stream ID
        /// </summary>
        private readonly TStreamId _streamId;

        /// <summary>
        /// Creates DefaultTransitionStreamReader with default portion size - 1000.
        /// </summary>
        public InMemoryTransitionStreamReader(InMemoryTransitionStore<TStreamId> store, TStreamId streamId)
        {
            _store = store;
            _streamId = streamId;
        }

        /// <summary>
        /// Reads stream by portions (default portion size 1000)
        /// </summary>
        public IEnumerable<ITransition<TStreamId>> Read()
        {
            var transitions = _store.LoadStreamTransitions(_streamId);
            return new TransitionStreamOrderValidator<TStreamId>(_streamId, transitions).Read();
        }

        public void Dispose()
        {
            // nothing to dispose
        }
    }
}