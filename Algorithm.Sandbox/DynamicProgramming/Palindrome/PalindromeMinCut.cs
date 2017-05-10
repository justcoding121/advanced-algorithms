using System.Collections.Generic;
using System.Linq;

namespace Algorithm.Sandbox.DynamicProgramming
{
    public class Palindrome
    {
        internal int i { get; set; }
        internal int j { get; set; }
    }

    /// <summary>
    /// Problem statement in detail below
    /// http://www.geeksforgeeks.org/dynamic-programming-set-17-palindrome-partitioning/
    /// </summary>
    public class PalindromeMinCut
    {
        public static List<Palindrome> GetMinCut(string input)
        {
            var palindromes = new List<Palindrome>();

            FindLongestPalindrome(input, 0, input.Length - 1,
                new Dictionary<string, int>(), palindromes);

            palindromes = palindromes
                            .OrderByDescending(x => x.j - x.i)
                            .ToList();

            var result = new List<Palindrome>();

            while (palindromes.Count > 0)
            {
                var current = palindromes.First();
               
                result.Add(new Palindrome() {
                    i = current.i,
                    j = current.j
                });

                palindromes.RemoveAll(x => x.i >= current.i 
                                && x.j <= current.j);

                //remove overlaps
                palindromes.RemoveAll(x => x.i >= current.i && x.i <= current.j);
                palindromes.RemoveAll(x => x.j <= current.j && x.j >= current.i);
            }

            return result.OrderBy(x => x.i).ToList();
        }

        private static int FindLongestPalindrome(string input,
            int i, int j,
            Dictionary<string, int> cache,
            List<Palindrome> palindromes)
        {
            if (i > j)
            {
                return 0;
            }

            if (i == j)
            {
                palindromes.Add(new Palindrome()
                {
                    i = i,
                    j = j
                });

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
                longestLengthA = FindLongestPalindrome(input, i + 1, j - 1, cache, palindromes);

                //for continuity, verify that
                //expected palindrome length between i & j match
                //palindrome length
                if (longestLengthA + 1 == j - i)
                {
                    longestLengthA = longestLengthA + 2;

                    //keep track of palindromes 
                    palindromes.Add(new Palindrome()
                    {
                        i = i,
                        j = j
                    });
                }

            }

            var longestLengthB = FindLongestPalindrome(input, i, j - 1, cache, palindromes);
            var longestLengthC = FindLongestPalindrome(input, i + 1, j, cache, palindromes);

            var results = new int[] { longestLengthA, longestLengthB, longestLengthC };

            var longest = results.Max();

            cache.Add(cacheKey, longest);

            return longest;
        }
    }
}
