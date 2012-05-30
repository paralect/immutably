using System;
using System.Collections.Generic;

namespace Immutably.Transitions
{
    public interface ITransitionRepository
    {
        /// <summary>
        /// Load single transition, uniquely identified by by streamId and streamVersion
        /// </summary>
        ITransition LoadStreamTransition(String streamId, Int32 streamVersion);

        /// <summary>
        /// Load last transition in the stream
        /// </summary>
        ITransition LoadLastStreamTransition(String streamId);

        /// <summary>
        /// Load <param name="count" /> transitions for specified stream, 
        /// ordered by Stream Sequence, starting from <param name="fromStreamVersion" />
        /// </summary>
        IList<ITransition> LoadStreamTransitions(String streamId, Int32 fromStreamVersion, Int32 count);

        /// <summary>
        /// Load <param name="count" /> transitions from store, 
        /// ordered by Timestamp, starting from <param name="fromTimestamp" />
        /// </summary>
        IList<ITransition> LoadStoreTransitions(DateTime fromTimestamp, Int32 count);

        /// <summary>
        /// Append transition
        /// </summary>
        void Append(ITransition transition);

        /// <summary>
        /// Returns all transitions for specified stream in chronological order
        /// Throws TransitionStreamNotExistsException if stream not exists
        /// </summary>
        IList<ITransition> LoadStreamTransitions(String streamId);

        /// <summary>
        /// Returns readonly collection of all transitions in the store in chronological order
        /// </summary>
        IList<ITransition> LoadStoreTransitions();
    }
}