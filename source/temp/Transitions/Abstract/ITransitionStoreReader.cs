using System;
using System.Collections.Generic;

namespace Immutably.Transitions
{
    public interface ITransitionStoreReader : IDisposable
    {
        IEnumerable<ITransition> ReadAll();
    }
}