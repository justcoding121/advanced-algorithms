using System.Collections.Generic;

namespace Advanced.Algorithms.DataStructures
{
    internal static class IEnumerableExtensions
    {
        internal static IEnumerable<T> AsEnumerable<T>(this IEnumerator<T> e)
        {
            while (e.MoveNext())
            {
                yield return e.Current;
            }
        }
    }
}
