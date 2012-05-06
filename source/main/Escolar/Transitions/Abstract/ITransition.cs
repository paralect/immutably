
using System;
using System.Collections.Generic;
using Escolar.Messages;

namespace Escolar.Transitions
{
    /// <summary>
    /// Transition - is a way to group a number of modifications (events) 
    /// for **one** Stream (usually Aggregate Root) in one atomic package, 
    /// that can be either canceled or persisted by Event Store.
    /// </summary>  
    public interface ITransition
    {
        Guid StreamId { get; }

        Int32 StreamSequence { get; }

        Int32 TransitionSequence { get; }

        IList<IEventEnvelope> EventEnvelopes { get; }

        IList<IEvent> Events { get; }
    }
}