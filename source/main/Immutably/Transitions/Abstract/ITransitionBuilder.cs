using Immutably.Messages;

namespace Immutably.Transitions
{
    public interface ITransitionBuilder
    {
        /// <summary>
        /// Adds event to transition.
        /// Event metadata will be automatically created 
        /// (based on this transition's StreamId, StreamSequence and next available TransitionSequence)
        /// </summary>
        ITransitionBuilder AddEvent(IEvent evnt);

        /// <summary>
        /// Adds event and corresponding event metadata to this transition.
        /// Event metadata should has correct StreamId, StreamSequence and TransitionSequence.
        /// </summary>
        ITransitionBuilder AddEvent(IEvent evnt, IEventMetadata metadata);

        /// <summary>
        /// Adds event envelope to this transition
        /// Event metadata should has correct StreamId, StreamSequence and TransitionSequence.
        /// </summary>
        ITransitionBuilder AddEvent(IEventEnvelope envelope);

        /// <summary>
        /// Build Transition
        /// </summary>
        ITransition Build();        
    }

    public interface ITransitionBuilder<TStreamId> : ITransitionBuilder
    {
        /// <summary>
        /// Adds event to transition.
        /// Event metadata will be automatically created 
        /// (based on this transition's StreamId, StreamSequence and next available TransitionSequence)
        /// </summary>
        new ITransitionBuilder<TStreamId> AddEvent(IEvent evnt);

        /// <summary>
        /// Adds event and corresponding event metadata to this transition.
        /// Event metadata should has correct StreamId, StreamSequence and TransitionSequence.
        /// </summary>
        new ITransitionBuilder<TStreamId> AddEvent(IEvent evnt, IEventMetadata<TStreamId> metadata);

        /// <summary>
        /// Adds event envelope to this transition
        /// Event metadata should has correct StreamId, StreamSequence and TransitionSequence.
        /// </summary>
        new ITransitionBuilder<TStreamId> AddEvent(IEventEnvelope<TStreamId> envelope);

        /// <summary>
        /// Build Transition
        /// </summary>
        new ITransition<TStreamId> Build();
    }
}