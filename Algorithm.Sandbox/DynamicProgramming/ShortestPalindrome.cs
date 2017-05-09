using Algorithm.Sandbox.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.DynamicProgramming
{
    /// <summary>
    /// https://leetcode.com/problems/shortest-palindrome/#/description
    /// </summary>
    public class ShortestPalindrome
    {
        public static string FindShortest(string input)
        {
            var jsWithZero_iValues = new List<int>();

            //find longest palindrome prefix
            FindLongestPalindrome(input,
               0, input.Length - 1,
               new AsDictionary<string, int>(),
               jsWithZero_iValues);

            var max = jsWithZero_iValues.Count > 0 ? jsWithZero_iValues.Max() : 1;

            var remaining = input.Length - max;

            var prefixToAdd = string.Concat(input.Substring(max, remaining).Reverse());

            return prefixToAdd + input;

        }

        /// <summary>
        /// DP top down
        /// </summary>
        /// <param name="input"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="cache"></param>
        /// <param name="jsWithZero_iValues">Contains all j values with 0 as i value</param>
        /// <returns></returns>
        private static int FindLongestPalindrome(string input,
            int i, int j,
            AsDictionary<string, int> cache,
            List<int> jsWithZero_iValues)
        {
            if (i >= input.Length || j < 0 || i > j)
            {
                return 0;
            }

            if (i == j)
            {
                return 1;
            }

            var cacheKey = string.Concat(i, j);

            if (cache.ContainsKey(cacheKey))
            {
                return cache[cacheKey];
            }

            var longestLengthA = 0;

            if (input[i] == input[j])
            {
                longestLengthA = FindLongestPalindrome(input, i + 1, j - 1, cache, jsWithZero_iValues);

                //for continuity, verify that
                //expected palindrome length between i & j match
                //palindrome length
                if (longestLengthA + 1 == j - i)
                {
                    longestLengthA = longestLengthA + 2;
                }

                //keep track of palindromes starting at i = 0
                if (i == 0)
                {
                    jsWithZero_iValues.Add(j);
                }
            }

            var longestLengthB = FindLongestPalindrome(input, i, j - 1, cache, jsWithZero_iValues);
            var longestLengthC = FindLongestPalindrome(input, i + 1, j, cache, jsWithZero_iValues);

            var results = new int[] { longestLengthA, longestLengthB, longestLengthC };

            var longest = results.Max();

            cache.Add(cacheKey, longest);

            return longest;
        }
    }
}
