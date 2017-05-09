using Algorithm.Sandbox.DataStructures;

namespace Algorithm.Sandbox.DynamicProgramming
{
    /// <summary>
    /// Problem statement in detail below
    /// http://www.geeksforgeeks.org/count-possible-decodings-given-digit-sequence/
    /// </summary>
    public class CountDecodings
    {
        public static int Count(string input)
        {
            return Count(input, input.Length, new AsDictionary<int, int>());
        }

        /// <summary>
        /// Dp top down
        /// </summary>
        /// <param name="input"></param>
        /// <param name="i"></param>
        /// <param name="cache"></param>
        /// <returns></returns>
        private static int Count(string input, int i,
            AsDictionary<int, int> cache)
        {
            if(cache.ContainsKey(i))
            {
                return cache[i];
            }

            if (i <=1)
            {
                return 1;
            }

            //each digit corresponds to a char
            var result = Count(input, i - 1, cache);

            //each two consecutive digits can also correspond to a char
            //if it is less than 27
            if (int.Parse(input.Substring(i - 2, 2)) < 27)
            {
                result += Count(input, i - 2, cache);
            }

            cache.Add(i, result);

            return result;
        }
    }
}
