using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.DynamicProgramming
{
    /// <summary>
    /// Problem statement below
    /// http://www.geeksforgeeks.org/dynamic-programming-set-31-optimal-strategy-for-a-game/
    /// </summary>
    public class OptimalGameStrategy
    {
        public static int GetStrategy(int[] input)
        {
            var result = GetStrategyA(input, 0, input.Length - 1, 
                new Dictionary<string, OGS_Total>());

            return result.ProfitA;
        }

        internal static OGS_Total GetStrategyA(int[] input, int i, int j, 
            Dictionary<string, OGS_Total> cache)
        {
            if (i == j)
            {
                return new OGS_Total() { ProfitA = input[i] };
            }

            var cacheKey = $"{i}-{j}";

            if (cache.ContainsKey(cacheKey))
            {
                return cache[cacheKey];
            }

            var pickLeft = GetStrategyB(input, i + 1, j, cache);
            var pickRight = GetStrategyB(input, i, j - 1, cache);

            OGS_Total picked;

            if(pickLeft.ProfitA + input[i] > pickRight.ProfitA + input[j])
            {
                pickLeft.ProfitA += input[i];
                picked = pickLeft;
            }
            else
            {
                pickRight.ProfitA += input[j];
                picked = pickRight;
            }

            cache.Add(cacheKey, new OGS_Total() {
                ProfitA = picked.ProfitA,
                ProfitB = picked.ProfitB
            });


            return picked;
          
        }

        internal static OGS_Total GetStrategyB(int[] input, int i, int j,
            Dictionary<string, OGS_Total> cache)
        {
            if (i == j)
            {
                return new OGS_Total() { ProfitB = input[i] };
            }

            var cacheKey = $"{i}-{j}";

            if(cache.ContainsKey(cacheKey))
            {
                return cache[cacheKey];
            }

            var pickLeft = GetStrategyA(input, i + 1, j, cache);
            var pickRight = GetStrategyA(input, i, j - 1, cache);

            OGS_Total picked;

            if (pickLeft.ProfitB + input[i] > pickRight.ProfitB + input[j])
            {
                pickLeft.ProfitB += input[i];
                picked = pickLeft;
            }
            else
            {
                pickRight.ProfitB += input[j];
                picked = pickRight;
            }

            cache.Add(cacheKey, new OGS_Total()
            {
                ProfitA = picked.ProfitA,
                ProfitB = picked.ProfitB
            });


            return picked;
        }

        internal struct OGS_Total
        {
            public int ProfitA;
            public int ProfitB;
        }
    }
}
