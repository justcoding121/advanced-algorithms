using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.DynamicProgramming.Palindrome
{
    /// <summary>
    /// Problem statement here
    /// http://www.geeksforgeeks.org/minimum-number-deletions-make-string-palindrome/
    /// </summary>
    public class PalindromeMinDeletion
    {
        public static int GetMinDeletion(string input)
        {
            var longPalindromeSequence = FindLongestPalindrome(input,
            0, input.Length - 1,
            new Dictionary<string, int>());

            return input.Length - longPalindromeSequence;
        }

        private static int FindLongestPalindrome(string input,
            int i, int j,
            Dictionary<string, int> cache)
        {
            if (i > j)
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

            if (input[i] == input[j])
            {
                return FindLongestPalindrome(input, i + 1, j - 1, cache) + 2;
            }

            var longest = Math.Max(FindLongestPalindrome(input, i, j - 1, cache),
                    FindLongestPalindrome(input, i + 1, j, cache));

            cache.Add(cacheKey, longest);

            return longest;
        }
    }
}
