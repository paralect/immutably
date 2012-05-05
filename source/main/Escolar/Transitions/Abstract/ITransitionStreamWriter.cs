using System;
using System.Collections.Generic;

namespace Escolar.Transitions
{
    public interface ITransitionStreamWriter : IDisposable 
    {
        void Write(IList<ITransition> transitions);
        void Write(params ITransition[] transitions);
    }
}