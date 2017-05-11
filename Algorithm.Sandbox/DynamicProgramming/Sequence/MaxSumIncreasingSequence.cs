using System.Linq;
using System.Collections.Generic;

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
            return FindSequence(input, input.Length - 1);
        }

        /// <summary>
        /// DP top down
        /// </summary>
        /// <param name="input"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        private static int FindSequence(int[] input, int i)
        {
            if (i < 0)
            {
                return 0;
            }

            if (i == 0)
            {
                return  input[0] ;
            }

            //can't pick i anyway
            if (input[i-1] > input[i])
            {
                return FindSequence(input, i - 1);
            }

            var sum = FindSequence(input, i - 1);

            if(sum < input[i])
            {
                return input[i];
            }

            //if picking i improved sum do so
            if (sum + input[i] > sum)
            {
                return sum + input[i];
            }

            return sum;

        }
    }
}
