using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.DynamicProgramming
{
    /// <summary>
    /// Problem statement below
    /// http://www.geeksforgeeks.org/dynamic-programming-set-24-optimal-binary-search-tree/
    /// </summary>
    public class OptimalBST
    {
        public static int FindOptimalCost(int[] keys, int[] freq)
        {
            return FindOptimalCost(freq, 0, keys.Length - 1, 1, new Dictionary<string, int>());
        }

        public static int FindOptimalCost(int[] freq, 
            int left, int right, int level, Dictionary<string, int> cache)
        {
            if (left > right)
            {
                return 0;
            }

            if (left == right)
            {
                return freq[left] * level;
            }
            var cacheKey = $"{left}-{right}-{level}";

            if(cache.ContainsKey(cacheKey))
            {
                return cache[cacheKey];
            }

            var min = int.MaxValue;

            for (int i = left; i <= right; i++)
            {
                min = Math.Min(min, FindOptimalCost(freq, 0, i - 1, level + 1, cache)
                      + FindOptimalCost(freq, i + 1, right, level + 1, cache) + (freq[i] * level));
            }

            cache.Add(cacheKey, min);

            return min;
        }
    }
}
