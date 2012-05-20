using System;
using System.Collections.Generic;

namespace Escolar.Transitions
{
    public interface ITransitionStreamReader<TStreamId> : IDisposable
    {
        IEnumerable<ITransition<TStreamId>> Read();
    }
}