using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.BitAlgorithms
{
    /// <summary>
    /// Problem details below
    /// http://www.geeksforgeeks.org/count-total-set-bits-in-all-numbers-from-1-to-n/
    /// https://stackoverflow.com/questions/9812742/finding-the-total-number-of-set-bits-from-1-to-n
    /// </summary>
    public class SetBits
    {
        public static int Count(int integer)
        {
            //base case
            //zero set bits in zero
            if (integer == 0)
            {
                return 0;
            }

            //any integer will be = 2 ^ b + 2 ^ b-1 + 2^ b -2 ....+ 2 ^ 0 + 0
            //where b is the MSB exponent
            var b = FindLeftMostSetBitPosition(integer);

            //if integer is 1, 3, 7, 15, 31 etc 
            //if integer is of the form (2^b)-1 where b = 0, 1, 2 .. (exponents)
            //eg. integer = 3 (011), position = 1 => 3 + 1 == (1<<(1+1)) => 4 == 4 (100)
            if (integer + 1 == (int)Math.Pow(2, b))
            {
                //based on the observation 
                //if integer is of the form (2^b)-1 where b = 0, 1, 2 .. (exponents)
                //for example the numbers 1, 3, 7, 15, 31 etc 
                //then BitCount = b * 2^(b-1)
                //eg integer => 3, position => 1 => 2 * 2^1 => 4
                return (b + 1) * (int)Math.Pow(2, b);
            }

            //based on the observation 
            //if integer is of the form (2^b)-1 where b = 0, 1, 2 .. (exponents)
            //for example the numbers 1, 3, 7, 15, 31 etc 
            //then BitCount = b * 2^(b-1)
            //eg Count(3) = 4
            var msbSetBitCount = b * (int)Math.Pow(2, b - 1);

            //now compute the bitCount for remaining balance
            var balance = integer - (int)Math.Pow(2, b);

            //again another observation is that
            //count for balance will equal to Count(balance) + (balance + 1)
            //Count(6) = Count(3) + remaining bit count
            //Count(6) = 4 + Count(2) + (2 + 1)
            //Count(6) = 4 + (1 + 0 + 1) + 2 + 1 = 9
            return msbSetBitCount + Count(balance) + (balance + 1);
            
        }

        public static int FindLeftMostSetBitPosition(int integer)
        {
            int count = 0;

            while (integer > 0)
            {
                integer = integer >> 1;
                count++;
            }

            return count - 1;
        }

    }
}
