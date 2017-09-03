using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.BitAlgorithms
{
    /// <summary>
    /// A number is Sparse if there are no two adjacent 1s in its binary representation. 
    /// For example 5 (binary representation: 101) is sparse, but 6 (binary representation: 110)
    /// is not sparse.Given a number x, find the smallest Sparse number
    /// which greater than or equal to x.
    /// Problem details below
    /// http://www.geeksforgeeks.org/given-a-number-find-next-sparse-number/
    /// </summary>
    public class NextSparseNumber
    {
        /// <summary>
        /// Start traversing the binary from least significant bit.
        /// a) If we get two adjacent 1's such that next (or third) 
        ///     bit is not 1, then
        ///         (i)  Make all bits after this 1 (including this 1) to last bit as 0. 
        ///         (ii) Set third bit to 1 and continue from third bit step a
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static int Next(int x)
        {
            //start from second last bit i
            var mask = 1 << 1;

            //set i to second last bit index
            for (int i = 30; i >= 0; i--)
            {
                //if i-1==0 && i==1 && i+1==1
                if ((x & (mask << 1)) == 0 && (x & mask) > 0 && (x & (mask >> 1)) > 0)
                {
                    //erase all bits to the right of i including i
                    var eraseMask = mask;
                    for (int j = i; j >= 0; j--)
                    {
                        x = x & ~eraseMask;
                        eraseMask >>= 1;
                    }

                    //set i-1
                    x = x | (mask << 1);
                }

                //shift mask left by one
                mask <<= 1;
            }

            return x;
        }
    }
}
