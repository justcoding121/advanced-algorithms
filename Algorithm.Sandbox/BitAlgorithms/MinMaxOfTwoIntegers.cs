using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.BitAlgorithms
{
    public class MinMaxOfTwoIntegers
    {
        /// <summary>
        /// Min without branching
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int FindMin(int a, int b)
        {
            //if a<b then -(a<b) = -1(all ones in 2s compliment binary)
            //so b ^ ((a^b) & 1111) => b ^ a ^ b => a
            //otherwise b^(a^b) & 0) => b ^ 0 => b
            return b ^ ((a ^ b) & -Convert.ToInt32(a < b));
        }

        /// <summary>
        /// Max without branching
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int FindMax(int a, int b)
        {
            //if a<b then -(a<b) = -1(all ones in 2s compliment binary)
            //so a ^ ((a^b) & 1111) => a ^ a ^ b => b
            //otherwise a^(a^b) & 0) => a ^ 0 => a
            return a ^ ((a ^ b) & -Convert.ToInt32(a < b));
        }
    }
}
