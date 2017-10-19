using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced.Algorithms.Combinatorics
{
    public class Variation
    {
        /*Variations are arrangements of selections of objects, where the order of the selected objects matters.
        To count k-element variations of n objects, we first need to choose a k-element combination and then
        a permutation of the selected objects*/

        //Without repetition
        /* It is also the number of ways of putting r distinct balls into input.Count distinct boxes such that each box
        receives at most one element. */

        //With repetition
        /* It is the number of all ways of putting r distinct balls into input.Count distinct boxes */
        public static List<List<T>> Find<T>(List<T> input, int r, bool withRepetition)
        {
            var result = new List<List<T>>();

            Recurse(input, r, withRepetition, new List<T>(), new HashSet<int>(), result);

            return result;
        }

        private static void Recurse<T>(List<T> input, int r, bool withRepetition,
         List<T> prefix, HashSet<int> prefixIndices,
         List<List<T>> result)
        {
            if (prefix.Count == r)
            {
                result.Add(new List<T>(prefix));
                return;
            }

            for (int j = 0; j < input.Count; j++)
            {
                if (prefixIndices.Contains(j) && !withRepetition)
                {
                    continue;
                }

                prefix.Add(input[j]);
                prefixIndices.Add(j);

                Recurse(input, r, withRepetition, prefix, prefixIndices, result);

                prefix.RemoveAt(prefix.Count - 1);
                prefixIndices.Remove(j);
            }
        }
    }
}
