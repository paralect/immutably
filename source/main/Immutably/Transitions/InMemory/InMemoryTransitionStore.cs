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
        private readonly Dictionary<Type, Object> _repositoriesByIdType = new Dictionary<Type, object>();

        /// <summary>
        /// Synchronization for concurrent reads and exclusive writes
        /// </summary>
        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();

        /// <summary>
        /// LoadAggregate single transition, uniquely identified by by streamId and streamSequence
        /// </summary>
        public ITransition LoadTransition(String streamId, int streamSequence)
        {
            _lock.EnterReadLock();

            try
            {
                var innerStore = GetRepositoryByIdType();
                return innerStore.LoadTransition(streamId, streamSequence);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        /// <summary>
        /// LoadAggregate <param name="count" /> transitions for specified stream, 
        /// ordered by Stream Sequence, starting from <param name="fromStreamSequence" />
        /// </summary>
        public IList<ITransition> LoadStreamTransitions<TStreamId>(String streamId, int fromStreamSequence, int count)
        {
            _lock.EnterReadLock();

            try
            {
                var innerStore = GetRepositoryByIdType();
                return innerStore.LoadStreamTransitions(streamId, fromStreamSequence, count);
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
                var innerStore = GetRepositoryByIdType();
                return innerStore.LoadStreamTransitions(streamId);
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
        public IList<ITransition> LoadStoreTransitions<TStreamId>(DateTime fromTimestamp, int count)
        {
            _lock.EnterReadLock();

            try
            {
                var innerStore = GetRepositoryByIdType();
                return innerStore.LoadStoreTransitions(fromTimestamp, count);
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
                var innerStore = GetRepositoryByIdType();
                return innerStore.LoadStoreTransitions();
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        /// <summary>
        /// Append transition
        /// </summary>
        public void Append<TStreamId>(ITransition transition)
        {
            _lock.EnterWriteLock();

            try
            {
                var innerStore = GetRepositoryByIdType();
                innerStore.Append(transition);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }        
    
        private InMemoryTransitionRepository GetRepositoryByIdType()
        {
            object innerStore;
            var exists = _repositoriesByIdType.TryGetValue(typeof (String), out innerStore);

            if (!exists)
                innerStore = _repositoriesByIdType[typeof (String)] = new InMemoryTransitionRepository();

            return (InMemoryTransitionRepository) innerStore;
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
            return GetRepositoryByIdType();
        }
    }
}