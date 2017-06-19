using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.DynamicProgramming
{
    /// <summary>
    /// problem details below
    /// https://leetcode.com/problems/burst-balloons/#/description
    /// </summary>
    public class BurstBalloon
    {
        public static int MaxCoins(int[] nums)
        {
            var list = new List<int>();

            //add dummy balloons with identity values at start and end
            list.Add(1);
            list.AddRange(nums);
            list.Add(1);

            return MaxCoinsR(list, 0, list.Count - 1, new Dictionary<string, int>());
        }

        /// <summary>
        /// top down DP
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="cache"></param>
        /// <returns></returns>
        private static int MaxCoinsR(List<int> nums,
            int left, int right,
            Dictionary<string, int> cache)
        {
            //base cases
            if (left > right)
            {
                return 0;
            }

            var cacheKey = $"{left}-{right}";

            if(cache.ContainsKey(cacheKey))
            {
                return cache[cacheKey];
            }

            var max = 0;

            for (int i = left + 1; i < right; i++)
            {
                //assume baloons between (left to i) and (i to right) where already burst 
                //so compute left * i * right which are remaining balloons 
                //and then compute sub max value for burst baloons b/w (left to i) and (i to right)
                max = Math.Max(max, (nums[left] * nums[i] * nums[right])
                    + MaxCoinsR(nums, left, i, cache) + MaxCoinsR(nums, i, right, cache));
            }

            cache.Add(cacheKey, max);

            return max;
        }


    }
}
