using System;

namespace Escolar.Transitions
{
    public class TransitionStreamNotExistsException : Exception
    {
        public TransitionStreamNotExistsException(string message)
            : base(message)
        {
        }
    }
}