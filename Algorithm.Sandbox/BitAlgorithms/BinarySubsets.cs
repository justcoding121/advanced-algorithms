using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.BitAlgorithms
{
    /// <summary>
    /// Generating all the binary subsets of an integer
    /// Find the subset of all set bits in given integer 
    /// </summary>
    public class BinarySubsets
    {
        internal static List<int> GetSubsets(int n)
        {
            var result = new List<int>();

            //AND i-1 with original number in each iteration
            //our goal is to keep unset bits as it is
            //but to find all sets formed by the set bit being turned off and on
            for (int i = n; i != 0; i = (i - 1) & n)
            {
                result.Add(i);
            }

            //i is 0 at the end
            result.Add(0);

            return result;
        }
    }
}
