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
        public static List<int> FindSequence(int[] input)
        {
            return FindSequence(input, input.Length - 1);
        }

        /// <summary>
        /// DP top down
        /// </summary>
        /// <param name="input"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        private static List<int> FindSequence(int[] input, int i)
        {
            if (i == 0)
            {
                return new List<int>() { input[i] };
            }

            var result = FindSequence(input, i - 1);

            //if last element of result is less than current element
            //append current to result
            if (result[result.Count - 1] < input[i])
            {
                result.Add(input[i]);
            }

            return result;
        }
    }
}
