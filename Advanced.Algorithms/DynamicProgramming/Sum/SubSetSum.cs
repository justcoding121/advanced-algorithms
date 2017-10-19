using System.Collections.Generic;

namespace Advanced.Algorithms.DynamicProgramming.Sum
{
    /// <summary>
    /// Problem statement
    /// http://www.geeksforgeeks.org/dynamic-programming-subset-sum-problem/
    /// </summary>
    public class SubSetSum
    {
        public static bool HasSubSet(int[] input, int sum)
        {
            return SubSetSumR(input, input.Length - 1, sum,
                new Dictionary<string, bool>());
        }

        private static bool SubSetSumR(int[] input, int i, int sum,
            Dictionary<string, bool> cache)
        {
            if (i < 0)
            {
                return false;
            }

            //found sequence
            if (input[i] == sum)
            {
                return true;
            }

            var cacheKey = $"{i}-{sum}";

            if (cache.ContainsKey(cacheKey))
            {
                return cache[cacheKey];
            }

            bool result;
            //skip
            if (input[i] > sum)
            {
                result = SubSetSumR(input, i - 1, sum, cache);
                cache.Add(cacheKey, result);
                return result;
            }

            //skip or pick
            result = SubSetSumR(input, i - 1, sum, cache)
                || SubSetSumR(input, i - 1, sum - input[i], cache);

            if (!cache.ContainsKey(cacheKey))
            {
                cache.Add(cacheKey, result);
            }

            return result;
        }
    }
}
