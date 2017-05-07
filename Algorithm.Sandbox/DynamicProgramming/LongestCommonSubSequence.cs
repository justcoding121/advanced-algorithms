using Algorithm.Sandbox.DataStructures;

namespace Algorithm.Sandbox.DynamicProgramming
{
    public class LongestCommonSubSequence
    {
        public static string FindSequence(string a, string b)
        {
            return FindSequence(a, b, a.Length - 1, b.Length - 1, new AsDictionary<string, string>());
        }

        /// <summary>
        /// DP top down
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        private static string FindSequence(string a, string b,
            int i, int j, 
            AsDictionary<string, string> cache)
        {
            if (i < 0 || j < 0)
            {
                return string.Empty;
            }

            var cacheKey = string.Concat(i, j);

            if (cache.ContainsKey(cacheKey))
            {
                return cache[cacheKey];
            }

            if (a[i] == b[j])
            {
                return FindSequence(a, b, i - 1, j - 1, cache) + a[i];
            }

            var result1 = FindSequence(a, b, i, j - 1, cache);
            var result2 = FindSequence(a, b, i - 1, j, cache);

            var result = result1.Length > result2.Length ? result1 : result2;

            cache.Add(cacheKey, result);

            return result;
        }
    }
}
