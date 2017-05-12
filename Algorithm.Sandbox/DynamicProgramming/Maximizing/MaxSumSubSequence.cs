using System;
using System.Collections.Generic;

using System.Linq;

namespace Algorithm.Sandbox.DynamicProgramming
{
    /// <summary>
    /// Problem statement in detail below
    /// https://www.hackerrank.com/challenges/maxsubarray
    /// </summary>
    public class MaxSumSequence
    {
        public static int FindMaxSumOfNonContiguousSequence(int[] input)
        {
            //if all are -ive number
            if (input.Max() < 0)
            {
                return input.Max();
            }

            //sum up +ive numbers
            return input.Where(x => x > 0).Sum();
        }

        public static int FindMaxSumOfContiguousSequence(int[] input)
        {
            return FindMaxSequenceSum(input, 0, input.Length - 1, new Dictionary<string, int>());
        }

        /// <summary>
        /// DP top down
        /// </summary>
        /// <param name="input"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        private static int FindMaxSequenceSum(int[] input,
            int i, int j,
            Dictionary<string, int> cache)
        {
            if (i > j)
            {
                return 0;
            }

            if (i == j)
            {
                return input[i];
            }

            var result = Math.Max(FindMaxSequenceSum(input, i, j - 1, cache),
                           FindMaxSequenceSum(input, i + 1, j, cache));

            var currentMax = FindSum(input, i, j, cache);

            return Math.Max(result, currentMax);
        }

        private static int FindSum(int[] input,
            int i, int j,
            Dictionary<string, int> cache)
        {
            if (i > j)
            {
                return 0;
            }

            if (i == j)
            {
                return input[i];
            }

            var cacheKey = string.Concat(i, j);

            if (cache.ContainsKey(cacheKey))
            {
                return cache[cacheKey];
            }

            var sum = FindSum(input, i + 1, j - 1, cache)
                + input[i] + input[j];

            cache.Add(cacheKey, sum);

            return sum;
        }
    }
}
