using System;
using System.Collections.Generic;

namespace Immutably.Transitions
{
    public interface ITransitionRepository<TStreamId>
    {
        /// <summary>
        /// Load single transition, uniquely identified by by streamId and streamSequence
        /// </summary>
        ITransition<TStreamId> LoadTransition(TStreamId streamId, Int32 streamSequence);

        /// <summary>
        /// Load <param name="count" /> transitions for specified stream, 
        /// ordered by Stream Sequence, starting from <param name="fromStreamSequence" />
        /// </summary>
        IList<ITransition<TStreamId>> LoadStreamTransitions(TStreamId streamId, Int32 fromStreamSequence, Int32 count);

        /// <summary>
        /// Load <param name="count" /> transitions from store, 
        /// ordered by Timestamp, starting from <param name="fromTimestamp" />
        /// </summary>
        IList<ITransition<TStreamId>> LoadStoreTransitions(DateTime fromTimestamp, Int32 count);

        /// <summary>
        /// Append transition
        /// </summary>
        void Append(ITransition<TStreamId> transition);
    }
}