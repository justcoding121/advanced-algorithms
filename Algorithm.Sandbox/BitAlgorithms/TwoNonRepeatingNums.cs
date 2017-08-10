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
            //xor result will be equal to x^y (xorResult = x^y)
            //because everything else will cancel out (since z^z = 0)
            int xorResult = 0;
            foreach(var number in input)
            {
                xorResult = xorResult ^ number;
            }

            //so now we need to find x and y from the equation x^y=xorResult
            //this can be done by splitting the list to two
            //with one list having a particular bit set in xorResult and another not set   
            //Lets pick the last set bit in xorResult as that particular bit
            var mask = xorResult & ~(xorResult - 1);

            var listA = new List<int>();
            var listB = new List<int>();

            foreach (var number in input)
            {
                if((number & mask) !=0)
                {
                    listA.Add(number);
                }
                else
                {
                    listB.Add(number);
                }

            }

            //now x is in listA and y is in listB or vice versa 
            //doing an xor on each list will cancel out all duplicates (since z^z =0)
            int x = 0, y = 0;

            foreach(var number in listA)
            {
                x = x ^ number;
            }

            foreach(var number in listB)
            {
                y = y ^ number;
            }

            return new int[] { x, y };
        }
    }
}
