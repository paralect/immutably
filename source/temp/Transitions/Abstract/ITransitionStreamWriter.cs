using System;
using System.Collections.Generic;

namespace Immutably.Transitions
{
    public interface ITransitionStreamWriter : IDisposable
    {
        /// <summary>
        /// Writes transition to the end of stream
        /// </summary>
        void Write(ITransition transition);

        /// <summary>
        /// Writes events as a single transition to the end of stream with specified <param name="streamVersion" />
        /// </summary>
        void Write(Int32 streamVersion, IEnumerable<Object> events);

        /// <summary>
        /// Writes transition to the end of stream with specified <param name="streamVersion" />
        /// </summary>
        void Write(Int32 streamVersion, Action<ITransitionBuilder> transitionBuilder);
    }
}