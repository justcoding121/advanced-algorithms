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

            return IsMatch(text, pattern, 0, 0, new Dictionary<string, bool>());
        }

        /// <summary>
        /// DP top down with Memoization
        /// Just Simulate the possibilities
        /// </summary>
        /// <param name="text"></param>
        /// <param name="pattern"></param>
        /// <param name="tIndex"></param>
        /// <param name="pIndex"></param>
        /// <param name="cache"></param>
        /// <returns></returns>
        private static bool IsMatch(string text, string pattern,
            int tIndex, int pIndex,
            Dictionary<string, bool> cache)
        {
            if (tIndex == text.Length
                && pIndex == pattern.Length)
            {
                return true;
            }

            if (tIndex == text.Length 
                || pIndex == pattern.Length)
            {
                return false;
            }

            var cacheKey = $"{tIndex}-{pIndex}";

            if(cache.ContainsKey(cacheKey))
            {
                return cache[cacheKey];
            }

            var result = false;

            switch (pattern[pIndex])
            {
                //try move one search char '?' as well as text char
                case '?':
                    result = IsMatch(text, pattern, tIndex + 1, pIndex + 1, cache);
                    break;

                //3 possibilities with search char '*'
                //move search char & move text char
                //move search char & and stay at same text char (since '*' can be for empty search char)
                //don't move search char, but move one text char
                case '*':
                    if (pIndex == pattern.Length - 1)
                    {
                        result = true;
                        break;
                    }

                    result = IsMatch(text, pattern, tIndex, pIndex + 1, cache)
                        || IsMatch(text, pattern, tIndex + 1, pIndex, cache)
                        || IsMatch(text, pattern, tIndex + 1, pIndex + 1, cache);
                    break;

                //try move one search char as well as text char
                //before that do a check for exact match of search char with current text char
                default:
                    if (text[tIndex] != pattern[pIndex])
                    {
                        result = false;
                        break;
                    }

                    result = IsMatch(text, pattern, tIndex + 1, pIndex + 1, cache);
                    break;
            }

            cache.Add(cacheKey, result);

            return result;
        }
    }
}
