using System;
using System.Collections.Generic;

namespace Escolar.Transitions
{
    public interface ITransitionStoreReader : IDisposable
    {
        IEnumerable<ITransition> Read();
    }
}