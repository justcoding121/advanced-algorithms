using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.DynamicProgramming.Count
{
    /// <summary>
    /// Problem details below
    /// http://www.geeksforgeeks.org/count-number-of-ways-to-cover-a-distance/
    /// </summary>
    public class WaysToCoverDistance
    {
        public static int GetWays(int dist)
        {
            return GetWays(dist, new Dictionary<int, int>());
        }

        private static int GetWays(int dist, Dictionary<int, int> cache)
        {
            if (dist < 0)
            {
                return 0;
            }

            if (dist == 0)
            {
                return 1;
            }

            if(cache.ContainsKey(dist))
            {
                return cache[dist];
            }

            var result = GetWays(dist - 1) + GetWays(dist - 2) + GetWays(dist - 3);

            cache.Add(dist, result);

            return result;
        }
    }
}
