using System;

namespace Escolar.Transitions
{
    public class TransitionAlreadyExistException : Exception
    {
        public TransitionAlreadyExistException(string message) : base(message)
        {
        }
    }
}