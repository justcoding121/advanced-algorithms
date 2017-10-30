using System.Collections.Generic;

namespace Advanced.Algorithms.DynamicProgramming
{
    /// <summary>
    /// Problem statement in detail below
    /// http://www.geeksforgeeks.org/dynamic-programming-set-18-partition-problem/
    /// </summary>
    public class BalancedPartition
    {
        /// <summary>
        /// Returns list of indices for chosen partition half
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool
            CanPartition(int[] input)
        {
            var sum = FindSum(input);

            if (sum % 2 == 1)
            {
                //cannot partition
                return false;
            }

            return CanPartition(input, input.Length - 1, sum / 2, new Dictionary<string, bool>());

        }

        private static bool CanPartition(int[] input, int i, int sum,
            Dictionary<string, bool> cache)
        {
            if (i < 0)
            {
                return false;
            }

            //found a partition
            if (input[i] == sum)
            {
                return true;
            }

            var cacheKey = $"{i}-{sum}";

            if (cache.ContainsKey(cacheKey))
            {
                return cache[cacheKey];
            }

            bool result;
            //cannot pick anyway
            if (input[i] > sum)
            {
                result = CanPartition(input, i - 1, sum, cache);
                cache.Add(cacheKey, result);
                return result;
            }

            //skip or pick
            result = CanPartition(input, i - 1, sum, cache)
                || CanPartition(input, i - 1, sum - input[i], cache);

            if (!cache.ContainsKey(cacheKey))
            {
                cache.Add(cacheKey, result);
            }

            return result;
        }

        private static int FindSum(int[] input)
        {
            var sum = 0;

            foreach(var item in input)
            {
                sum += item;
            }

            return sum;
        }
    }
}
