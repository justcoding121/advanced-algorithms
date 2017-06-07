using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.DynamicProgramming.Minimizing
{
    /// <summary>
    /// Problem statement below
    /// http://www.geeksforgeeks.org/dynamic-programming-set-11-egg-dropping-puzzle/
    /// </summary>
    public class MinEggDrop
    {
        public static int GetMinDrops(int floors, int eggs)
        {
            return GetMinDrops(floors, eggs,
                new Dictionary<string, int>());
        }
        public static int GetMinDrops(int floors, int eggs,
            Dictionary<string, int> cache)
        {
            //no more floor
            //no need for a trial
            if (floors == 0)
            {
                return 0;
            }

            //for one floor one trial
            if (floors == 1)
            {
                return 1;
            }

            //we need floors number of trial when
            //we only have one egg left
            if (eggs == 1)
            {
                return floors;
            }

            var cacheKey = $"{floors}-{eggs}";

            if(cache.ContainsKey(cacheKey))
            {
                return cache[cacheKey];
            }

            var minDrops = int.MaxValue;

            //simulate drop from 1st to current floor
            for (int i = 1; i <= floors; i++)
            {
                //broke the egg at ith floor
                var broke = GetMinDrops(i - 1, eggs - 1, cache) + 1;

                //did'nt break at ith floor
                var didntBreak = GetMinDrops(floors - i, eggs, cache) + 1;

                var min = Math.Max(didntBreak, broke);

                minDrops = Math.Min(min, minDrops);
            }

            cache.Add(cacheKey, minDrops);

            return minDrops;
        }
    }
}
