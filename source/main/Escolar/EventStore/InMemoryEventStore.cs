using System;
using System.Collections.Generic;
using System.Threading;
using Escolar.Messages;
using System.Linq;

namespace Escolar.EventStore
{
    public class InMemoryEventStore : IEventStore
    {
        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();
        private readonly List<IEventEnvelope> _memory = new List<IEventEnvelope>();

        public IList<IEventEnvelope> GetById(Guid id)
        {
            _lock.EnterReadLock();

            try
            {
                return _memory
                    .Where(e => e.Metadata.SenderId == id)
                    .ToList();
            }
            finally 
            {
                _lock.ExitReadLock();
            }
        }

        public void Append(IList<IEventEnvelope> events)
        {
            _lock.EnterWriteLock();

            try
            {
                _memory.AddRange(events);
            }
            finally 
            {
                _lock.ExitWriteLock();
            }
        }
    }
}