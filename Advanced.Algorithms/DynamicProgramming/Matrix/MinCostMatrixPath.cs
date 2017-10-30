using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced.Algorithms.DynamicProgramming
{
    /// <summary>
    /// Problem descriptions here
    /// http://www.geeksforgeeks.org/dynamic-programming-set-6-min-cost-path/
    /// </summary>
    public class MinCostMatrixPath
    {
        public static int FindPath(int[,] matrix)
        {
            return FindPath(matrix, 0, 0, new Dictionary<string, int>());
        }

        private static int FindPath(int[,] matrix, int i, int j, Dictionary<string, int> cache)
        {
            if (i >= matrix.GetLength(0)
                || j >= matrix.GetLength(1))
            {
                return int.MaxValue;
            }

            if (i == (matrix.GetLength(0) - 1)
                && j == (matrix.GetLength(1) - 1))
            {
                return matrix[i, j];
            }

            var cacheKey = $"{i}-{j}";

            if(cache.ContainsKey(cacheKey))
            {
                return cache[cacheKey];
            }

            var pathResults = new List<int>();

            pathResults.Add(FindPath(matrix, i + 1, j + 1, cache));
            pathResults.Add(FindPath(matrix, i, j + 1, cache));
            pathResults.Add(FindPath(matrix, i + 1, j, cache));

            var result = pathResults.Min() + matrix[i, j];

            cache.Add(cacheKey, result);

            return result;
        }
    }
}
