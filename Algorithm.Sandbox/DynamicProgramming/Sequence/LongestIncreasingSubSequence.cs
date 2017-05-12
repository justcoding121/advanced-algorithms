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
            if (j == 0)
            {
                return 1;
            }

            var currentMaxLength = 1;

            for (int i = 0; i < j; i++)
            {
                //from 0 to i
                var subMaxLength = FindSequence(input, i, ref maxLength);

                //if 0 to i sequence last value (i) is less than current value j
                //And if it improves our max
                if (input[i] < input[j]
                    && currentMaxLength < subMaxLength + 1)
                {
                    currentMaxLength = subMaxLength + 1;
                }
            }

            maxLength = Math.Max(maxLength, currentMaxLength);

            return currentMaxLength;
        }
    }
}
