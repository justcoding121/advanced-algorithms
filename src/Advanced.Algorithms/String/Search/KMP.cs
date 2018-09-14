namespace Advanced.Algorithms.String
{
    /// <summary>
    /// Knuth–Morris–Pratt(KMP) string search implementation.
    /// </summary>
    public class KMP
    {
        /// <summary>
        /// Returns the start index of first appearance
        /// of pattern in input string.
        /// Returns -1 if no match.
        /// </summary>
        public int Search(string input, string pattern)
        {
            var matchingInProgress = false;
            var matchIndex = new int[pattern.Length];
            var j = 0;

            //create match index of chars
            //to keep track of closest suffixes in pattern that form prefix of pattern
            for (int i = 1; i < pattern.Length; i++)
            {
                //prefix don't match suffix anymore
                if (!pattern[i].Equals(pattern[j]))
                {
                    //don't skip unmatched i for next iteration
                    //since our for loop increments i
                    if (matchingInProgress)
                    {
                        i--;
                    }

                    matchingInProgress = false;

                    //move back j to the beginning of last matched char
                    j = matchIndex[j == 0 ? 0 : j - 1];
                }
                //prefix match suffix so far
                else
                {
                    matchingInProgress = true;
                    //increment index of suffix 
                    //to prefix index for corresponding char
                    matchIndex[i] = j + 1;
                    j++;
                }
            }

            matchingInProgress = false;
            //now start matching
            j = 0;

            for (var i = 0; i < input.Length; i++)
            {
                if (input[i] == pattern[j])
                {
                    matchingInProgress = true;
                    j++;

                    //match complete
                    if (j == pattern.Length)
                    {
                        return i - pattern.Length + 1;
                    }

                }
                else
                {
                    //reduce i by one so that next comparison won't skip current i
                    //which is not matching with current j
                    //since our for loop increments i
                    if (matchingInProgress)
                    {
                        i--;
                    }

                    matchingInProgress = false;

                    //jump back to closest suffix with prefix of pattern
                    if (j != 0)
                    {
                        j = matchIndex[j - 1];
                    }

                }
            }

            return -1;
        }
    }
}
