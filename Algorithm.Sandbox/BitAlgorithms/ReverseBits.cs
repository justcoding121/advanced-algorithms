using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.BitAlgorithms
{
    /// <summary>
    /// Problem details below
    /// http://www.geeksforgeeks.org/write-an-efficient-c-program-to-reverse-bits-of-a-number/
    /// </summary>
    public class ReverseBits
    {
        public static uint Reverse(uint x)
        {
            var count = 32;

            uint y = x;
            uint result = 0;

            while (y > 0)
            {
                result = result << 1;
                result = (result | (y & 1));
                y = y >> 1;
                count--;
            }

            result = result << count;

            return result;
        }
    }
}
