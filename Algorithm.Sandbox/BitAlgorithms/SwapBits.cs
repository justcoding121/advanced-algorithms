using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.BitAlgorithms
{
    /// <summary>
    /// Problem details below
    /// http://www.geeksforgeeks.org/swap-bits-in-a-given-number/
    /// </summary>
    public class SwapBits
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int Swap(int input, int i, int j, int n)
        {
            //Mask to isolate swap bits 
            var iSwapMask = (int.MaxValue << i) & (int.MaxValue >> (32 - i - n - 1));
            var jSwapMask = (int.MaxValue << j) & (int.MaxValue >> (32 - j - n - 1));

            //update mask bits with what appears in inpute and shift to new positions
            var iSwap = (iSwapMask & input) << (j - i);
            var jSwap = (jSwapMask & input) >> (j - i);

            //set all bits affected by swap to zero
            input = input & ~(iSwapMask | jSwapMask);

            //now apply the swap bits
            return input | iSwap | jSwap;

        }
    }
}
