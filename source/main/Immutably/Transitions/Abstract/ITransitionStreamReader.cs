using System;
using System.Collections.Generic;

namespace Immutably.Transitions
{
    public interface ITransitionStreamReader : IDisposable
    {
        IEnumerable<ITransition> Read();
    }

    public interface ITransitionStreamReader<TStreamId> : ITransitionStreamReader
    {
        new IEnumerable<ITransition<TStreamId>> Read();
    }
}