using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.DynamicProgramming
{
    /// <summary>
    /// Problem details below
    /// http://www.geeksforgeeks.org/dynamic-programming-set-13-cutting-a-rod/
    /// </summary>
    public class CuttingRod
    {
        public static int GetMaxProfit(int[] lengths, int[] prices)
        {
            var priceByLength = new Dictionary<int, int>();

            for (int i = 0; i < lengths.Length; i++)
            {
                priceByLength.Add(lengths[i], prices[i]);
            }

            return GetMaxProfit(priceByLength, lengths.Length, new Dictionary<int, int>());
        }

        private static int GetMaxProfit(Dictionary<int, int> priceByLength,
            int curLength, Dictionary<int, int> cache)
        {
            if(curLength < 0)
            {
                return int.MinValue;
            }

            if(curLength == 0)
            {
                return 0;
            }

            if(cache.ContainsKey(curLength))
            {
               return cache[curLength];
            }

            var max = 0;

            //get max of cutting at current length
            for (int i = 1; i <= curLength; i++)
            {
                max = Math.Max(max, GetMaxProfit(priceByLength, curLength - i, cache) 
                    + priceByLength[i]);
            }

            cache.Add(curLength, max);

            return max;
        }
    }
}
