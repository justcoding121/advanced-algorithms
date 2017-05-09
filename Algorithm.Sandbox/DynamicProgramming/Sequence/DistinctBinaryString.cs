using Algorithm.Sandbox.DataStructures;

namespace Algorithm.Sandbox.DynamicProgramming
{
    /// <summary>
    /// Problem statement in detail below
    /// http://www.geeksforgeeks.org/count-number-binary-strings-without-consecutive-1s/
    /// </summary>
    public class DistinctBinaryString
    {
        public static int Count(int length)
        {
            var cache = new AsDictionary<string, int>();
          
            //last digit can be 1 or 0 (sum both counts)
            return Count(length, true, cache)
               + Count(length, false, cache);
        }

        /// <summary>
        /// DP top down
        /// </summary>
        /// <param name="length"></param>
        /// <param name="lastWasOne"></param>
        /// <param name="cache"></param>
        /// <returns></returns>
        public static int Count(int length, 
            bool lastWasOne, 
            AsDictionary<string, int> cache)
        {
            //base case
            if (length == 1)
            {
                return 1;
            }

            var cacheKey = string.Concat(length, lastWasOne);

            if(cache.ContainsKey(cacheKey))
            {
                return cache[cacheKey];
            }

            //if last was 1, next can't be one
            if(lastWasOne)
            {
                return Count(length - 1, !lastWasOne, cache);
            }

            //if last was 0, next can be 1 or 0
            //sum the count 
            var result = Count(length - 1, lastWasOne, cache) 
                + Count(length - 1, !lastWasOne, cache);

            cache.Add(cacheKey, result);

            return result;
        }

    }
}
