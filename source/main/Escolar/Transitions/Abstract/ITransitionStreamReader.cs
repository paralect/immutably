using System;
using System.Collections.Generic;

namespace Escolar.Transitions
{
    public interface ITransitionStreamReader : IDisposable
    {
        IEnumerable<ITransition> Read();
    }
}