using System;
using System.Collections.Generic;

namespace Escolar.Transitions
{
    public interface ITransitionStreamWriter : IDisposable 
    {
        void Write(ITransition transition);
    }
}