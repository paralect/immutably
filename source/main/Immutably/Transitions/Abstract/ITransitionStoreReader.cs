using System;
using System.Collections.Generic;

namespace Escolar.Transitions
{
    public interface ITransitionStoreReader<TStreamId> : IDisposable
    {
        IEnumerable<ITransition<TStreamId>> Read();
    }
}