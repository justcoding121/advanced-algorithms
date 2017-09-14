using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.BitAlgorithms
{
    //GCD without division or mod operators 
    //GCD by substraction
    public class GCD
    {
        public static int Find(int a, int b)
        {
            if (b == 0)
            {
                return a;
            }
            if (a == 0)
            {
                return b;
            }

            //fix negative numbers
            if (a < 0)
            {
                a = -a;
            }
            if (b < 0)
            {
                b = -b;
            }

            // p and q even
            if ((a & 1) == 0 && (b & 1) == 0)
            {
                //divide both a and b by 2
                //multiply by 2 the result
                return Find(a >> 1, b >> 1) << 1;
            }

            // a is even, b is odd
            else if ((a & 1) == 0)
            {
                //divide a by 2
                return Find(a >> 1, b);
            }

            // a is odd, b is even
            else if ((b & 1) == 0)
            {
                //divide by by 2
                return Find(a, b >> 1);
            }
            // a and b odd, a >= b
            else if (a >= b)
            {
                //since substracting two odd numbers gives an even number
                //divide (a-b) by 2 to reduce calculations
                return Find((a - b) >> 1, b);
            }
            // a and b odd, a < b
            else
            {
                //since substracting two odd numbers gives an even number
                //divide (b-a) by 2 to reduce calculations
                return Find(a, (b - a) >> 1);
            }

        }
    }
}
