using System;
using System.Collections.Generic;

namespace Algorithm.Sandbox.DynamicProgramming
{
    /// <summary>
    /// Problem statement in detail below
    /// http://www.geeksforgeeks.org/dynamic-programming-set-3-longest-increasing-subsequence/
    /// </summary>
    public class LongestIncreasingSubSequence
    {
        public static int FindSequence(int[] input)
        {
            var maxLength = 0;

            FindSequence(input, input.Length - 1,
                ref maxLength, new Dictionary<int, int>());

            return maxLength;
        }

        /// <summary>
        /// DP top down
        /// </summary>
        /// <param name="input"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        private static int FindSequence(int[] input, int j, 
            ref int netLongest,
            Dictionary<int, int> cache)
        {
            if (j == 0)
            {
                return 1;
            }

            if(cache.ContainsKey(j))
            {
                return cache[j];
            }

            var currentLongest = 1;

            for (int i = 0; i < j; i++)
            {
                //from 0 to i
                var subLongest = FindSequence(input, i, ref netLongest, cache);

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
    }
}
