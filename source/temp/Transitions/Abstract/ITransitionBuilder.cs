using System;
using System.Collections.Generic;
using Immutably.Messages;

namespace Immutably.Transitions
{
    public interface ITransitionBuilder
    {
        /// <summary>
        /// Adds event to transition.
        /// Event metadata will be automatically created 
        /// (based on this transition's StreamId, StreamVersion and next available TransitionSequence)
        /// </summary>
        ITransitionBuilder AddEvent(Object evnt);        
        
        /// <summary>
        /// Adds event and corresponding event metadata to this transition.
        /// Event metadata should has correct StreamId, StreamVersion and TransitionSequence.
        /// </summary>
        ITransitionBuilder AddEvent(Object evnt, IEventMetadata metadata);

        /// <summary>
        /// Adds event envelope to this transition
        /// Event metadata should has correct StreamId, StreamVersion and TransitionSequence.
        /// </summary>
        ITransitionBuilder AddEvent(IEventEnvelope envelope);

        /// <summary>
        /// Adds events to transition.
        /// Event metadata will be automatically created 
        /// (based on this transition's StreamId, StreamVersion and next available TransitionSequence)
        /// </summary>
        ITransitionBuilder AddEvents(IEnumerable<Object> events);

        /// <summary>
        /// Build Transition
        /// </summary>
        ITransition Build();        
    }
}