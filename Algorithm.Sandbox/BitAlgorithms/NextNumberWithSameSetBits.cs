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
    public class NextNumberWithSameSetBits
    {
        //assume x = 156 (10011100), the result should be 163 (10100011)
        //important thing is to move left the first set bit after MSB
        public static int NextHigh(int x)
        {
            //rightOne => 00000100
            var rightOne = x & -x;

            //result = 10011100 + 00000100
            //result = 10100000 
            var result = x + rightOne;

            var y = x;
            //now lets move the remaining ones to right
            while ((y & 1) == 0)
            {
                y >>= 1;
            }
            var oneCount = 0;
            while ((y & 1) == 1)
            {
                oneCount++;
                y >>= 1;
            }

            oneCount--;
            //move ones to end
            while (oneCount > 0)
            {
                result = result | (1 << (oneCount - 1));
                oneCount--;
            }

            return result;
        }

        //assume x = 163 (10100011), the result should be 156 (10011100)
        //important thing is to move left the first set bit after MSB
        public static int NextSmall(int x)
        {

            //find the position of MSB after last ones and right shift it one bit to right
            //example 10100011 => position = 5
            var y = x;
            var lastSetBitCount = 0;
            int index = 0;
            while ((y & 1) > 0)
            {
                index++;
                lastSetBitCount++;
                y >>= 1;
            }
            while ((y & 1) == 0)
            {
                index++;
                y >>= 1;
            }

            //erase and move bit to right 
            // 10100011 => 10010011
            var result = x & ~(1 << index) | (1 << (index - 1));

            //now our goal is to move last set bits close to y
            //ie => 10010011 => 10011100
            index -= 2;
            while (lastSetBitCount > 0)
            {
                result = result | (1 << index);
                lastSetBitCount--;
                index--;
            }
            while (index >= 0)
            {
                result = result & ~(1 << index);
                index--;
            }

            return result;
        }
    }
}
