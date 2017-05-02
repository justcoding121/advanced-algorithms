namespace Algorithm.Sandbox.String
{
    /// <summary>
    /// A Manacher's lingest palindrome implementation
    /// </summary>
    public class ManachersPalindrome
    {
        /// <summary>
        /// Find the longest palindrome
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public int FindLongestPalindrome(string input)
        {
            var palindromeLengths = new int[input.Length];

            int j = -1, k = 1;
            var currentLength = 1;

            for (int i = 0; i < input.Length; i++)
            {
                while (j >= 0 && k < input.Length)
                {
                    if (input[j] == input[k])
                    {
                        j--;
                        k++;
                        currentLength += 2;
                    }
                    else
                    {
                        break;
                    }

                }

                palindromeLengths[i] = currentLength;

                //use pre-computed mirror values
                if (i + 1 < k)
                {
                    var longestCenter = i + 1;
                    var l = i - 1;

                    for (int m = i + 1; m < k; m++)
                    {
                        if (m + (palindromeLengths[l] / 2) > k)
                        {
                            palindromeLengths[m] = palindromeLengths[l]
                                - 2 * ((m + (palindromeLengths[l] / 2)) - k);
                        }
                        else
                        {
                            palindromeLengths[m] = palindromeLengths[l];
                        }

                        l--;

                        if (palindromeLengths[m]
                            > palindromeLengths[longestCenter])
                        {
                            longestCenter = m;
                        }

                    }

                    //set values to save computation for values already known
                    j = longestCenter - (palindromeLengths[longestCenter] / 2);
                    k = longestCenter + (palindromeLengths[longestCenter] / 2);
                    currentLength = palindromeLengths[longestCenter];

                    //substract one to compensate for loop ++
                    i = longestCenter - 1;

                    continue;
                }

                j = i;
                k = i + 2;
                currentLength = 1;
            }

            return -1;
        }
    }
}
