#region (c) 2010-2012 Lokad - CQRS- New BSD License 

// Copyright (c) Lokad 2010-2012, http://www.lokad.com
// This code is released as Open Source under the terms of the New BSD Licence

#endregion

using System.Collections.Generic;

namespace Lokad.Cqrs.TapeStorage
{
    /// <summary>
    /// Named tape stream, that usually matches to an aggregate instance
    /// </summary>
    public interface ITapeStream
    {
        /// <summary>
        /// Reads up to <see cref="maxCount"/> records starting with version next to <see cref="afterVersion"/>.
        /// </summary>
        /// <param name="afterVersion">Number of version to start after.</param>
        /// <param name="maxCount">The max number of records to load.</param>
        /// <returns>collection of blocks</returns>
        IEnumerable<TapeRecord> ReadRecords(long afterVersion, int maxCount);

        /// <summary>
        /// Returns current storage version
        /// </summary>
        /// <returns>current version of the storage</returns>
        long GetCurrentVersion();

        /// <summary>
        /// Tries the append data to the tape storage, ensuring that
        /// the version condition is met (if the condition is specified).
        /// </summary>
        /// <param name="buffer">The data to append.</param>
        /// <param name="appendCondition">The append condition.</param>
        /// <returns>version of the appended data</returns>
        long TryAppend(byte[] buffer, TapeAppendCondition appendCondition = default(TapeAppendCondition));
    }
}