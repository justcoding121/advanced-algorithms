using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.DynamicProgramming
{
    /// <summary>
    /// Problem statement below
    /// http://www.geeksforgeeks.org/how-to-print-maximum-number-of-a-using-given-four-keys/
    /// </summary>
    public class PrintMaxAs
    {
        public static int GetCount(int keyPressAvailable)
        {
            //return findoptimal(keyPressAvailable);
            return GetCount(keyPressAvailable, new Dictionary<int, int>());
        }

        private static int GetCount(int keyPressAvailable,
            Dictionary<int, int> cache)
        {
            if (keyPressAvailable <= 0)
            {
                return 0;
            }

            if (keyPressAvailable == 1)
            {
                return 1;
            }

            if(cache.ContainsKey(keyPressAvailable))
            {
                return cache[keyPressAvailable];
            }

            var results = new List<int>();

            //possibility one - Key A was pressed
            results.Add(GetCount(keyPressAvailable - 1, cache) + 1);

            //possibility two - Copy, Paste with Ctrl-A, Ctrl-C and Ctrl-V
            results.Add(GetCount(keyPressAvailable - 3, cache) * 2);

            //possibility three - Keep pasting repeatedly using Ctrl-V
            //first such repetition is only possible after three key presses shown below
            //'A', Ctrl-A and Ctrl-C
            var ctrlVMax = 0;
            for (int i = 3; i < keyPressAvailable; i++)
            {
                //Get max A's with i key presses
                var maxAs = GetCount(i, cache);

                //now repeat maxA's the remaining available key press times
                //and find the max
                ctrlVMax = Math.Max(ctrlVMax, maxAs * (keyPressAvailable - i - 1));
            }

            results.Add(ctrlVMax);

            var max = results.Max();

            cache.Add(keyPressAvailable, max);

            return max;
        }
    }

}
