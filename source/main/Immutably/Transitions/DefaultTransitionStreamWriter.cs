using System;
using System.Collections.Generic;
using Immutably.Messages;

namespace Immutably.Transitions
{
    /// <summary>
    /// Writes transitions to stream
    /// </summary>
    public class DefaultTransitionStreamWriter : ITransitionStreamWriter
    {
        /// <summary>
        /// Transition repository
        /// </summary>
        private readonly ITransitionRepository _repository;

        /// <summary>
        /// Stream Id
        /// </summary>
        private readonly String _streamId;

        /// <summary>
        /// Creates DefaultTransitionStreamWriter
        /// </summary>
        public DefaultTransitionStreamWriter(ITransitionRepository repository, String streamId)
        {
            _repository = repository;
            _streamId = streamId;
        }

        /// <summary>
        /// Writes transition to the end of stream
        /// </summary>
        public void Write(ITransition transition)
        {
            _repository.Append(transition);
        }

        /// <summary>
        /// Writes transition to the end of stream with specified <param name="streamSequence" />
        /// </summary>
        public void Write(Int32 streamSequence, Action<ITransitionBuilder> transitionBuilder)
        {
            var transition = new TransitionBuilder(_streamId, streamSequence, DateTime.UtcNow);
            transitionBuilder(transition);
            _repository.Append(transition.Build());
        }

        /// <summary>
        /// Writes events as a single transition to the end of stream with specified <param name="streamSequence" />
        /// </summary>
        public void Write(int streamSequence, IEnumerable<IEvent> events)
        {
            Write(streamSequence, builder => builder
                .AddEvents(events)
            );
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}