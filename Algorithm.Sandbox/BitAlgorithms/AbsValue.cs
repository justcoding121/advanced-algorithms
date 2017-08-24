using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.BitAlgorithms
{
    /// <summary>
    /// Problem details below
    /// http://www.geeksforgeeks.org/compute-the-integer-absolute-value-abs-without-branching/
    /// </summary>
    public class AbsValue
    {
        public static int GetAbs(int x)
        {
            //will be all ones or zeros depending on sign bit
            //for +ive all zeros 0000
            //for -ive all ones 1111
            var mask = x >> 31;

            //for +ive numbers say +1
            //(0001 + 0000) ^ 0000 => 0001 ^ 0000 = > 00001 ( because x ^ 0 = x)
            //for -ive numbers say -1
            //(1111 + 1111) ^ 1111 => 1110 ^ 1111 => 0001
            return (x + mask) ^ mask;
        }
    }
}
