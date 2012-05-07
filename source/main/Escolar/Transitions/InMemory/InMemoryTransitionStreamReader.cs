using System;
using System.Collections.Generic;

namespace Escolar.Transitions
{
    public class InMemoryTransitionStreamReader : ITransitionStreamReader
    {
        private readonly InMemoryTransitionRepository _repository;
        private readonly Guid _streamId;
        

        public InMemoryTransitionStreamReader(InMemoryTransitionRepository repository, Guid streamId)
        {
            _repository = repository;
            _streamId = streamId;
        }

        public IEnumerable<ITransition> Read()
        {
            var list = _repository.GetById(_streamId);

            foreach (var transition in list)
            {
                yield return transition;
            }
        }

        public void Dispose()
        {
            
        }
    }
}