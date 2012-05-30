using System;
using System.Collections.Generic;
using System.Threading;

namespace Immutably.Transitions
{
    /// <summary>
    /// In memory transition store.
    /// Thread safe.
    /// </summary>
    public class InMemoryTransitionStore : ITransitionStore
    {
        private InMemoryTransitionRepository _repository;

        /// <summary>
        /// Synchronization for concurrent reads and exclusive writes
        /// </summary>
        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();

        public InMemoryTransitionStore()
        {
            _repository = new InMemoryTransitionRepository();
        }

        /// <summary>
        /// LoadAggregate single transition, uniquely identified by by streamId and streamVersion
        /// </summary>
        public ITransition LoadStreamTransition(String streamId, int streamVersion)
        {
            _lock.EnterReadLock();

            try
            {
                return _repository.LoadStreamTransition(streamId, streamVersion);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        /// <summary>
        /// Load last transition in the stream
        /// </summary>
        public ITransition LoadLastStreamTransition(String streamId)
        {
            _lock.EnterReadLock();

            try
            {
                return _repository.LoadLastStreamTransition(streamId);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        /// <summary>
        /// LoadAggregate <param name="count" /> transitions for specified stream, 
        /// ordered by Stream Sequence, starting from <param name="fromStreamVersion" />
        /// </summary>
        public IList<ITransition> LoadStreamTransitions(String streamId, int fromStreamVersion, int count)
        {
            _lock.EnterReadLock();

            try
            {
                return _repository.LoadStreamTransitions(streamId, fromStreamVersion, count);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        /// <summary>
        /// Returns all transitions for specified stream in chronological order
        /// </summary>
        public IList<ITransition> LoadStreamTransitions(String streamId)
        {
            _lock.EnterReadLock();

            try
            {
                return _repository.LoadStreamTransitions(streamId);
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
                return _repository.LoadStoreTransitions(fromTimestamp, count);
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
                return _repository.LoadStoreTransitions();
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
                _repository.Append(transition);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }


        public ITransitionStreamReader CreateStreamReader(String streamId, Int32 fromSequence = 0)
        {
            return new InMemoryTransitionStreamReader(this, streamId);
        }

        public ITransitionStreamWriter CreateStreamWriter(String streamId)
        {
            return new DefaultTransitionStreamWriter(CreateTransitionRepository(), streamId);
        }

        public ITransitionStoreReader CreateStoreReader()
        {
            return new InMemoryTransitionStoreReader(this);
        }

        public ITransitionRepository CreateTransitionRepository()
        {
            return _repository;
        }
    }
}