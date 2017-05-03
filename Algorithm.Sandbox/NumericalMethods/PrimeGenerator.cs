using Algorithm.Sandbox.DataStructures;
using System;

namespace Algorithm.Sandbox.NumericalMethods
{
    public class PrimeGenerator
    {
        /// <summary>
        /// Prime generation using Sieve of Eratosthenes
        /// </summary>
        /// <param name="max"></param>
        /// <returns></returns>
        public static AsArrayList<int> GetAllPrimes(int max)
        {
            var primeTable = new bool[max + 1];

            var sqrt = Math.Sqrt(max);

            for (int i = 2; i < sqrt; i++)
            {
                //mark multiples of current number as true
                if (!primeTable[i])
                {
                    for (int j = 2 * i; j <= max; j = j + i)
                    {
                        primeTable[j] = true;
                    }
                }
            }

            //now write back results
            var result = new AsArrayList<int>();

            for (int i = 2; i < primeTable.Length; i++)
            {
                if (!primeTable[i])
                {
                    result.Add(i);
                }
            }

            return result;
        }
    }
}
