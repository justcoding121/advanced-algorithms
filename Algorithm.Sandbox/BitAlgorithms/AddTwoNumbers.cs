using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.BitAlgorithms
{
    /// <summary>
    /// Add two numbers using bitwise operators
    /// </summary>
    public class AddTwoNumbers
    {
        public static int Add(int a, int b)
        {
            //until no more carry bits left
            while (b != 0)
            {
                //expected carry can be obtained by AND
                var carry = a & b;

                //single bit addition can be done by doing XOR 
                a = a ^ b;

                //now assign carry to b
                b = carry << 1;
            }

            return a;
        }
    }
}
