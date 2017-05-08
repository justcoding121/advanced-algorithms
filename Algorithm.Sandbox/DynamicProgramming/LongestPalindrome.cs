using Algorithm.Sandbox.DataStructures;
using System;

namespace Algorithm.Sandbox.DynamicProgramming
{
    public class LongestPalindrome
    {
        public int FindPalindrome(string input)
        {
            return FindPalindrome(input, 0, input.Length - 1, new AsDictionary<string, int>());
        }

        /// <summary>
        /// DP top down
        /// </summary>
        /// <param name="input"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        private int FindPalindrome(string input, 
            int i, int j,
            AsDictionary<string, int> cache)
        {
            if (i == j)
            {
                return 1;
            }

            var cacheKey = string.Concat(i, j);

            if (cache.ContainsKey(cacheKey))
            {
                return cache[cacheKey];
            }

            var longestLength = 0;

            if (input[i] == input[j])
            {
                longestLength = FindPalindrome(input, i + 1, j - 1, cache);

                //for continuity, verify length between i & j matches 
                //palindrome length
                if (longestLength + 1 == j - i)
                {
                    return longestLength + 2;
                }

                return longestLength;
            }

            longestLength = Math.Max(FindPalindrome(input, i, j - 1, cache),
                FindPalindrome(input, i + 1, j, cache));

            cache.Add(cacheKey, longestLength);

            return longestLength;
        }
    }
}
