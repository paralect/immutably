using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Lokad.Cqrs.TapeStorage
{
    sealed class MemoryTapeStream : ITapeStream
    {
        ConcurrentDictionary<string, IList<TapeRecord>> _dictionary;
        readonly string _name;

        public MemoryTapeStream(ConcurrentDictionary<string, IList<TapeRecord>> dictionary, string name)
        {
            _dictionary = dictionary;
            _name = name;
        }

        public IEnumerable<TapeRecord> ReadRecords(long afterVersion, int maxCount)
        {
            if (afterVersion < 0)
                throw new ArgumentOutOfRangeException("afterVersion", "Must be zero or greater.");

            if (maxCount <= 0)
                throw new ArgumentOutOfRangeException("maxCount", "Must be more than zero.");

            IList<TapeRecord> bytes;
            if (_dictionary.TryGetValue(_name, out bytes))
            {
                foreach (var bytese in bytes.Where(r => r.Version > afterVersion).Take(maxCount))
                {
                    yield return bytese;
                }
            }
        }

        public long GetCurrentVersion()
        {
            IList<TapeRecord> records;
            if (_dictionary.TryGetValue(_name, out records))
            {
                return records.Count;
            }
            return 0;
        }

        public long TryAppend(byte[] buffer, TapeAppendCondition appendCondition = new TapeAppendCondition())
        {
            if (buffer == null)
                throw new ArgumentNullException("buffer");

            if (buffer.Length == 0)
                throw new ArgumentException("Buffer must contain at least one byte.");

            try
            {
                var result = _dictionary.AddOrUpdate(_name, s =>
                {
                    appendCondition.Enforce(0);
                    var records = new List<TapeRecord>();
                    records.Add(new TapeRecord(1, buffer));
                    return records;
                }, (s, list) =>
                {
                    appendCondition.Enforce(list.Count);
                    return list.Concat(new[] {new TapeRecord(list.Count + 1, buffer)}).ToList();
                });

                return result.Count;
            }
            catch (TapeAppendConditionException e)
            {
                return 0;
            }
        }
    }
}