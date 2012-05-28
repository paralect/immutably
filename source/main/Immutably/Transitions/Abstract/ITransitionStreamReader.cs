using System;
using System.Collections.Generic;

namespace Immutably.Transitions
{
    public interface ITransitionStreamReader : IDisposable
    {
        IEnumerable<ITransition> ReadAll();
        ITransition ReadLast();
    }
}