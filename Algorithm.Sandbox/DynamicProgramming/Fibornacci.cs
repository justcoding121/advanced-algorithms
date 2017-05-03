using Algorithm.Sandbox.DataStructures;

namespace Algorithm.Sandbox.DynamicProgramming
{
    public class FibornacciGenerator
    {
        /// <summary>
        /// get Fibornacci numbers
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static AsArrayList<int> GetFibornacciNumbers(int number)
        {
            var result = new AsArrayList<int>();

            var cache = new AsDictionary<int, int>();

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
            AsDictionary<int, int> cache)
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
