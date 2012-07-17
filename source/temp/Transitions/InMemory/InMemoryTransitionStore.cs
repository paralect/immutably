using System;
using Immutably.Data;

namespace Immutably.Transitions
{
    /// <summary>
    /// In memory transition store.
    /// Thread safe.
    /// </summary>
    public class InMemoryTransitionStore : ITransitionStore
    {
        /// <summary>
        /// DataFactory, used to create data types, such as state and messages (events, commands)
        /// </summary>
        private readonly IDataFactory _dataFactory;

        /// <summary>
        /// DataFactory, used to create data types, such as state and messages (events, commands)
        /// </summary>
        public IDataFactory DataFactory
        {
            get { return _dataFactory; }
        }

        /// <summary>
        /// For in-memory mode we use only one instance of repository
        /// </summary>
        private InMemoryTransitionRepository _repository;

        public InMemoryTransitionStore(IDataFactory dataFactory)
        {
            _dataFactory = dataFactory;
            _repository = new InMemoryTransitionRepository();
        }

        public ITransitionStreamReader CreateStreamReader(String streamId, Int32 fromSequence = 0)
        {
            return new InMemoryTransitionStreamReader(this, streamId);
        }

        public ITransitionStreamWriter CreateStreamWriter(String streamId)
        {
            return new InMemoryTransitionStreamWriter(this, streamId);
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