using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.BitAlgorithms
{
    /// <summary>
    /// Problem details below
    /// http://www.geeksforgeeks.org/divisibility-9-using-bitwise-operators/
    /// </summary>
    public class IsMultipleOfNine
    {
        //Any number N can be expressed as some multiple of 8 and remainder
        //N = 8k + r (here r is N % 8, so from 0-7)
        //or, N = 9k - k + r
        //Clearly, 9k is divisible by 9.
        //So, if N is divisible by 9, then (-k + r) should be divisible by 9
        //or -(-k + r) should also be divisible by 9 => (k-r) should be divisble by 9
        //where, k = N/8 = N>>3 and r = N % 8 = N&7
        public static bool IsTrue(int x)
        {
            if (x == 9)
            {
                return true;
            }

            if (x < 9)
            {
                return false;
            }

            return IsTrue((x >> 3) - (x & 7));
        }
    }
}
