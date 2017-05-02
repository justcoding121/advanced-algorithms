using System;

namespace Algorithm.Sandbox.String
{
    /// <summary>
    /// A Manacher's longest palindrome implementation
    /// </summary>
    public class ManachersPalindrome
    {
        /// <summary>
        /// Find the longest palindrome in linear time
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public int FindLongestPalindrome(string input)
        {
            var palindromeLengths = new int[input.Length];

            int left = -1, right = 1;
            int length = 1;

            //loop through each char
            for (int i = 0; right < input.Length; i++)
            {
                //terminate if end of input
                while (left >= 0 && right < input.Length)
                {
                    if (input[left] == input[right])
                    {
                        left--;
                        right++;
                        length += 2;
                    }
                    else
                    {
                        //end of current palindrome
                        break;
                    }

                }

                var @continue = false;

                //set length of current palindrome
                palindromeLengths[i] = length;

                //use mirror values on left side of palindrome
                //to fill palindrome lengths on right side of palindrome
                //so that we can save computations
                if (right > i + 1)
                {
                    var l = i - 1;
                    var r = i + 1;

                    //start from current palindrome center
                    //all the way to right end of current palindrome
                    while (r < right)
                    {
                        //find mirror char palindrome length
                        var mirrorLength = palindromeLengths[l];

                        //mirror palindrome left end exceeds
                        //current palindrom left end
                        if (l - (mirrorLength / 2) < left + 1)
                        {
                            //set length equals to maximum
                            //we can reach and then continue exploring
                            palindromeLengths[r] = (2 * (l - (left + 1))) + 1;
                            r++;
                            l--;
                            continue;
                        }
                        //mirror palindrome is totally contained
                        //in our current palindrome
                        else if (palindromeLengths[l] == 1
                            || (l - (mirrorLength / 2) > left + 1
                            && r + (mirrorLength / 2) < right - 1))
                        {
                            //so just set the value and continue exploring
                            palindromeLengths[r] = palindromeLengths[l];
                            r++;
                            l--;
                            continue;
                        }
                        //mirror palindrome exactly fits inside right side
                        //of current palindrome
                        else 
                        {
                            //set length equals to maximum
                            //and then continue exploring in main loop
                            length = palindromeLengths[l];

                            //continue to main loop
                            //update state values to skip
                            //already computed values
                            i = r;
                            left = i - (length / 2) - 1;
                            right = i + (length / 2) + 1;

                            @continue = true;
                            break;

                        }

                    }

                    //to compensate for loop i--
                    i = r - 1;

                }

                //continue to main loop
                //states values are already set
                if (@continue)
                {
                    continue;
                }
                else
                {
                    //reset as usual
                    left = i;
                    right = i + 2;
                    length = 1;
                }
            }

            return FindMax(palindromeLengths);
        }

        /// <summary>
        /// returns the max index in given int[] array
        /// </summary>
        /// <param name="palindromeLengths"></param>
        /// <returns></returns>
        private int FindMax(int[] palindromeLengths)
        {
            var maxIndex = -1;
            var max = int.MinValue;

            for (int i = 0; i < palindromeLengths.Length; i++)
            {
                if (max < palindromeLengths[i])
                {
                    max = palindromeLengths[i];
                    maxIndex = i;
                }
            }

            return maxIndex;

        }
    }
}
