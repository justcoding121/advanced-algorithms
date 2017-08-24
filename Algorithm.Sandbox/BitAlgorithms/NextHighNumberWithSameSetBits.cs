using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.BitAlgorithms
{
    /// <summary>
    /// Problem details below
    /// http://www.geeksforgeeks.org/next-higher-number-with-same-number-of-set-bits/
    /// </summary>
    public class NextHighNumberWithSameSetBits
    {
        //assume x = 156 (10011100), the result should be 163 (10100011)
        //important thing is to move left the MSB
        public static int Find(int x)
        {
            //rightOne => 00000100
            var rightOne = x & -x;

            //result = 10011100 + 00000100
            //result = 10100000 
            var result = x + rightOne;

            //okay now we almost got the result
            //now we need to move the remaining 1s in x to the rightmost

            //get the count of right most zeros in x
            //x = 10011100 => rightmostZerosCount = 2
            var rightmostZerosCount = 0;
            var y = x;
            while ((y & 1) == 0)
            {
                rightmostZerosCount++;
                y = y >> 1;
            }

            //now we want to have mask with rightmost bits as ones
            //ie we need x = 10011100 => 000000011
            var rightShifted = BitHacks.GetRightmostSubBitsStartingWithAnUnsetBit(x >> (rightmostZerosCount + 1));

            //10100000 | 00000011 => 10100011
            return result | rightShifted;

        }
    }
}
