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
            return MaxCoinsR(nums.ToList());
        }

        public static int MaxCoinsR(List<int> nums)
        {
            var max = 0;

            for (int i = 0; i < nums.Count; i++)
            {
                var subArray = SplitMergeArray(nums, i);

                var subMaxLeft = MaxCoinsR(subArray);

                var currentMax = subMaxLeft +
                    ((i == 0 ? 1 : nums[i - 1])
                    * nums[i]
                    * ((i == nums.Count - 1) ? 1 : nums[i + 1]));

                if (currentMax > max)
                {
                    max = currentMax;
                }

            }

            return max;
        }

        private static List<int> SplitMergeArray(List<int> nums, int k)
        {
            var list = new List<int>();

            for (int i = 0; i < k; i++)
            {
                list.Add(nums[i]);
            }

            for (int i = k + 1; i < nums.Count; i++)
            {
                list.Add(nums[i]);
            }

            return list;
        }
    }
}
