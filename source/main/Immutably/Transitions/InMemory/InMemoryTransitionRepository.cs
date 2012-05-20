using System;
using System.Collections.Generic;
using System.Linq;

namespace Escolar.Transitions
{
    /// <summary>
    /// In memory transition repository.
    /// Not thread safe
    /// </summary>
    public class InMemoryTransitionRepository<TStreamId> : ITransitionRepository<TStreamId>
    {
        /// <summary>
        /// Inner struct for Transition id, used as key in Dictionary
        /// </summary>
        public struct TransitionId<TStreamId>
        {
            public TStreamId StreamId { get; set; }
            public Int32 StreamSequence { get; set; }

            public TransitionId(TStreamId streamId, int streamSequence)
                : this()
            {
                StreamId = streamId;
                StreamSequence = streamSequence;
            }
        }

        /// <summary>
        /// Main collection of transitions in order of addition.
        /// </summary>
        private readonly List<ITransition<TStreamId>> _transitions = new List<ITransition<TStreamId>>();

        /// <summary>
        /// Indexes
        /// </summary>
        private readonly Dictionary<TransitionId<TStreamId>, ITransition<TStreamId>> _indexByTransactionId = new Dictionary<TransitionId<TStreamId>, ITransition<TStreamId>>();
        private readonly Dictionary<TStreamId, List<ITransition<TStreamId>>> _indexByStreamId = new Dictionary<TStreamId, List<ITransition<TStreamId>>>();

        /// <summary>
        /// LoadAggregate single transition, uniquely identified by by streamId and streamSequence
        /// </summary>
        public ITransition<TStreamId> LoadTransition(TStreamId streamId, int streamSequence)
        {
            return _indexByTransactionId[new TransitionId<TStreamId>(streamId, streamSequence)];
        }

        /// <summary>
        /// LoadAggregate <param name="count" /> transitions for specified stream, 
        /// ordered by Stream Sequence, starting from <param name="fromStreamSequence" />
        /// </summary>
        public IList<ITransition<TStreamId>> LoadStreamTransitions(TStreamId streamId, int fromStreamSequence, int count)
        {
            List<ITransition<TStreamId>> transitions;
            var exists = _indexByStreamId.TryGetValue(streamId, out transitions);

            if (!exists)
                throw new TransitionStreamNotExistsException(String.Format("Stream with id [{0}] doesn't exist", streamId));

            return transitions
                .Where(t => t.StreamSequence >= fromStreamSequence)
                .Take(count)
                .ToList();
        }

        /// <summary>
        /// Returns all transitions for specified stream in chronological order
        /// </summary>
        public IList<ITransition<TStreamId>> LoadStreamTransitions(TStreamId streamId)
        {
            List<ITransition<TStreamId>> transitions;
            var exists = _indexByStreamId.TryGetValue(streamId, out transitions);

            if (!exists)
                throw new TransitionStreamNotExistsException(String.Format("Stream with id [{0}] doesn't exist", streamId));

            return transitions.AsReadOnly();
        }

        /// <summary>
        /// LoadAggregate <param name="count" /> transitions from store, 
        /// ordered by transition Timestamp, starting from <param name="fromTimestamp" />
        /// </summary>
        /// <param name="fromTimestamp">
        /// Not inclusively timestamp value
        /// </param>
        public IList<ITransition<TStreamId>> LoadStoreTransitions(DateTime fromTimestamp, int count)
        {
            return _transitions
                .Where(t => t.Timestamp > fromTimestamp)
                .Take(count)
                .ToList();
        }

        /// <summary>
        /// Returns readonly collection of all transitions in the store in chronological order
        /// </summary>
        internal IList<ITransition<TStreamId>> LoadStoreTransitions()
        {
            return _transitions.AsReadOnly();
        }

        /// <summary>
        /// Append transition
        /// </summary>
        public void Append(ITransition<TStreamId> transition)
        {
            var key = new TransitionId<TStreamId>(transition.StreamId, transition.StreamSequence);

            if (_indexByTransactionId.ContainsKey(key))
                throw new TransitionAlreadyExistsException(String.Format("Transition with id ({0}, {1}) already exists", transition.StreamId, transition.StreamSequence));

            List<ITransition<TStreamId>> stream;
            if (!_indexByStreamId.TryGetValue(transition.StreamId, out stream))
                _indexByStreamId[transition.StreamId] = stream = new List<ITransition<TStreamId>>();

            stream.Add(transition);
            _indexByTransactionId[key] = transition;
            _transitions.Add(transition);
        }        
    }
}