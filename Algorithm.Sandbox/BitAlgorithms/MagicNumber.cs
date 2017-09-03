using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.BitAlgorithms
{
    /// <summary>
    /// Problem details below
    /// http://www.geeksforgeeks.org/find-nth-magic-number/
    /// </summary>
    public class MagicNumber
    {
        //If we notice carefully the magic numbers can be represented as 001, 010, 011, 100, 101, 110 etc, 
        //where 001 is 0*pow(5,3) + 0*pow(5,2) + 1*pow(5,1). 
        //So basically we need to add powers of 5 for each bit set in given integer n.
        public static int FindNth(int n)
        {
            int exp = 1;
            var result = 0;
            var mask = 1;

            while (n > 0)
            {
                if ((n & mask) > 0)
                {
                    result += (int)Math.Pow(5, exp);
                }
                exp++;
                n >>= 1;
            }

            return result;
        }
    }
}
