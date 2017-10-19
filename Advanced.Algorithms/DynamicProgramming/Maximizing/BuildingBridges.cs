using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced.Algorithms.DynamicProgramming
{
    /// <summary>
    /// Problem details below
    /// http://www.geeksforgeeks.org/dynamic-programming-building-bridges/
    /// </summary>
    public class BuildingBridges
    {
        public static int GetMaxBridges(int[] sideA, int[] sideB)
        {
            var pairs = new List<Tuple<int, int>>();

            for (int i = 0; i < sideA.Length; i++)
            {
                pairs.Add(new Tuple<int, int>(sideA[i], sideB[i]));
            }

            //sort by sideB then by SideA
            pairs = pairs.OrderBy(x => x.Item2).ThenBy(x => x.Item1).ToList();

            var longest = -1;
            //now run LIS on sideA
            LIS(pairs, pairs.Count - 1, ref longest, new Dictionary<int, int>());

            return longest;
        }

        private static int LIS(List<Tuple<int, int>> input,
          int j, ref int netLongest, Dictionary<int, int> cache)
        {
            if (j == 0)
            {
                return 1;
            }

            if (cache.ContainsKey(j))
            {
                return cache[j];
            }

            var longest = 1;

            for (int i = 0; i < j; i++)
            {
                var subLongest = LIS(input, i, ref netLongest, cache);

                if (input[i].Item1 <= input[j].Item1
                    && longest < subLongest + 1)
                {
                    longest = subLongest + 1;
                }
            }

            netLongest = Math.Max(netLongest, longest);

            cache.Add(j, longest);

            return longest;

        }
    }
}
