using System;
using System.Collections.Generic;

namespace Algorithm.Sandbox.DynamicProgramming
{
    /// <summary>
    /// Problem statement in detail below
    /// http://www.geeksforgeeks.org/dynamic-programming-set-15-longest-bitonic-subsequence/
    /// </summary>
    public class LongestBitonicSubSequence
    {
        public static int FindSequence(int[] input)
        {
            var netLongest = 0;

            var increasingCache = new Dictionary<int, int>();
            var decreasingCache = new Dictionary<string, int>();

            for (int i = 0; i < input.Length; i++)
            {

                var maxIncreasingSeqLength = 1;

                FindLongestIncreasingSequence(input, i,
                    ref maxIncreasingSeqLength, increasingCache);

                var maxDecreasingSeqLength = 0;

                FindLongestDecreasingSequence(input, i, input.Length - 1,
                    ref maxDecreasingSeqLength, decreasingCache);

                var longest = maxIncreasingSeqLength + 
                                    maxDecreasingSeqLength - 1;

                netLongest = Math.Max(netLongest, longest);

            }
           

            return netLongest;

        }

        /// <summary>
        /// DP top down
        /// </summary>
        /// <param name="input"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        private static int FindLongestIncreasingSequence(int[] input, int j,
            ref int netLongest,
            Dictionary<int, int> cache)
        {
            if (j == 1)
            {
                return 1;
            }

            if (cache.ContainsKey(j))
            {
                return cache[j];
            }

            var currentLongest = 1;

            for (int i = 0; i < j; i++)
            {
                //from 0 to i
                var subLongest = FindLongestIncreasingSequence(input, i, ref netLongest, cache);

                //if 0 to i sequence last value (i) is less than current value j
                //And if it improves our current Longest
                if (input[i] < input[j]
                    && currentLongest < subLongest + 1)
                {
                    currentLongest = subLongest + 1;
                }
            }

            netLongest = Math.Max(netLongest, currentLongest);

            cache.Add(j, currentLongest);

            return currentLongest;
        }

        /// <summary>
        /// DP top down
        /// from left to j
        /// </summary>
        /// <param name="input"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        private static int FindLongestDecreasingSequence(int[] input, 
            int left, int j,
            ref int netLongest,
            Dictionary<string, int> cache)
        {
            if (j == left)
            {
                return 1;
            }

            var cacheKey = string.Concat(left, j);

            if (cache.ContainsKey(cacheKey))
            {
                return cache[cacheKey];
            }

            var currentLongest = 1;

            for (int i = left; i < j; i++)
            {
                //from 0 to i
                var subLongest = FindLongestDecreasingSequence(input, left, i, ref netLongest, cache);

                //if 0 to i sequence last value (i) is greater than current value j
                //And if it improves our current Longest
                if (input[i] > input[j]
                    && currentLongest < subLongest + 1)
                {
                    currentLongest = subLongest + 1;
                }
            }

            netLongest = Math.Max(netLongest, currentLongest);

            cache.Add(cacheKey, currentLongest);

            return currentLongest;
        }
    }
}
