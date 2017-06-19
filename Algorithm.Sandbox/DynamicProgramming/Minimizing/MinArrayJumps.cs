using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.DynamicProgramming.Minimizing
{
    /// <summary>
    /// Problem statement below
    /// http://www.geeksforgeeks.org/minimum-number-of-jumps-to-reach-end-of-a-given-array/
    /// </summary>
    public class MinArrayJumps
    {
        public static int GetMinJumps(int[] input)
        {
            return GetMinJumps(input, input.Length - 1,
                new Dictionary<int, int>());
        }

        /// <summary>
        /// DP top down
        /// Simulate all possible jumps
        /// </summary>
        /// <param name="input"></param>
        /// <param name="j"></param>
        /// <param name="cache"></param>
        /// <returns></returns>
        private static int GetMinJumps(int[] input, int j, 
            Dictionary<int, int> cache)
        {
            if (j == 0)
            {
                return 0;
            }

            if(cache.ContainsKey(j))
            {
                return cache[j];
            }

            var localMin = int.MaxValue;

            for (int i = 0; i < j; i++)
            {
                var subMin = GetMinJumps(input, i, cache);

                //jump possible only if i + input[i] >=j
                if (i + input[i] >= j
                    && subMin + 1 < localMin)
                {
                    localMin = subMin + 1;
                }
            }

            cache.Add(j, localMin);

            return localMin;
        }
    }
}
