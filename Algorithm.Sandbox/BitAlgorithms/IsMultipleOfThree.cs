using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.BitAlgorithms
{
    /// <summary>
    /// Problem details below
    /// http://www.geeksforgeeks.org/write-an-efficient-method-to-check-if-a-number-is-multiple-of-3/
    /// </summary>
    public class IsMultipleOfThree
    {
        /// <summary>
        /// Based on the observation that a number is multiple of 3 
        /// if difference of odd and even set bits in binary form of the number
        /// is also a multiple of 3.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static bool IsTrue(int v)
        {
            var index = 0;

            var oddCount = 0;
            var evenCount = 0;

            var isEven = true;

            var y = v;
            while (index < 32)
            {
                if (isEven)
                {
                    if ((y & 1) > 0)
                    {
                        evenCount++;
                    }

                }
                else
                {
                    if ((y & 1) > 0)
                    {
                        oddCount++;
                    }
                }

                index++;
                isEven = !isEven;
                y = y >> 1;
            }

            return Math.Abs(oddCount - evenCount) % 3 == 0;

        }
    }
}
