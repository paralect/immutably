using System;
using System.Collections.Generic;

namespace Immutably.Transitions
{
    public interface ITransitionStoreReader<TStreamId> : IDisposable
    {
        IEnumerable<ITransition<TStreamId>> Read();
    }
}