using System;

namespace Escolar.Transitions
{
    public interface ITransitionStore
    {
        ITransitionStreamReader CreateStreamReader(Guid streamId, Int32 fromSequence = 0);

        ITransitionStreamWriter CreateStreamWriter(Guid streamId);

        ITransitionStoreReader CreateStoreReader();

        ITransitionRepository CreateTransitionRepository();
    }
}