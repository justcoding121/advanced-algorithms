using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.DynamicProgramming
{
    /// <summary>
    /// problem details below
    /// https://leetcode.com/problems/text-justification/#/description
    /// </summary>
    public class TextJustification
    {
        public static int GetJustification(List<string> words, int maxLineWidth)
        {
            //all other lines above last line
            return GetJustificationLastLine(words, maxLineWidth, maxLineWidth, words.Count - 1);
        }

        /// <summary>
        /// Last line is left justfied; so do a greedy word pick for last line
        /// </summary>
        /// <param name="words"></param>
        /// <param name="maxLineWidth"></param>
        /// <param name="currentLinePos"></param>
        /// <param name="nextWordIndex"></param>
        /// <returns></returns>
        private static int GetJustificationLastLine(List<string> words, int maxLineWidth,
            int currentLinePos, int nextWordIndex)
        {
            var lineLength = maxLineWidth;
            var wordIndex = words.Count - 1;
            for (int i = words.Count - 1; i >= 0; i--)
            {
                if (lineLength >= maxLineWidth)
                {
                    wordIndex--;
                    lineLength -= words[wordIndex].Length;
                }
                else
                {
                    break;
                }
            }

            //all other lines above last line
            return GetJustification(words, maxLineWidth, maxLineWidth, wordIndex);
        }

        /// <summary>
        /// DP top down
        /// </summary>
        /// <param name="words"></param>
        /// <param name="maxLineWidth"></param>
        /// <param name="currentLinePos"></param>
        /// <param name="nextWordIndex"></param>
        /// <returns></returns>
        private static int GetJustification(List<string> words, int maxLineWidth,
         int currentLinePos, int nextWordIndex)
        {
            //base case
            if (nextWordIndex == -1)
            {
                return 0;
            }

            //last word of line
            if (currentLinePos == maxLineWidth)
            {
                //place word in current Line Position
                return GetJustification(words, maxLineWidth, currentLinePos - words[nextWordIndex].Length, nextWordIndex - 1) + 1;
            }
            //in between the line or first word of the line
            //have space for the word
            else if (currentLinePos >= words[nextWordIndex].Length)
            {
                //place word in current Line Position
                var withWord = GetJustification(words, maxLineWidth, currentLinePos - words[nextWordIndex].Length, nextWordIndex - 1);

                //add a space in current Line Position
                var withSpace = GetJustification(words, maxLineWidth, currentLinePos - 1, nextWordIndex);

                if (withSpace == int.MaxValue)
                {
                    return withWord;
                }

                return Math.Min(withWord, withSpace + 1);
            }
            else
            {
                //no space to add the word in current line
                //so terminate this path search
                return int.MaxValue;
            }
        }
    }
}
