using System.Collections.Generic;

namespace Algorithm.Sandbox.DynamicProgramming
{
    /// <summary>
    /// Problem statement in detail below
    /// http://www.geeksforgeeks.org/program-for-nth-fibonacci-number/
    /// </summary>
    public class FibornacciGenerator
    {
        /// <summary>
        /// get Fibornacci numbers
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static List<int> GetFibornacciNumbers(int number)
        {
            var result = new List<int>();

            var cache = new Dictionary<int, int>();

            Fibornacci(number, cache);

            foreach (var item in cache)
            {
                result.Add(item.Value);
            }

            return result;
        }

        /// <summary>
        /// Recursively compute
        /// </summary>
        /// <param name="number"></param>
        /// <param name="cache"></param>
        /// <returns></returns>
        private static int Fibornacci(int number, 
            Dictionary<int, int> cache)
        {
            if(cache.ContainsKey(number))
            {
                return cache[number];
            }

            if(number <= 2)
            {
                cache.Add(number, 1);
                return 1;
            }

            var result = Fibornacci(number - 1, cache)
                + Fibornacci(number - 2, cache);

            cache.Add(number, result);

            return result;
        }
    }
}
