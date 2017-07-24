using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.DynamicProgramming
{
    /// <summary>
    /// Problem details below
    /// http://www.geeksforgeeks.org/dynamic-programming-set-32-word-break-problem/
    /// </summary>
    public class WordBreakProblem
    {
        public static bool CanBreak(HashSet<string> dictionary, string sentence)
        {
            return CanBreak(dictionary, sentence, sentence.Length, new Dictionary<int, bool>());
        }

        private static bool CanBreak(HashSet<string> dictionary,
            string sentence, int charIndex, Dictionary<int, bool> cache)
        {
            if (charIndex == 0)
            {
                return true;
            }

            if (cache.ContainsKey(charIndex))
            {
                return cache[charIndex];
            }

            var result = false;

            for (int i = 0; i < charIndex; i++)
            {
                if (CanBreak(dictionary, sentence, i, cache))
                {
                    if (IsMatch(dictionary, sentence.Substring(i, charIndex - i)))
                    {
                        result = true;
                        break;
                    }

                }
            }

            cache.Add(charIndex, result);

            return result;
        }

        private static bool IsMatch(HashSet<string> dictionary, string searchWord)
        {
            if (dictionary.Contains(searchWord))
            {
                return true;
            }

            return false;
        }
    }
}
