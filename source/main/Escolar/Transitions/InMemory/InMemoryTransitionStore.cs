using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Escolar.Messages;
using Escolar.Transitions;

namespace Escolar.Transitions
{
    public class InMemoryTransitionStore : ITransitionStore, ITransitionRepository
    {
        /// <summary>
        /// Inner struct for Transition id, used as key in Dictionary
        /// </summary>
        public struct TransitionId
        {
            public Guid StreamId { get; set; }
            public Int32 StreamSequence { get; set; }

            public TransitionId(Guid streamId, int streamSequence) : this()
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
        private readonly Dictionary<Guid, List<ITransition>>   _indexByStreamId = new Dictionary<Guid, List<ITransition>>();

        /// <summary>
        /// Synchronization for concurrent reads and exclusive writes
        /// </summary>
        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();

        /// <summary>
        /// Load single transition, uniquely identified by by streamId and streamSequence
        /// </summary>
        public ITransition LoadTransition(Guid streamId, int streamSequence)
        {
            _lock.EnterReadLock();

            try
            {
                return _indexByTransactionId[new TransitionId(streamId, streamSequence)];
            }
            finally
            {
                _lock.ExitReadLock();
            }               
        }

        /// <summary>
        /// Load <param name="count" /> transitions for specified stream, 
        /// ordered by Stream Sequence, starting from <param name="fromStreamSequence" />
        /// </summary>
        public IList<ITransition> LoadStreamTransitions(Guid streamId, int fromStreamSequence, int count)
        {
            _lock.EnterReadLock();

            try
            {
                return _indexByStreamId[streamId]
                    .Where(t => t.StreamSequence >= fromStreamSequence)
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
        /// </summary>
        public IList<ITransition> LoadStreamTransitions(Guid streamId)
        {
            _lock.EnterReadLock();

            try
            {
                return _indexByStreamId[streamId].AsReadOnly();
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        /// <summary>
        /// Load <param name="count" /> transitions from store, 
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
        internal IList<ITransition> LoadStoreTransitions()
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
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public ITransitionStreamReader CreateStreamReader(Guid streamId, Int32 fromSequence = 0)
        {
            return new TransitionStreamReaderValidatorDecorator(
                new InMemoryTransitionStreamReader(this, streamId),
                streamId
            );
        }

        public ITransitionStreamWriter CreateStreamWriter(Guid streamId)
        {
            return new DefaultTransitionStreamWriter(CreateTransitionRepository(), streamId);
        }

        public ITransitionStoreReader CreateStoreReader()
        {
            return new InMemoryTransitionStoreReader(this);
        }

        public ITransitionRepository CreateTransitionRepository()
        {
            return this;
        }
    }
}