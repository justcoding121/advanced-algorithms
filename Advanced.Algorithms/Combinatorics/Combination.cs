using System.Collections.Generic;

namespace Advanced.Algorithms.Combinatorics
{
    /// <summary>
    /// Combinations computer.
    /// </summary>
    public class Combination
    {
        public static List<List<T>> Find<T>(List<T> input, int r, bool withRepetition)
        {
            var result = new List<List<T>>();

            recurse(input, r, withRepetition, 0, new List<T>(), new HashSet<int>(), result);

            return result;
        }

        private static void recurse<T>(List<T> input, int r, bool withRepetition,
            int k, List<T> prefix, HashSet<int> prefixIndices,
            List<List<T>> result)
        {
            if (prefix.Count == r)
            {
                result.Add(new List<T>(prefix));
                return;
            }

            for (int j = k; j < input.Count; j++)
            {
                if (prefixIndices.Contains(j) && !withRepetition)
                {
                    continue;
                }

                prefix.Add(input[j]);
                prefixIndices.Add(j);

                recurse(input, r, withRepetition, j, prefix, prefixIndices, result);

                prefix.RemoveAt(prefix.Count - 1);
                prefixIndices.Remove(j);
            }
        }

    }
}
