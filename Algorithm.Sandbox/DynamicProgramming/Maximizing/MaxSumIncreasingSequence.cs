using System.Linq;
using System.Collections.Generic;
using System;

namespace Algorithm.Sandbox.DynamicProgramming
{
    /// <summary>
    /// Problem statement in detail below
    /// http://www.geeksforgeeks.org/dynamic-programming-set-3-longest-increasing-subsequence/
    /// </summary>
    public class MaxSumIncreasingSequence
    {
        public static int FindSum(int[] input)
        {
            var netMax = 0;

            var result = FindSum(input, input.Length - 1, ref netMax);

            return netMax;
        }

        /// <summary>
        /// DP top down
        /// </summary>
        /// <param name="input"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        //private static int FindMaxSequenceSum(int[] input,
        //    int i, int j,
        //    Dictionary<string, int> cache)
        //{
        //    if (i > j)
        //    {
        //        return 0;
        //    }

        //    if (i == j)
        //    {
        //        return input[i];
        //    }

        //    var result = Math.Max(FindMaxSequenceSum(input, i, j - 1, cache),
        //                   FindMaxSequenceSum(input, i + 1, j, cache));

        //    var currentMax = FindSum(input, i, j, int.MaxValue);

        //    return Math.Max(result, currentMax);
        //}

        public static int FindSum(int[] input,
            int j, ref int netMax)
        {
            throw new NotImplementedException();
        }
    }
}
