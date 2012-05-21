using System;
using System.Collections.Generic;

namespace Immutably.Transitions
{
    public interface ITransitionStoreReader : IDisposable
    {
        IEnumerable<ITransition> Read();
    }

    public interface ITransitionStoreReader<TStreamId> : ITransitionStoreReader
    {
        new IEnumerable<ITransition<TStreamId>> Read();
    }
}