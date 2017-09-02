using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.BitAlgorithms
{
    /// <summary>
    /// Problem details below
    /// http://www.geeksforgeeks.org/write-a-c-program-to-find-the-parity-of-an-unsigned-integer/
    /// </summary>
    public class ParityFinder
    {
        /// <summary>
        /// Parity is odd if total set bit count is odd, otherwise even
        /// Returs true if odd parity, otherwise returns false for even parity
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static bool Find(int n)
        {
            //init to even parity
            bool parity = false;

            while (n > 0)
            {
                parity = !parity;

                //each step will eliminate rightmost set bit
                n = n & (n - 1);
            }

            return parity;
        }
    }
}
