using System;

namespace Immutably.Transitions
{
    public interface ITransitionStore
    {
        ITransitionStreamReader CreateStreamReader(String streamId, Int32 fromSequence = 0);
        ITransitionStreamWriter CreateStreamWriter(String streamId);
        ITransitionStoreReader CreateStoreReader();
        ITransitionRepository CreateTransitionRepository();
    }
}