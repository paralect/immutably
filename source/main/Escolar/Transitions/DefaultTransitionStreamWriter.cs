using System;
using System.Collections.Generic;
using Escolar.Messages;

namespace Escolar.Transitions
{
    /// <summary>
    /// Writes transitions to stream
    /// </summary>
    public class DefaultTransitionStreamWriter<TStreamId> : ITransitionStreamWriter<TStreamId>
    {
        /// <summary>
        /// Transition repository
        /// </summary>
        private readonly ITransitionRepository<TStreamId> _repository;

        /// <summary>
        /// Stream Id
        /// </summary>
        private readonly TStreamId _streamId;

        /// <summary>
        /// Creates DefaultTransitionStreamWriter
        /// </summary>
        public DefaultTransitionStreamWriter(ITransitionRepository<TStreamId> repository, TStreamId streamId)
        {
            _repository = repository;
            _streamId = streamId;
        }

        /// <summary>
        /// Writes transition to the end of stream
        /// </summary>
        public void Write(ITransition<TStreamId> transition)
        {
            _repository.Append(transition);
        }

        /// <summary>
        /// Writes transition to the end of stream with specified <param name="streamSequence" />
        /// </summary>
        public void Write(Int32 streamSequence, Action<ITransitionBuilder<TStreamId>> transitionBuilder)
        {
            var transition = new TransitionBuilder<TStreamId>(_streamId, streamSequence, DateTime.UtcNow);
            transitionBuilder(transition);
            _repository.Append(transition.Build());
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}