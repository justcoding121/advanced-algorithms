using System.Collections.Generic;

namespace Algorithm.Sandbox.DynamicProgramming.Sum
{
    /// <summary>
    /// Problem statement
    /// http://www.geeksforgeeks.org/dynamic-programming-subset-sum-problem/
    /// </summary>
    public class SubSetSum
    {
        public static bool HasSubSet(int[] input, int sum)
        {
            return SubSetSumR(input, input.Length - 1, sum,
                new Dictionary<int, bool>());
        }

        public static bool SubSetSumR(int[] input, int i, int sum, 
            Dictionary<int, bool> cache)
        {
            if (i < 0)
            {
                return false;
            }

            //found sequence
            if (input[i] == sum)
            {
                return true;
            }

            if(cache.ContainsKey(i))
            {
                return cache[i];
            }

            bool result;
            //skip
            if (input[i] > sum)
            {
                result = SubSetSumR(input, i - 1, sum, cache);
                cache.Add(i, result);
                return result;
            }

            //skip or pick
            result = SubSetSumR(input, i - 1, sum, cache)
                || SubSetSumR(input, i, sum - input[i], cache);

            if (!cache.ContainsKey(i))
            {
                cache.Add(i, result);
            }

            return result;
        }
    }
}
