using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.BitAlgorithms
{
    /// <summary>
    /// Problem details below
    /// http://www.geeksforgeeks.org/find-the-two-repeating-elements-in-a-given-array/
    /// </summary>
    public class TwoRepeatingNums
    {
        public static int[] Find(int[] input, int max)
        {
            var concatenatedList = new List<int>();

            //given numbers in input
            foreach(var number in input)
            {
                concatenatedList.Add(number);
            }

            //all numbers b/w 1 and max
            for (int i = 1; i <= max; i++)
            {
                concatenatedList.Add(i);
            }

            //xor all given numbers and all numbers b/w 1 and max
            //xor result will be equal to x^y (xorResult = x^y and x and y will each appear three times)
            //because everything else will cancel out (since z^z = 0)
            int xorResult = 0;
            foreach (var number in concatenatedList)
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

            //split in to two lists
            foreach (var number in concatenatedList)
            {
                if ((number & mask) != 0)
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

            foreach (var number in listA)
            {
                x = x ^ number;
            }

            foreach (var number in listB)
            {
                y = y ^ number;
            }

            return new int[] { x, y };
        }
    }
}
