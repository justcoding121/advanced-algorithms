using Algorithm.Sandbox.DataStructures;
using System.Linq;

namespace Algorithm.Sandbox.DynamicProgramming
{
    /// <summary>
    /// Problem statement in detail below
    /// http://www.geeksforgeeks.org/dynamic-programming-set-12-longest-palindromic-subsequence/
    /// </summary>
    public class LongestPalindrome
    {
        public int FindPalindrome(string input)
        {
            return FindLongestPalindrome(input, 0, input.Length - 1, new AsDictionary<string, int>());
        }

        /// <summary>
        /// DP top down
        /// </summary>
        /// <param name="input"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        private int FindLongestPalindrome(string input,
            int i, int j,
            AsDictionary<string, int> cache)
        {
            if (i >= input.Length || j < 0 || i > j)
            {
                return 0;
            }

            if (i == j)
            {
                return 1;
            }

            var cacheKey = string.Concat(i , j);

            if (cache.ContainsKey(cacheKey))
            {
                return cache[cacheKey];
            }

            var longestLengthA = 0;

            if (input[i] == input[j])
            {
                longestLengthA = FindLongestPalindrome(input, i + 1, j - 1, cache); 

                //for continuity, verify that
                //expected palindrome length between i & j match
                //palindrome length
                if (longestLengthA + 1 == j - i)
                {
                    longestLengthA = longestLengthA + 2;
                }
            }

            var longestLengthB = FindLongestPalindrome(input, i, j - 1, cache);
            var longestLengthC = FindLongestPalindrome(input, i + 1, j, cache);

            var results = new int[] { longestLengthA, longestLengthB, longestLengthC };

            var longest = results.Max();

            cache.Add(cacheKey, longest);

            return longest;
        }
    }
}
