using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Escolar.Messages;
using Escolar.Transitions;

namespace Escolar.Transitions
{
    public struct TransitionId
    {
        public Guid StreamId { get; set; }
        public Int32 Version { get; set; }
    }

    public class InMemoryTransitionStore : ITransitionStore
    {
        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();

        private readonly Dictionary<TransitionId, ITransition> _indexByTransactionId = new Dictionary<TransitionId, ITransition>();
        private readonly Dictionary<Guid, List<ITransition>>   _indexByStreamId = new Dictionary<Guid, List<ITransition>>();

        private readonly List<ITransition> _transitions = new List<ITransition>();

        internal IList<ITransition> GetById(Guid id)
        {
            _lock.EnterReadLock();

            try
            {
                return _indexByStreamId[id];
            }
            finally
            {
                _lock.ExitReadLock();
            }            
        }

        internal void Append(ITransition transition)
        {
            _lock.EnterWriteLock();

            try
            {
                var key = new TransitionId()
                {
                    StreamId = transition.StreamId,
                    Version = transition.Version
                };

                if (_indexByTransactionId.ContainsKey(key))
                    throw new Exception(String.Format("Transition with id ({0}, {1}) already exists", transition.StreamId, transition.Version));

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

        public ITransitionStreamReader CreateStreamReader(Guid streamId)
        {
            return new InMemoryTransitionStreamReader(new InMemoryTransitionRepository(this), streamId);
        }

        public ITransitionStreamWriter CreateStreamWriter(Guid streamId)
        {
            return new InMemoryTransitionStreamWriter(new InMemoryTransitionRepository(this), streamId);
        }

        public ITransitionStoreReader CreateStoreReader()
        {
            throw new NotImplementedException();
        }
    }
}