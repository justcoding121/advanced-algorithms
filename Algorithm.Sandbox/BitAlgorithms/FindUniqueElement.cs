using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.BitAlgorithms
{
    /// <summary>
    /// Given an array where every element occurs three times,
    /// except one element which occurs only once. Find the element that occurs once.
    /// Expected time complexity is O(n) and O(1) extra space.
    /// Examples:
    /// Input: arr[] = {12, 1, 12, 3, 12, 1, 1, 2, 3, 3}
    /// Output: 2
    /// Problem details below
    /// http://www.geeksforgeeks.org/find-the-element-that-appears-once/
    /// </summary>
    public class FindUniqueElement
    {
        public static int Find(int[] input)
        {
            //keep track of numbers repeating ones, twice or thrice
            int ones = 0, twos = 0;
            foreach(var number in input)
            {
                //add current number to twos by checking if it appears in ones 
                twos = twos | (ones & number);

                //add current number to ones
                //if the number appears second time this will be reset to zero (because x^x=0)
                ones = ones ^ number;

                //eliminate element appearing 3rd time
                //from both ones and twos (common in ones and twos)
                var threesEliminationMask = ~(ones & twos);

                ones = ones & threesEliminationMask;
                twos = twos & threesEliminationMask;
            }

            return ones;
        }

            //1st example 
            //------------ 
            //2, 2, 2, 4 

            //After first iteration, 
            //ones = 2, twos = 0 
            //After second iteration, 
            //ones = 0, twos = 2 
            //After third iteration, 
            //ones = 0, twos = 0 
            //After fourth iteration, 
            //ones = 4, twos = 0 

            //2nd example 
            //------------ 
            //4, 2, 2, 2 

            //After first iteration, 
            //ones = 4, twos = 0 
            //After second iteration, 
            //ones = 6, twos = 0 
            //After third iteration, 
            //ones = 4, twos = 2 
            //After fourth iteration, 
            //ones = 4, twos = 0 
    }
}
