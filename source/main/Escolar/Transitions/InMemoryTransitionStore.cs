using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Escolar.Messages;
using Escolar.Transitions;

namespace Escolar.Transitions
{
    public class InMemoryTransitionStore : ITransitionStore
    {
        private readonly ITransitionStore _store;
        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();
        private readonly List<ITransition> _transitions = new List<ITransition>();

        internal IList<ITransition> GetById(Guid id)
        {
            _lock.EnterReadLock();

            try
            {
                return _transitions
                    .Where(e => e.EntityId == id)
                    .ToList();
            }
            finally
            {
                _lock.ExitReadLock();
            }            
        }

        internal void Append(IList<ITransition> transitions)
        {
            _lock.EnterWriteLock();

            try
            {
                _transitions.AddRange(transitions);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public ITransitionSession OpenSession()
        {
            return new InMemoryTransitionSession(this);
        }
    }
}