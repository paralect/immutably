using System;

namespace Immutably.Transitions
{
    public interface ITransitionStreamWriter : IDisposable
    {
        /// <summary>
        /// Writes transition to the end of stream
        /// </summary>
        void Write(ITransition transition);

        /// <summary>
        /// Writes transition to the end of stream with specified <param name="streamSequence" />
        /// </summary>
        void Write(Int32 streamSequence, Action<ITransitionBuilder> transitionBuilder);
    }
}