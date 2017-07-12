using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox
{
    /// <summary>
    /// Problem details below
    /// http://www.geeksforgeeks.org/check-whether-a-given-string-is-an-interleaving-of-two-other-given-strings-set-2/
    /// </summary>
    public class StringInterleaving
    {
        public static bool IsInterleaved(string a, string b, string c)
        {
            return IsInterleaved(a, b, c, 
                a.Length - 1, b.Length - 1, c.Length - 1,
                new Dictionary<string, bool>());
        }


        public static bool IsInterleaved(string a, string b, string c,
            int aIndex, int bIndex, int cIndex, Dictionary<string, bool> cache)
        {
            var cacheKey = $"{aIndex}-{bIndex}-{cIndex}";

            if(cache.ContainsKey(cacheKey))
            {
                return cache[cacheKey];
            }

            if (cIndex == -1)
            {
                return true;
            }

            var result = false;

            if (aIndex >= 0 && c[cIndex] == a[aIndex])
            {
                result = IsInterleaved(a, b, c, aIndex - 1, bIndex, cIndex - 1, cache);
            }

            if (bIndex >= 0 && c[cIndex] == b[bIndex])
            {
                result = result || IsInterleaved(a, b, c, aIndex, bIndex - 1, cIndex - 1, cache);
            }

            cache.Add(cacheKey, result);

            return result;
        }
    }
}
