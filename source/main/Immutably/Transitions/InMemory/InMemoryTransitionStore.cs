using System;
using System.Collections.Generic;
using System.Threading;
using Escolar.Messages;
using Escolar.Transitions;

namespace Escolar.Transitions
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
        public ITransition<TStreamId> LoadTransition<TStreamId>(TStreamId streamId, int streamSequence)
        {
            _lock.EnterReadLock();

            try
            {
                var innerStore = GetRepositoryByIdType<TStreamId>();
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
        public IList<ITransition<TStreamId>> LoadStreamTransitions<TStreamId>(TStreamId streamId, int fromStreamSequence, int count)
        {
            _lock.EnterReadLock();

            try
            {
                var innerStore = GetRepositoryByIdType<TStreamId>();
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
        public IList<ITransition<TStreamId>> LoadStreamTransitions<TStreamId>(TStreamId streamId)
        {
            _lock.EnterReadLock();

            try
            {
                var innerStore = GetRepositoryByIdType<TStreamId>();
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
        public IList<ITransition<TStreamId>> LoadStoreTransitions<TStreamId>(DateTime fromTimestamp, int count)
        {
            _lock.EnterReadLock();

            try
            {
                var innerStore = GetRepositoryByIdType<TStreamId>();
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
        internal IList<ITransition<TStreamId>> LoadStoreTransitions<TStreamId>()
        {
            _lock.EnterReadLock();

            try
            {
                var innerStore = GetRepositoryByIdType<TStreamId>();
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
        public void Append<TStreamId>(ITransition<TStreamId> transition)
        {
            _lock.EnterWriteLock();

            try
            {
                var innerStore = GetRepositoryByIdType<TStreamId>();
                innerStore.Append(transition);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }        
    
        private InMemoryTransitionRepository<TStreamId> GetRepositoryByIdType<TStreamId>()
        {
            object innerStore;
            var exists = _repositoriesByIdType.TryGetValue(typeof (TStreamId), out innerStore);

            if (!exists)
                innerStore = _repositoriesByIdType[typeof (TStreamId)] = new InMemoryTransitionRepository<TStreamId>();

            return (InMemoryTransitionRepository<TStreamId>) innerStore;
        }


        public ITransitionStreamReader<TStreamId> CreateStreamReader<TStreamId>(TStreamId streamId, Int32 fromSequence = 0)
        {
            return new InMemoryTransitionStreamReader<TStreamId>(this, streamId);
        }

        public ITransitionStreamWriter<TStreamId> CreateStreamWriter<TStreamId>(TStreamId streamId)
        {
            return new DefaultTransitionStreamWriter<TStreamId>(CreateTransitionRepository<TStreamId>(), streamId);
        }

        public ITransitionStoreReader<TStreamId> CreateStoreReader<TStreamId>()
        {
            return new InMemoryTransitionStoreReader<TStreamId>(this);
        }

        public ITransitionRepository<TStreamId> CreateTransitionRepository<TStreamId>()
        {
            return GetRepositoryByIdType<TStreamId>();
        }
    }
}