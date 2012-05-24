using System;
using System.Collections.Generic;
using System.Linq;

namespace Immutably.Transitions
{
    /// <summary>
    /// In memory transition repository.
    /// Not thread safe
    /// </summary>
    public class InMemoryTransitionRepository : ITransitionRepository
    {
        /// <summary>
        /// Inner struct for Transition id, used as key in Dictionary
        /// </summary>
        public struct TransitionId
        {
            public String StreamId { get; set; }
            public Int32 StreamSequence { get; set; }

            public TransitionId(String streamId, int streamSequence)
                : this()
            {
                StreamId = streamId;
                StreamSequence = streamSequence;
            }
        }

        /// <summary>
        /// Main collection of transitions in order of addition.
        /// </summary>
        private readonly List<ITransition> _transitions = new List<ITransition>();

        /// <summary>
        /// Indexes
        /// </summary>
        private readonly Dictionary<TransitionId, ITransition> _indexByTransactionId = new Dictionary<TransitionId, ITransition>();
        private readonly Dictionary<String, List<ITransition>> _indexByStreamId = new Dictionary<String, List<ITransition>>();

        /// <summary>
        /// LoadAggregate single transition, uniquely identified by by streamId and streamSequence
        /// </summary>
        public ITransition LoadTransition(String streamId, int streamSequence)
        {
            return _indexByTransactionId[new TransitionId(streamId, streamSequence)];
        }

        /// <summary>
        /// LoadAggregate <param name="count" /> transitions for specified stream, 
        /// ordered by Stream Sequence, starting from <param name="fromStreamSequence" />
        /// </summary>
        public IList<ITransition> LoadStreamTransitions(String streamId, int fromStreamSequence, int count)
        {
            List<ITransition> transitions;
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
        public IList<ITransition> LoadStreamTransitions(String streamId)
        {
            List<ITransition> transitions;
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
        public IList<ITransition> LoadStoreTransitions(DateTime fromTimestamp, int count)
        {
            return _transitions
                .Where(t => t.Timestamp > fromTimestamp)
                .Take(count)
                .ToList();
        }

        /// <summary>
        /// Returns readonly collection of all transitions in the store in chronological order
        /// </summary>
        internal IList<ITransition> LoadStoreTransitions()
        {
            return _transitions.AsReadOnly();
        }

        /// <summary>
        /// Append transition
        /// </summary>
        public void Append(ITransition transition)
        {
            var key = new TransitionId(transition.StreamId, transition.StreamSequence);

            if (_indexByTransactionId.ContainsKey(key))
                throw new TransitionAlreadyExistsException(String.Format("Transition with id ({0}, {1}) already exists", transition.StreamId, transition.StreamSequence));

            List<ITransition> stream;
            if (!_indexByStreamId.TryGetValue(transition.StreamId, out stream))
                _indexByStreamId[transition.StreamId] = stream = new List<ITransition>();

            stream.Add(transition);
            _indexByTransactionId[key] = transition;
            _transitions.Add(transition);
        }        
    }
}