using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.BitAlgorithms
{
    /// <summary>
    /// Swap consecutive odd and even bits from right
    /// Problem details below
    /// http://www.geeksforgeeks.org/swap-all-odd-and-even-bits/
    /// </summary>
    public class SwapOddEvenBits
    {
        public static int Swap(int input)
        {
            var x = input >> 1;
            var y = input << 1;

            var oddMask = createOddMask();
            var evenMask = createEvenMask();

            return (x & oddMask) | (y & evenMask);
        }

        private static int createOddMask()
        {
            var mask = 1;
            var result = 0;

            for (int i = 0; i < 32; i++)
            {
                if (BitHacks.IsEven(i))
                {
                    result = result | mask;
                }

                mask = mask << 1;
            }


            return result;
        }

        private static int createEvenMask()
        {
            var mask = 1;
            var result = 0;

            for (int i = 0; i < 32; i++)
            {
                if (!BitHacks.IsEven(i))
                {
                    result = result | mask;
                }

                mask = mask << 1;
            }

            return result;
        }
    }
}
