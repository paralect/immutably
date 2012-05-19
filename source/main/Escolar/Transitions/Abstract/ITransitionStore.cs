using System;

namespace Escolar.Transitions
{
    public interface ITransitionStore
    {
        ITransitionStreamReader<TStreamId> CreateStreamReader<TStreamId>(TStreamId streamId, Int32 fromSequence = 0);

        ITransitionStreamWriter<TStreamId> CreateStreamWriter<TStreamId>(TStreamId streamId);

        ITransitionStoreReader<TStreamId> CreateStoreReader<TStreamId>();

        ITransitionRepository<TStreamId> CreateTransitionRepository<TStreamId>();
    }
}