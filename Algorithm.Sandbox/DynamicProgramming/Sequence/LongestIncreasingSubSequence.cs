using Algorithm.Sandbox.DataStructures;
using System;

namespace Algorithm.Sandbox.DynamicProgramming
{
    /// <summary>
    /// Problem statement in detail below
    /// http://www.geeksforgeeks.org/dynamic-programming-set-3-longest-increasing-subsequence/
    /// </summary>
    public class LongestIncreasingSubSequence
    {
        public static int[] FindSequence(int[] input)
        {
            return FindSequence(input, input.Length - 1, new AsDictionary<int, int[]>());
        }

        /// <summary>
        /// DP top down
        /// </summary>
        /// <param name="input"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        private static int[] FindSequence(int[] input, int i, 
            AsDictionary<int, int[]> cache)
        {
            if (i == 0)
            {
                return new int[1] { input[i] };
            }

            if(cache.ContainsKey(i))
            {
                return cache[i];
            }

            var result = FindSequence(input, i - 1, cache);

            //if last element of result is less than current element
            //append current to result
            if (result[result.Length - 1] < input[i])
            {
                //copy array result for previous i
                var newResult = new int[result.Length + 1];
                Array.Copy(result, newResult, result.Length);

                //append current result at end
                newResult[newResult.Length - 1] = input[i];

                return newResult;
            }

            cache.Add(i, result);

            return result;
        }
    }
}
