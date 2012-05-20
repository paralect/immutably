using System;
using System.Collections.Generic;

namespace Immutably.Transitions
{
    public interface ITransitionStreamReader<TStreamId> : IDisposable
    {
        IEnumerable<ITransition<TStreamId>> Read();
    }
}