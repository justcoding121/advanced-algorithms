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
            return input.Where(x => x > 0).Sum();
        }

        public static int FindMaxSumOfContiguousSequence(int[] input)
        {
            return FindSequenceSum(input, 0, input.Length - 1);
        }

        /// <summary>
        /// DP top down
        /// </summary>
        /// <param name="input"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        private static int FindSequenceSum(int[] input, int i, int j)
        {
            if (i > j)
            {
                return 0;
            }

            if (i == j)
            {
                return input[i];
            }

            var results = new List<int>();

            results.Add(FindSequenceSum(input, i, j - 1));
            results.Add(FindSequenceSum(input, i + 1, j));

            return results.Max();
        }
    }
}
