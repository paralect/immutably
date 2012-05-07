using System;

namespace Escolar.Transitions
{
    public interface ITransitionStore
    {
        ITransitionStreamReader CreateStreamReader(Guid streamId);

        ITransitionStreamWriter CreateStreamWriter(Guid streamId);

        ITransitionStoreReader CreateStoreReader();

        ITransitionRepository CreateTransitionRepository();
    }
}