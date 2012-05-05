using System;
using System.Collections.Generic;

namespace Escolar.Transitions
{
    public class InMemoryTransitionRepository
    {
        private readonly InMemoryTransitionStore _store;

        public InMemoryTransitionRepository(InMemoryTransitionStore store)
        {
            _store = store;
        }

        public IList<ITransition> GetById(Guid id)
        {
            return _store.GetById(id);
        }

        public void Append(IList<ITransition> transitions)
        {
            _store.Append(transitions);
        }        
    }
}