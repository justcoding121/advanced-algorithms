using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.DynamicProgramming.Minimizing
{
    /// <summary>
    /// Problem statemement below
    /// http://www.geeksforgeeks.org/dynamic-programming-set-5-edit-distance/
    /// </summary>
    public class MinEditDistance
    {
        public static int GetMin(string a, string b)
        {
            return GetMin(a, b, a.Length - 1, b.Length - 1, new Dictionary<string, int>());
        }

        private static int GetMin(string a, string b, 
            int i, int j,
            Dictionary<string, int> cache)
        {
            //this means our edits from right to left 
            //caused string lenths to not match
            //so return max value for edit 
            //(which means this search path is not a viable option)
            if (i < 0 || j < 0)
            {
                return int.MaxValue;
            }

            //base case
            //string lengths are matching
            if (i == j && i == 0)
            {
                //nothing to edit
                if (a[i] == b[j])
                {
                    return 0;
                }
                else
                {
                    //do a replace operation
                    return 1;
                }
            }

            var cacheKey = $"{i}-{j}";

            if(cache.ContainsKey(cacheKey))
            {
                return cache[cacheKey];
            }

            int min = 0;

            if (a[i] == b[j])
            {
                //nothing to edit
                min = GetMin(a, b, i - 1, j - 1, cache);
            }
            else
            {
                //replace
                var resultA = GetMin(a, b, i - 1, j - 1, cache);

                //or insert
                var resultB = GetMin(a, b, i - 1, j, cache);

                //or delete
                var resultC = GetMin(a, b, i, j - 1, cache);

                var results = new List<int>() { resultA, resultB, resultC };

                //pick the option that given min distance 
                //+1 for one of the current operation above
                min = results.Min() + 1;
            }

            cache.Add(cacheKey, min);

            return min;
        }
    }
}
