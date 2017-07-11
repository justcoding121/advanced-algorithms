using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.DynamicProgramming
{
    /// <summary>
    /// Problem details below
    /// https://leetcode.com/problems/wildcard-matching/#/description
    /// </summary>
    public class WildCardMatching
    {
        public static bool IsMatch(string text, string pattern)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException(text);
            }

            if (string.IsNullOrEmpty(pattern))
            {
                throw new ArgumentNullException(pattern);
            }

            return IsMatch(text, pattern, 0, 0);
        }

        private static bool IsMatch(string text, string pattern, int tIndex, int pIndex)
        {
            if (tIndex == text.Length
                && pIndex == pattern.Length)
            {
                return true;
            }

            if (pIndex == pattern.Length && pattern[pIndex - 1] == '*')
            {
                return true;
            }

            if (tIndex == text.Length || pIndex == pattern.Length)
            {
                return false;
            }

            switch (pattern[pIndex])
            {
                case '*':
                    return IsMatch(text, pattern, tIndex, pIndex + 1)
                        || IsMatch(text, pattern, tIndex + 1, pIndex)
                        || IsMatch(text, pattern, tIndex + 1, pIndex + 1);

                case '?':
                    return IsMatch(text, pattern, tIndex + 1, pIndex + 1);

                default:
                    if (text[tIndex] != pattern[pIndex])
                    {
                        return false;
                    }

                    return IsMatch(text, pattern, tIndex + 1, pIndex + 1);
            }
        }
    }
}
