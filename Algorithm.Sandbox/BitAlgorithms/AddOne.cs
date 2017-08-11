using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.BitAlgorithms
{
    /// <summary>
    /// Problem details below
    /// http://www.geeksforgeeks.org/add-1-to-a-given-number/
    /// </summary>
    public class AddOne
    {
        public static int Find(int input)
        {
            var carry = 1;

            //until an empty bit (0 bit) in input is available
            // for placing the carry 
            while ((input & carry) > 0)
            {
                //bitwise addition
                input = input ^ carry;

                //move carry
                carry = carry << 1;
            }

            //a place was found to set the carry
            //so set it now
            input = input ^ carry;

            return input;
        }
    }
}
