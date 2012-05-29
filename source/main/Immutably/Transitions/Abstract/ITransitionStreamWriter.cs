using System;
using System.Collections.Generic;
using Immutably.Messages;

namespace Immutably.Transitions
{
    public interface ITransitionStreamWriter : IDisposable
    {
        /// <summary>
        /// Writes transition to the end of stream
        /// </summary>
        void Write(ITransition transition);

        /// <summary>
        /// Writes events as a single transition to the end of stream with specified <param name="streamSequence" />
        /// </summary>
        void Write(Int32 streamSequence, IEnumerable<IEvent> events);

        /// <summary>
        /// Writes transition to the end of stream with specified <param name="streamSequence" />
        /// </summary>
        void Write(Int32 streamSequence, Action<ITransitionBuilder> transitionBuilder);
    }
}