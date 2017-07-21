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
            return GetJustification(words, maxLineWidth, words.Count - 1, new Dictionary<int, int>());
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
          int nextWordIndex, Dictionary<int, int> cache)
        {
            //base case
            if (nextWordIndex == 0)
            {
                var spaceLength = maxLineWidth - words[0].Length;
                return spaceLength * spaceLength;
            }

            if(cache.ContainsKey(nextWordIndex))
            {
                return cache[nextWordIndex];
            }

            var localMin = int.MaxValue;

            //track current line progress
            var totalWordsInCurrentLine = 0;
            var totalCharacterWidthInCurrentLine = 0;

            //just simulate breaking lines between every word
            //or group of words that will fit in current line
            for (int i = nextWordIndex; i > 0 && totalCharacterWidthInCurrentLine < maxLineWidth; i--)
            {
                totalWordsInCurrentLine++;
                totalCharacterWidthInCurrentLine += words[i].Length;

                //empty space at the end of the line
                //will equal to max line width - total word width - (width of spaces between words)
                //width of spaces b/w words will equal to (total words in current line - 1)
                var emptyEndSpaceWidthOnCurrentLine = (maxLineWidth - totalCharacterWidthInCurrentLine)
                    - (totalWordsInCurrentLine - 1);

                //previos optimal result
                var prevLineMin = GetJustification(words, maxLineWidth, i - 1, cache);

                //use squares/or cubes of empty space lengths at the end of the line
                //to amplify the spaces in each line
                localMin = Math.Min(localMin , (prevLineMin 
                    + (emptyEndSpaceWidthOnCurrentLine * emptyEndSpaceWidthOnCurrentLine)));
            }

            cache.Add(nextWordIndex, localMin);

            return localMin;
        }
    }
}
