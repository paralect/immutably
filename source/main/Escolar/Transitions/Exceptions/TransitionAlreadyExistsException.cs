using System;

namespace Escolar.Transitions
{
    public class TransitionAlreadyExistsException : Exception
    {
        public TransitionAlreadyExistsException(string message) : base(message)
        {
        }
    }
}