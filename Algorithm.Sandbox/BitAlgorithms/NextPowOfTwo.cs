using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.BitAlgorithms
{
    /// <summary>
    /// Problem details below
    /// http://www.geeksforgeeks.org/smallest-power-of-2-greater-than-or-equal-to-n/
    /// </summary>
    public class NextPowOfTwo
    {
        internal static int Next(int v)
        {
            if (v == 0)
            {
                return 1;
            }

            //is a power of two already
            if ((v & (v - 1)) == 0)
            {
                return v;
            }

            var result = 1;

            //shift result one left until y is 0
            var y = v;
            while (y > 0)
            {
                result = result << 1;
                y = y >> 1;
            }

            return result;
        }
    }
}
