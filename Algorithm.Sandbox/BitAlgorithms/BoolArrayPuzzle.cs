using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.BitAlgorithms
{
    //Input: A array arr[] of two elements having value 0 and 1

    //Output: Make both elements 0.

    //Specifications: Following are the specifications to follow.
    //1) It is guaranteed that one element is 0 but we do not know its position.
    //2) We can’t say about another element it can be 0 or 1.
    //3) We can only complement array elements, no other operation like and, or, multi, division, …. etc.
    //4) We can’t use if, else and loop constructs.
    //5) Obviously, we can’t directly assign 0 to array elements.
    public class BoolArrayPuzzle
    {
        public static int[] ChangeToZero(int[] input)
        {
            //if input[0] was zero then; input[0] = input[0] => input[0] = 0;
            //if input[0] was one then; input[0] = input[1] => input[0] = 0;
            input[0] = input[input[0]];

            //now that we are sure that input[0] = 0 copy that to input[1]
            input[1] = input[0];

            return input;
        }
    }
}
