using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithm.Sandbox.DynamicProgramming.Matrix
{
    /// <summary>
    /// Problem statement below
    ///http://www.geeksforgeeks.org/given-matrix-o-x-find-largest-subsquare-surrounded-x/
    /// </summary>
    public class MaxXSideSubSquare
    {
        public static int FindMax(char[,] matrix)
        {
           var result = findMaxSubMatrixPerimeter(matrix,
            0, 0, matrix.GetLength(0) - 1, matrix.GetLength(1) - 1,
            new Dictionary<string, int>());

            //return length of one side
            return result / 4;

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
        private static int findMaxSubMatrixPerimeter(char[,] matrix,
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

            results.Add(findMaxSubMatrixPerimeter(matrix, x1 + 1,
                   y1, x2, y2, cache));

            results.Add(findMaxSubMatrixPerimeter(matrix, x1,
                    y1 + 1, x2, y2, cache));

            results.Add(findMaxSubMatrixPerimeter(matrix, x1,
               y1, x2 - 1, y2, cache));

            results.Add(findMaxSubMatrixPerimeter(matrix, x1,
                y1, x2, y2 - 1, cache));

            //current matrix sum
            var sum = GetOnesSum(matrix, x1, y1, x2, y2, cache);

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
        private static int GetOnesSum(char[,] matrix,
            int x1, int y1, int x2, int y2,
            Dictionary<string, int> cache)
        {
            if (x1 == x2 && y1 == y2)
            {
                return 0;
            }

            //check for a square
            if (x2 - x1 != y2 - y1)
            {
                return 0;
            }

            var cacheKey = string.Concat(x1, y1, x2, y2);

            if (cache.ContainsKey(cacheKey))
            {
                return cache[cacheKey];
            }

            var sum = 0;

            var x = new int[2] { x1, x2 };
            //visit only the perimeter
            for (int i = 0; i < x.Length ; i++)
            {
                for (int j = y1; j <= y2; j++)
                {
                    if (matrix[x[i], j] == 'X')
                    {
                        sum += 1;
                    }
                    else
                    {
                        cache.Add(cacheKey, 0);
                        return 0;
                    }
                }
            }

            var y = new int[2] { y1, y2 };
            //visit only the perimeter
            for (int i = 0; i < y.Length; i++)
            {
                for (int j = x1; j <= x2; j++)
                {
                    if (matrix[j, y[i]] == 'X')
                    {
                        sum += 1;
                    }
                    else
                    {
                        cache.Add(cacheKey, 0);
                        return 0;
                    }
                }
            }

            cache.Add(cacheKey, sum);

            return sum;
        }
    }
}
