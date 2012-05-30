using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

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
            public Int32 StreamVersion { get; set; }

            public TransitionId(String streamId, int streamVersion)
                : this()
            {
                StreamId = streamId;
                StreamVersion = streamVersion;
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
        /// Synchronization for concurrent reads and exclusive writes
        /// </summary>
        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();

        /// <summary>
        /// LoadAggregate single transition, uniquely identified by by streamId and streamVersion
        /// Throws TransitionNotExistsException if no such transition found.
        /// </summary>
        public ITransition LoadStreamTransition(String streamId, int streamVersion)
        {
            _lock.EnterReadLock();

            try
            {
                ITransition transition;
                if (!_indexByTransactionId.TryGetValue(new TransitionId(streamId, streamVersion), out transition))
                    throw new TransitionNotExistsException(streamId, streamVersion);

                return transition;
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        /// <summary>
        /// Load last transition in the stream
        /// Throws TransitionStreamNotExistsException if stream not exists.
        /// </summary>
        public ITransition LoadLastStreamTransition(String streamId)
        {
            _lock.EnterReadLock();

            try
            {
                List<ITransition> transitions;
                var exists = _indexByStreamId.TryGetValue(streamId, out transitions);

                if (!exists || transitions.Count == 0)
                    throw new TransitionStreamNotExistsException(streamId);

                return transitions.Last();
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        /// <summary>
        /// LoadAggregate <param name="count" /> transitions for specified stream, 
        /// ordered by Stream Sequence, starting from <param name="fromStreamVersion" />
        /// Throws TransitionStreamNotExistsException if stream not exists
        /// </summary>
        public IList<ITransition> LoadStreamTransitions(String streamId, int fromStreamVersion, int count)
        {
            _lock.EnterReadLock();

            try
            {
                List<ITransition> transitions;
                var exists = _indexByStreamId.TryGetValue(streamId, out transitions);

                if (!exists)
                    throw new TransitionStreamNotExistsException(streamId);

                return transitions
                    .Where(t => t.StreamVersion >= fromStreamVersion)
                    .Take(count)
                    .ToList();
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        /// <summary>
        /// Returns all transitions for specified stream in chronological order
        /// Throws TransitionStreamNotExistsException if stream not exists
        /// </summary>
        public IList<ITransition> LoadStreamTransitions(String streamId)
        {
            _lock.EnterReadLock();

            try
            {
                List<ITransition> transitions;
                if (!_indexByStreamId.TryGetValue(streamId, out transitions))
                    throw new TransitionStreamNotExistsException(streamId);

                return transitions;
            }
            finally
            {
                _lock.ExitReadLock();
            }
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
            _lock.EnterReadLock();

            try
            {
                return _transitions
                    .Where(t => t.Timestamp > fromTimestamp)
                    .Take(count)
                    .ToList();
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        /// <summary>
        /// Returns readonly collection of all transitions in the store in chronological order
        /// </summary>
        public IList<ITransition> LoadStoreTransitions()
        {
            _lock.EnterReadLock();

            try
            {
                return _transitions.AsReadOnly();
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        /// <summary>
        /// Append transition
        /// </summary>
        public void Append(ITransition transition)
        {
            _lock.EnterWriteLock();

            try
            {
                var key = new TransitionId(transition.StreamId, transition.StreamVersion);

                if (_indexByTransactionId.ContainsKey(key))
                    throw new TransitionAlreadyExistsException(transition.StreamId, transition.StreamVersion);

                List<ITransition> stream;
                if (!_indexByStreamId.TryGetValue(transition.StreamId, out stream))
                    _indexByStreamId[transition.StreamId] = stream = new List<ITransition>();

                stream.Add(transition);
                _indexByTransactionId[key] = transition;
                _transitions.Add(transition);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }        
    }
}