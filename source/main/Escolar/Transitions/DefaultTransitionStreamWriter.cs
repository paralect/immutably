using System;
using System.Collections.Generic;
using Escolar.Messages;

namespace Escolar.Transitions
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
        private readonly Guid _streamId;

        /// <summary>
        /// Creates DefaultTransitionStreamWriter
        /// </summary>
        public DefaultTransitionStreamWriter(ITransitionRepository repository, Guid streamId)
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

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}