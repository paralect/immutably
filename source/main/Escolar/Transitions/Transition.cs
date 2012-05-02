using System;
using System.Collections.Generic;
using System.Linq;
using Escolar.Messages;
using Escolar.Transitions;

namespace Escolar.Transitions
{
    /// <summary>
    /// Transition - is a way to group a number of modifications (events) 
    /// for **one** Stream (usually Aggregate Root) in one atomic package, 
    /// that can be either canceled or persisted by Event Store.
    /// </summary>  
    public class Transition : ITransition
    {
        private readonly List<IEventEnvelope> _eventEnvelopes;
        private readonly Int32 _version;
        private readonly Guid _entityId;

        public IList<IEventEnvelope> EventEnvelopes
        {
            get { return _eventEnvelopes; }
        }

        public int Version
        {
            get { return _version; }
        }

        public Guid EntityId
        {
            get { return _entityId; }
        }

        public Transition(List<IEventEnvelope> eventEnvelopes)
        {
            _eventEnvelopes = eventEnvelopes;
            _version = eventEnvelopes.Last().Metadata.SenderVersion;
            _entityId = eventEnvelopes.Last().Metadata.SenderId;
        }
    }
}