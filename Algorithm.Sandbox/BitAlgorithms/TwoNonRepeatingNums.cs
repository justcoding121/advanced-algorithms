using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.BitAlgorithms
{
    /// <summary>
    /// Given an array in which all numbers except two are repeated once.
    /// (i.e. we have 2n+2 numbers and n numbers are occurring twice and remaining two have occurred once). 
    /// Find those two numbers in the most efficient way.
    /// Problem details below
    /// http://www.geeksforgeeks.org/find-two-non-repeating-elements-in-an-array-of-repeating-elements/
    /// </summary>
    public class TwoNonRepeatingNums
    {
        public static int[] Find(int[] input)
        {
            var xorResult = 0;
            foreach(var number in input)
            {
                xorResult = xorResult ^ number;
            }

            //xor result will be equal to x^y (xorResult = x^y)
            //because everything else will cancel out (because z^z = 0)
            //so now we need to find x and y from the equation x^y=xorResult
            int x = 0, y = 0;

            var mask = 1;

            while(xorResult!=0)
            {

            }

            throw new NotImplementedException();
        }
    }
}
