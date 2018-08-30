using System;

namespace Advanced.Algorithms.Numerical
{
    /// <summary>
    /// Tests for Prime in School method optimized.
    /// </summary>
    public class PrimeTester
    {
        /// <summary>
        /// Check if given number is prime.
        /// </summary>
        public static bool IsPrime(int number)
        {
            if (number <= 1)
            {
                return false;
            }

            if (number <= 3)
            {
                return true;
            }

            //number can be divided by 2 or 3
            if (number % 2 == 0 || number % 3 == 0)
            {
                return false;
            }

            //skip six numbers in each step
            //since we don't need to check for 3 even numbers 
            //and one number divisible by 3
            //inside the loop
            //check until square root of number
            var sqrt = Math.Sqrt(number);
            for (var i = 5; i <= sqrt; i = i + 6)
            {
                //check for two potential primes
                if (number % i == 0 || number % (i + 2) == 0)
                {
                    return false;
                }
            }

            return true;
        }

    }
}
