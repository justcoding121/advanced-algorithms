using Algorithm.Sandbox.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.DynamicProgramming
{
    public class CountDecodings
    {
        public static int Count(string input)
        {
            return Count(input, input.Length, new AsDictionary<int, int>());
        }

        private static int Count(string input, int i,
            AsDictionary<int, int> cache)
        {
            if(cache.ContainsKey(i))
            {
                return cache[i];
            }

            if (i <=1)
            {
                return 1;
            }

            var result = Count(input, i - 1, cache);

            if (int.Parse(input.Substring(i - 2, 2)) < 27)
            {
                result += Count(input, i - 2, cache);
            }

            cache.Add(i, result);

            return result;
        }
    }
}
