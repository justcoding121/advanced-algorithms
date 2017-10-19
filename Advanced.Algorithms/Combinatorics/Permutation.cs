using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced.Algorithms.Combinatorics
{
    public class Permutation
    {
        public static List<List<T>> Find<T>(List<T> input, bool withRepetition = false)
        {
            var result = new List<List<T>>();

            Recurse(input, withRepetition, new List<T>(), new HashSet<int>(), result);

            return result;
        }

        private static void Recurse<T>(List<T> input, bool withRepetition,
            List<T> prefix, HashSet<int> prefixIndices,
            List<List<T>> result)
        {
            if (prefix.Count == input.Count)
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

                Recurse(input, withRepetition, prefix, prefixIndices, result);

                prefix.RemoveAt(prefix.Count - 1);
                prefixIndices.Remove(j);
            }
        }

        public static List<List<T>> Find<T>(List<T> input, bool withRepetition,
            bool withInversions) where T : IComparable
        {
            var result = new List<List<T>>();

            Recurse(input, withRepetition, withInversions,
                new List<T>(), new HashSet<int>(), result);

            return result;
        }

        private static void Recurse<T>(List<T> input,
            bool withRepetition, bool withInversions,
            List<T> prefix, HashSet<int> prefixIndices,
            List<List<T>> result) where T : IComparable
        {
            if (prefix.Count == input.Count
                && (withInversions ||
                (prefix.Count > 0 && prefix[0].CompareTo(prefix[prefix.Count - 1]) < 0)))
            {
                result.Add(new List<T>(prefix));
                return;
            }

            if (prefix.Count == input.Count)
            {
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

                Recurse(input, withRepetition, withInversions, prefix, prefixIndices, result);

                prefix.RemoveAt(prefix.Count - 1);
                prefixIndices.Remove(j);
            }
        }

    }
}
