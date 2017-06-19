using System.Collections.Generic;

namespace Algorithm.Sandbox.DynamicProgramming
{
    /// <summary>
    /// Problem statement in detail below
    /// http://www.geeksforgeeks.org/dice-throw-problem/
    /// </summary>
    public class DiceThrow
    {
        public static int WaysToGetSum(int sum, int dice, int face)
        {
            return WaysToGetSum(sum, dice, face, new Dictionary<string, int>());
        }

        /// <summary>
        /// DP top down
        /// </summary>
        /// <param name="sum"></param>
        /// <param name="dice"></param>
        /// <param name="face"></param>
        /// <param name="cache"></param>
        /// <returns></returns>
        private static int WaysToGetSum(int sum, int dice, int face,
            Dictionary<string, int> cache)
        {
            var cacheKey = string.Concat(sum, dice);

            if(cache.ContainsKey(cacheKey))
            {
                return cache[cacheKey];
            }

            //reached sum and dice got exhausted
            //we reached the solution
            if (sum == 0 && dice == 0)
            {
                return 1;
            }

            //cannot pick so no solution
            if (sum <= 0 || dice <=0)
            {
                return 0;
            }

            var ways = 0;

            //for all face values 
            for (int f = face; f >= 1; f--)
            {
                //only if sum is less than current face value
                if (f <= sum)
                {
                    //if we pick a face value, then we can get rid of that dice
                    ways += WaysToGetSum(sum - f, dice - 1, face);
                }
            }

            cache.Add(cacheKey, ways);

            return ways;
        }
    }
}
