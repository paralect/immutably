﻿using System;
using System.Collections.Generic;
using Immutably.Messages;
using Immutably.Utilities;

namespace Immutably.Transitions
{
    /// <summary>
    /// Transition - is a way to group a number of modifications (events) 
    /// for **one** Stream (usually Aggregate Root) in one atomic package, 
    /// that can be either canceled or persisted by Event Store.
    /// </summary>  
    public interface ITransition
    {
        /// <summary>
        /// ID of stream, this transition belongs to
        /// </summary>
        Object StreamId { get; }

        /// <summary>
        /// Serial number of this transition inside stream
        /// </summary>
        Int32 StreamSequence { get; }

        /// <summary>
        /// Timestamp when transition was saved to the Store
        /// (or more accurately - current datetime that was set to Transition _before_ storing it to the Store)
        /// </summary>
        DateTime Timestamp { get; }

        /// <summary>
        /// Readonly collection of Event envelopes, in order (by transition sequence)
        /// </summary>
        IIndexedEnumerable<IEventEnvelope> EventsWithMetadata { get; }

        /// <summary>
        /// Readonly collection of Events, in order (by transition sequence)
        /// </summary>
        IIndexedEnumerable<IEvent> Events { get; }        
    }

    /// <summary>
    /// Transition - is a way to group a number of modifications (events) 
    /// for **one** Stream (usually Aggregate Root) in one atomic package, 
    /// that can be either canceled or persisted by Event Store.
    /// 
    /// Strongly typed by StreamId type.
    /// </summary>  
    public interface ITransition<TStreamId> : ITransition
    {
        /// <summary>
        /// ID of stream, this transition belongs to
        /// </summary>
        new TStreamId StreamId { get; }

        /// <summary>
        /// Readonly collection of Event envelopes, in order (by transition sequence)
        /// </summary>
        new IIndexedEnumerable<IEventEnvelope<TStreamId>> EventsWithMetadata { get; }
    }
}