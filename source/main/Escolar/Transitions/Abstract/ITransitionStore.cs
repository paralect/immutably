using System;

namespace Escolar.Transitions
{
    public interface ITransitionStore<TStreamId>
    {
        ITransitionStreamReader<TStreamId> CreateStreamReader(TStreamId streamId, Int32 fromSequence = 0);

        ITransitionStreamWriter<TStreamId> CreateStreamWriter(TStreamId streamId);

        ITransitionStoreReader<TStreamId> CreateStoreReader();

        ITransitionRepository<TStreamId> CreateTransitionRepository();
    }
}