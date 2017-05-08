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
            var prefixPalindromes = new List<int>();

            var longest = FindLongest(input,
                0, input.Length - 1,
                prefixPalindromes);

            var max = prefixPalindromes.Max();

            var remaining = input.Length - max;

            return string.Concat(input.Substring(max, remaining).Reverse(), input);

        }

        private static int FindLongest(string input,
            int i, int j,
            List<int> prefixPalindromeRightEnds)
        {
            if (i == j)
            {
                return 1;
            }

            if (input[i] == input[j])
            {
                var result = FindLongest(input, i + 1, j - 1, prefixPalindromeRightEnds);

                if (result + 1 == j - i)
                {
                    if (i == 0)
                    {
                        prefixPalindromeRightEnds.Add(j);
                    }

                    return result + 2;
                }

                return result;
            }

            return Math.Max(FindLongest(input, i + 1, j, prefixPalindromeRightEnds),
                FindLongest(input, i, j - 1, prefixPalindromeRightEnds));
        }
    }
}
