using System;
using System.Collections.Generic;

namespace Escolar.Transitions
{
    public class InMemoryTransitionRepository
    {
        private readonly InMemoryTransitionStore _store;
        private readonly InMemoryTransitionSession _session;

        public InMemoryTransitionRepository(InMemoryTransitionStore store, InMemoryTransitionSession session)
        {
            _store = store;
            _session = session;
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