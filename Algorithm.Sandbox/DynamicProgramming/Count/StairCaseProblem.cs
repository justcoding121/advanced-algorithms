using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.DynamicProgramming
{
    /// <summary>
    /// Problem details below
    /// http://www.geeksforgeeks.org/count-ways-reach-nth-stair/
    /// </summary>
    public class StairCaseProblem
    {
        public static int CountWays(int steps)
        {
            return CountWays(steps, new Dictionary<int, int>());
        }

        private static int CountWays(int steps, Dictionary<int, int> cache)
        {
            if(steps < 0)
            {
                return 0;
            }

            if (steps == 0)
            {
                return 1;
            }

            if (cache.ContainsKey(steps))
            {
                return cache[steps];
            }

            var result = CountWays(steps - 1) + CountWays(steps - 2);

            cache.Add(steps, result);

            return result;
        }
    }
}
