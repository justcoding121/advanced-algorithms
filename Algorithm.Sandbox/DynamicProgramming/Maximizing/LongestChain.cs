using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.DynamicProgramming.Maximizing
{
    /// <summary>
    /// Problem details below
    /// http://www.geeksforgeeks.org/dynamic-programming-set-14-variations-of-lis/
    /// </summary>
    public class LongestChain
    {
        public static int GetLongest(List<int[]> interlocks)
        {
            var pairs = new List<Tuple<int, int>>();

            for (int i = 0; i < interlocks.Count; i++)
            {
                pairs.Add(new Tuple<int, int>(interlocks[i][0], interlocks[i][1]));
            }

            //sort by start of interlock, then by end of it
            pairs = pairs.OrderBy(x => x.Item1).ThenBy(x => x.Item2).ToList();

            var longest = -1;
            //now run LIS on End of interlock
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

                if (input[i].Item2 <= input[j].Item1
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
