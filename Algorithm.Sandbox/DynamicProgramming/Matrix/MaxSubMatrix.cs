using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithm.Sandbox.DynamicProgramming.Matrix
{
    /// <summary>
    /// Problem statement below
    /// http://www.geeksforgeeks.org/dynamic-programming-set-27-max-sum-rectangle-in-a-2d-matrix/
    /// </summary>
    public class MaxSubMatrix
    {
        public static int FindMaxSubMatrixSum(int[,] matrix)
        {
            return findMaxSubMatrixSum(matrix,
            0, 0, matrix.GetLength(0) - 1, matrix.GetLength(1) - 1,
            new Dictionary<string, int>());

        }

        /// <summary>
        /// DP top down
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="cache"></param>
        /// <returns></returns>
        private static int findMaxSubMatrixSum(int[,] matrix,
            int x1, int y1, int x2, int y2, 
            Dictionary<string, int> cache)
        {
            if (y1 > y2
                || x1 > x2)
            {
                return 0;
            }

            //sub matrix sums
            var results = new List<int>();

            results.Add(findMaxSubMatrixSum(matrix, x1 + 1,
                   y1, x2, y2, cache));

            results.Add(findMaxSubMatrixSum(matrix, x1,
                    y1 + 1, x2, y2, cache));

            results.Add(findMaxSubMatrixSum(matrix, x1,
               y1, x2 - 1, y2, cache));

            results.Add(findMaxSubMatrixSum(matrix, x1,
                y1, x2, y2 - 1, cache));

            //current matrix sum
            var sum = GetSum(matrix, x1, y1, x2, y2, cache);

            //update max sum
            return Math.Max(sum, results.Max());

        }

        /// <summary>
        /// Get sum of given matrix
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="cache"></param>
        /// <returns></returns>
        private static int GetSum(int[,] matrix,
            int x1, int y1, int x2, int y2, 
            Dictionary<string, int> cache)
        {
            if (x1 == x2 && y1 == y2)
            {
                return matrix[x1, y1];
            }

            var cacheKey = string.Concat(x1, y1, x2, y2);

            if(cache.ContainsKey(cacheKey))
            {
                return cache[cacheKey];
            }

            var sum = 0;
            for (int i = x1; i <= x2; i++)
            {
                for (int j = y1; j <= y2; j++)
                {
                    sum += matrix[i, j];
                }
            }

            cache.Add(cacheKey, sum);

            return sum;
        }
    }
}
