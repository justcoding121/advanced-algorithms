using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced.Algorithms.Combinatorics
{
    public class Variation
    {
        /*Variations are arrangements of selections of objects, where the order of the selected objects matters.
        To count r-element variations of n objects, we first need to choose a r-element combination and then
        a permutation of the selected objects*/

        //Without repetition
        /* It is also the number of ways of putting n distinct balls into r distinct boxes such that each box
        receives at most one element. */

        //With repetition
        /* It is the number of all ways of putting n distinct balls into r distinct boxes */
        public static List<List<T>> Find<T>(List<T> input, int r, bool withRepetition)
        {
            var combinations = new List<List<T>>();

            combinations.AddRange(Combination.Find(input, r, withRepetition));

            var variations = new List<List<T>>();

            foreach (var combination in combinations)
            {
                variations.AddRange(Permutation.Find(combination, combination.Count));
            }

            return variations;
        }

      
    }
}
