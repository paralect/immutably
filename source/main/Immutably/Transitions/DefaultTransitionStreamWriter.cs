using System;
using System.Collections.Generic;
using Immutably.Data;
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

        private readonly IDataFactory _dataFactory;

        /// <summary>
        /// Stream Id
        /// </summary>
        private readonly String _streamId;

        /// <summary>
        /// Creates DefaultTransitionStreamWriter
        /// </summary>
        public DefaultTransitionStreamWriter(ITransitionRepository repository, IDataFactory dataFactory, String streamId)
        {
            _repository = repository;
            _dataFactory = dataFactory;
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
        /// Writes transition to the end of stream with specified <param name="streamVersion" />
        /// </summary>
        public void Write(Int32 streamVersion, Action<ITransitionBuilder> transitionBuilder)
        {
            var transition = new TransitionBuilder(_dataFactory, _streamId, streamVersion, DateTime.UtcNow);
            transitionBuilder(transition);
            _repository.Append(transition.Build());
        }

        /// <summary>
        /// Writes events as a single transition to the end of stream with specified <param name="streamVersion" />
        /// </summary>
        public void Write(int streamVersion, IEnumerable<IEvent> events)
        {
            Write(streamVersion, builder => builder
                .AddEvents(events)
            );
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}