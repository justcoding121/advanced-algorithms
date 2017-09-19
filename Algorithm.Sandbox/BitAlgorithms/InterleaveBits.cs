using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.BitAlgorithms
{
    // Interleave bits of x and y, so that all of the
    // bits of x are in the even positions and y in the odd;
    // z gets the resulting Morton Number
    public class InterleaveBits
    {
        internal static int Interleave(int x, int y)
        {
            var result = 0;

            //two 16-bit numbers to a 32-bit morton number
            for (int i = 0; i <= 15; i++)
            {
                //pick bit from x
                result |=  (x & (1 << i)) << (2 * i - i);

                //pick bit by y
                result |=  (y & (1 << i)) << (2 * i - i + 1);

            }

            return result;
        }
    }
}
