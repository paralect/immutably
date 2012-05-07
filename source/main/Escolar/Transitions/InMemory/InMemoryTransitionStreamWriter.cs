using System;
using System.Collections.Generic;
using Escolar.Messages;

namespace Escolar.Transitions
{
    public class InMemoryTransitionStreamWriter : ITransitionStreamWriter
    {
        private readonly InMemoryTransitionRepository _repository;
        private readonly Guid _streamId;

        public InMemoryTransitionStreamWriter(InMemoryTransitionRepository repository, Guid streamId)
        {
            _repository = repository;
            _streamId = streamId;
        }

        public void Write(ITransition transition)
        {
            _repository.Append(transition);
        }

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