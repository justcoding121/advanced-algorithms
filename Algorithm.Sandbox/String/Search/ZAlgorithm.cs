using System;
/// <summary>
/// A Z algorithm implementation for pattern search
/// </summary>
namespace Algorithm.Sandbox.String.Search
{
    public class ZAlgorithm
    {
        /// <summary>
        /// Returns the start index of first appearance
        /// of pattern in input string
        /// returns -1 if no match
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public int Search(string input, string pattern)
        {
            var z = Z(pattern + input, pattern.Length);

            for (int i = 0; i < z.Length; i++)
            {
                //if match length eqyals pattern Length + separator length
                if (z[i] == pattern.Length)
                {
                    //substract pattern length and separator length
                    return i - pattern.Length;
                }
            }

            return -1;
        }

        /// <summary>
        /// The z function computes the length of matching prefix at each char 
        /// in given input
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private int[] Z(string input, int patternLength)
        {
            var result = new int[input.Length];

            int prefixIndex = 0;
            for (int i = 1; i < input.Length; i++)
            {
                var k = i;

                //increment prefixIndex (count of matching chars)
                while (k < input.Length && prefixIndex <= patternLength
                    && input[prefixIndex] == input[k])
                {
                    prefixIndex++;
                    k++;
                }

                //assign result
                result[i] = prefixIndex;

                //is prefixIndex > 1 we have a choice to use earlier values 
                //in result
                if (prefixIndex > 1)
                {
                    //z-box left & right
                    var left = i;
                    var right = i + prefixIndex;

                    prefixIndex = 1;
                    for (int m = left + 1; m < right; m++)
                    {
                        if (m + result[prefixIndex] < right)
                        {
                            result[m] = result[prefixIndex];
                            prefixIndex++;
                        }
                        else
                        {
                            //move left end of z box to current element
                            left = m;
                            prefixIndex = result[prefixIndex];

                            //cannot exceed size of input
                            if (m + prefixIndex < input.Length)
                            {
                                //increment right end of Z box as far as match goes
                                while (right < input.Length 
                                    && prefixIndex <= patternLength
                                    && input[prefixIndex] == input[right])
                                {
                                    right++;
                                    prefixIndex++;
                                }

                                result[m] = prefixIndex;
                                prefixIndex = 1;
                            }
                            else
                            {
                                //since i is assigned with right below
                                //and i is incremented by for loop do a right--
                                right--;
                                break;
                            }
                        }
                    }

                    //move i to end of z box
                    //since i is incremented by for loop do a right - 1
                    i = right - 1;
                }

                //reset prefix Index
                prefixIndex = 0;
            }

            return result;
        }
    }
}
