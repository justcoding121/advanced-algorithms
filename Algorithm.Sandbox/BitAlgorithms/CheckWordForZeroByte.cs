using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.BitAlgorithms
{
    /// <summary>
    /// checks if any 8-bit byte in 32-bit word is 0
    /// </summary>
    public class CheckWordForZeroByte
    {
        public static bool HasZeroBytes(int x) 
        {
            //mask ...01111111
            var mask = 0x7F7F7F7F;

            //if x has a bit set in any byte 
            //it will cause the msb to be set during addition with mask
            //so result will be zero at the end
            return ~(((x & mask) + mask | mask)) != 0;

            //Examples below

            //let x = 00000010
            // ~(((x & mask) + mask | mask) | mask)
            //=>~(((00000010 & 01111111) + 01111111 | 01111111))
            //=>~(((00000010 + 01111111 | 01111111))
            //=>~(((10000000 | 011111111))
            //=>~11111111
            //=>0 
            //0!=0 => false

            //let x = 00000000
            // ~(((x & mask) + mask | mask) | mask)
            //=>~(((00000000 & 01111111) + 01111111 | 01111111))
            //=>~(((00000000 + 01111111 | 01111111))
            //=>~(((01111111 | 011111111))
            //=>~ 01111111
            //=>10000000 
            //10000000 !=0 => true


        }
    }
}
