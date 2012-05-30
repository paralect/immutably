using System;
using Immutably.Data;

namespace Immutably.Transitions
{
    public interface ITransitionStore
    {
        /// <summary>
        /// DataFactory, used to create data types, such as state and messages (events, commands)
        /// </summary>
        IDataFactory DataFactory { get; }

        ITransitionStreamReader CreateStreamReader(String streamId, Int32 fromSequence = 0);
        ITransitionStreamWriter CreateStreamWriter(String streamId);
        ITransitionStoreReader CreateStoreReader();
        ITransitionRepository CreateTransitionRepository();

    }
}