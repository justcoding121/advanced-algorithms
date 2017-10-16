using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.Combinatorics
{
    public class Variation
    {
        /*Variations are arrangements of selections of objects, where the order of the selected objects matters.
        To count k-element variations of n objects, we first need to choose a k-element combination and then
        a permutation of the selected objects*/

        //Without repetition
        /* It is also the number of ways of putting r distinct balls into input.Count distinct boxes such that each box
        receives at most one element. */

        //With repetition
        /* It is the number of all ways of putting r distinct balls into input.Count distinct boxes */
        public static List<List<T>> Find<T>(List<T> input, int r, bool enableRepetition)
        {
            var result = new List<List<T>>();

            var combinations = Combination.Find(input, r, false);

            foreach (var combination in combinations)
            {
                var permutations = Permutation.Find(combination, enableRepetition);

                foreach(var permutation in permutations)
                {
                    result.Add(permutation);
                }
                
            }

            return result;
        }
    }
}
