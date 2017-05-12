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

            FindSequence(input, input.Length - 1, ref maxLength);

            return maxLength;
        }

        /// <summary>
        /// DP top down
        /// </summary>
        /// <param name="input"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        private static int FindSequence(int[] input, int j, ref int maxLength)
        {
            throw new NotImplementedException();
        }
    }
}
