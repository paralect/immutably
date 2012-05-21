using System.Collections;
using System.Collections.Generic;

namespace Immutably.Utilities
{
    /// <summary>
    /// Approach described here:
    /// http://stackoverflow.com/a/5832173
    /// </summary>
    public interface IIndexedEnumerable<out T> : IEnumerable<T>
    {
        T this[int index] { get; }
        int Count { get; }
    }

    public static class IndexedEnumerableExtension
    {
        public static IIndexedEnumerable<T> AsIndexedEnumerable<T>(this IList<T> tail)
        {
            return new IIndexedEnumerableWrapper<T>(tail);
        }
        private class IIndexedEnumerableWrapper<T> : IIndexedEnumerable<T>
        {
            private readonly IList<T> _list;

            public int Count
            {
                get { return _list.Count; }
            }

            public IIndexedEnumerableWrapper(IList<T> list)
            {
                _list = list;
            }

            public T this[int index]
            {
                get { return _list[index]; }
            }

            public IEnumerator<T> GetEnumerator()
            {
                return _list.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return _list.GetEnumerator();
            }
        }
    }
}