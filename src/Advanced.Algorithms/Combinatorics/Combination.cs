using System.Collections.Generic;

namespace Advanced.Algorithms.Combinatorics
{
    /// <summary>
    /// Combinations computer.
    /// </summary>
    public class Combination
    {
        public static IEnumerable<T[]> Find<T>(List<T> input, int r, bool withRepetition)
        {
            return recurse(input, r, withRepetition, 0, new List<T>(), new HashSet<int>());
        }

        private static IEnumerable<T[]> recurse<T>(List<T> input, int r, bool withRepetition, int k, List<T> prefix, HashSet<int> prefixIndices)
        {
            if (prefix.Count == r)
            {
                yield return prefix.ToArray();
            }

            for (int j = k; j < input.Count; j++)
            {
                if (prefixIndices.Contains(j) && !withRepetition)
                {
                    continue;
                }

                prefix.Add(input[j]);
                prefixIndices.Add(j);

                foreach (var item in recurse(input, r, withRepetition, j, prefix, prefixIndices))
                {
                    yield return item;
                }

                prefix.RemoveAt(prefix.Count - 1);
                prefixIndices.Remove(j);
            }
        }
    }
}
