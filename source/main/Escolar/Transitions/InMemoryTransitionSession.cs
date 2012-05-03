using System;
using System.Collections.Generic;
using System.Linq;
using Escolar.Messages;

namespace Escolar.Transitions
{
    public class InMemoryTransitionSession : ITransitionSession
    {
        private readonly InMemoryTransitionStore _store;
        private readonly InMemoryTransitionRepository _repository;

        private readonly List<ITransition> _pendingTransitions = new List<ITransition>();
        private readonly List<IEventEnvelope> _pendingEventEnvelopes = new List<IEventEnvelope>();

        public InMemoryTransitionSession(InMemoryTransitionStore store)
        {
            _store = store;
            _repository = new InMemoryTransitionRepository(_store, this);
        }

        public IList<ITransition> LoadTransitions(Guid streamId)
        {
            return _repository.GetById(streamId);
        }

        public void Append(IList<IEventEnvelope> envelopes)
        {
            _pendingEventEnvelopes.AddRange(envelopes);
        }

        public void Append(params IEventEnvelope[] eventEnvelope)
        {
            Append(eventEnvelope.ToList());
        }

        public void Append(IList<ITransition> transitions)
        {
            _pendingTransitions.AddRange(transitions);
        }

        public void Append(params ITransition[] transitions)
        {
            Append(transitions.ToList());
        }

        public void SaveChanges()
        {
            var transitions = new List<ITransition>(_pendingTransitions.Count + 1);

            if (_pendingTransitions.Count > 0)
                transitions.AddRange(_pendingTransitions);

            if (_pendingEventEnvelopes.Count > 0)
                transitions.Add(new Transition(_pendingEventEnvelopes));
            
            if (transitions.Count <= 0)
                return;

            _repository.Append(transitions);
        }

        public void Dispose()
        {
            
        }
    }
}