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
                case '?':
                    result = IsMatch(text, pattern, tIndex + 1, pIndex + 1, cache);
                    break;

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
