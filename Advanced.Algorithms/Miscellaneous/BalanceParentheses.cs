using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advanced.Algorithms.Miscellaneous
{
    public class BalanceParentheses
    {
        /// <summary>
        /// O(n) time complexity
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string Balance(string text)
        {
            var result = new StringBuilder();

            for (int i = 0; i < text.Length; i++)
            {
                var progress = Balance(text, i, result);

                if (progress == i)
                {
                    continue;
                }

                i = progress;
            }

            return result.ToString();
        }

        /// <summary>
        /// Recursively visit child parentheses and add balanced items to result
        /// </summary>
        /// <param name="text"></param>
        /// <param name="i"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static int Balance(string text, int i, StringBuilder result)
        {
            if (i >= text.Length)
            {
                return text.Length;
            }

            //close parentheses
            if (text[i] == ')')
            {
                return i;
            }

            //a character
            if (text[i] != '(' && text[i] != ')')
            {
                result.Append(text[i]);
                return Balance(text, i + 1, result);
            }

            //open parenthesis

            var subResult = new StringBuilder();
            while (i + 1 < text.Length && text[i] == '(')
            {
                i = Balance(text, i + 1, subResult);

                if (i < text.Length && text[i] == ')')
                {
                    result.Append("(" + subResult.ToString() + ")");
                }
                else
                {
                    result.Append(subResult.ToString());
                }

                subResult.Clear();
                i++;
            }

            return i;
        }
    }
}
