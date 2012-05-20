using System;

namespace Immutably.Transitions
{
    public class TransitionStreamNotExistsException : Exception
    {
        public TransitionStreamNotExistsException(string message)
            : base(message)
        {
        }
    }
}