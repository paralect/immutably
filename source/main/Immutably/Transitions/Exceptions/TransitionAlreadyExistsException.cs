using System;

namespace Immutably.Transitions
{
    public class TransitionAlreadyExistsException : Exception
    {
        public TransitionAlreadyExistsException(string message) : base(message)
        {
        }
    }
}