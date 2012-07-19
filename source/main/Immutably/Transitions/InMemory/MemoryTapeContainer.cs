using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Lokad.Cqrs.TapeStorage
{
    public sealed class MemoryTapeContainer : ITapeContainer
    {
        ConcurrentDictionary<string, IList<TapeRecord>> _dict = new ConcurrentDictionary<string, IList<TapeRecord>>();


        public ITapeStream GetOrCreateStream(string name)
        {
            return new MemoryTapeStream(_dict, name);
        }

        public void InitializeForWriting() {}
    }
}