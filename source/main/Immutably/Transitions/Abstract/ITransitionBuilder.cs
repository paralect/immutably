using Immutably.Messages;

namespace Immutably.Transitions
{
    public interface ITransitionBuilder<TStreamId>
    {
        /// <summary>
        /// Adds event to transition.
        /// Event metadata will be automatically created 
        /// (based on this transition's StreamId, StreamSequence and next available TransitionSequence)
        /// </summary>
        ITransitionBuilder<TStreamId> AddEvent(IEvent evnt);

        /// <summary>
        /// Adds event and corresponding event metadata to this transition.
        /// Event metadata should has correct StreamId, StreamSequence and TransitionSequence.
        /// </summary>
        ITransitionBuilder<TStreamId> AddEvent(IEvent evnt, IEventMetadata<TStreamId> metadata);

        /// <summary>
        /// Adds event envelope to this transition
        /// Event metadata should has correct StreamId, StreamSequence and TransitionSequence.
        /// </summary>
        ITransitionBuilder<TStreamId> AddEvent(IEventEnvelope<TStreamId> envelope);

        /// <summary>
        /// Build Transition
        /// </summary>
        ITransition<TStreamId> Build();
    }
}