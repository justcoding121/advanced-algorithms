using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.Combinatorics
{
    public class Combination
    {
        public static List<List<T>> Find<T>(List<T> input, int r, bool enableRepetition)
        {
            var result = new List<List<T>>();

            Recurse(input, r, enableRepetition, 0, new List<T>(), new HashSet<int>(), result);

            return result;
        }

        private static void Recurse<T>(List<T> input, int r, bool enableRepetition,
            int k, List<T> prefix, HashSet<int> prefixIndices,
            List<List<T>> result)
        {
            if (prefix.Count == r)
            {
                result.Add(new List<T>(prefix));
                return;
            }
            else
            {

            }

            for (int j = k; j < input.Count; j++)
            {
                if (prefixIndices.Contains(j) && !enableRepetition)
                {
                    continue;
                }

                prefix.Add(input[j]);
                prefixIndices.Add(j);

                Recurse(input, r, enableRepetition, j, prefix, prefixIndices, result);

                prefix.RemoveAt(prefix.Count - 1);
                prefixIndices.Remove(j);
            }
        }


    }
}
