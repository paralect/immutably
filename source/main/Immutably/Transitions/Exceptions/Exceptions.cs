using System;

namespace Immutably.Transitions
{
    /// <summary>
    /// Base class for all transition-related exceptions
    /// </summary>
    public class TransitionException : Exception
    {
        public TransitionException()  {}
        public TransitionException(string message) : base(message) { }
    }

    public class TransitionAlreadyExistsException : TransitionException
    {
        public TransitionAlreadyExistsException(String streamId, Int32 version)
            : base(String.Format(
                "Transition with id ({0}, {1}) already exists", streamId, version)) { }
    }

    public class TransitionStreamNotExistsException : TransitionException
    {
        public TransitionStreamNotExistsException(String id)
            : base(String.Format(
                "Stream with id [{0}] doesn't exist", id)) { }
    }

    public class TransitionNotExistsException : TransitionException
    {
        public TransitionNotExistsException(String id, Int32 version)
            : base(String.Format(
                "Transition with id ({0}, {1} doesn't exist", id, version)) { }
    }
}