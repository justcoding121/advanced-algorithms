using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
