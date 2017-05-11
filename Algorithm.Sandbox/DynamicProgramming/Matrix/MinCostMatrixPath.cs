using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.DynamicProgramming
{
    /// <summary>
    /// Problem descriptions here
    /// http://www.geeksforgeeks.org/dynamic-programming-set-6-min-cost-path/
    /// </summary>
    public class MinCostMatrixPath
    {
        public static int FindPath(int[,] matrix)
        {
            return FindPath(matrix, 0, 0);
        }

        private static int FindPath(int[,] matrix, int i, int j)
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

            var pathResults = new List<int>();

            pathResults.Add(FindPath(matrix, i + 1, j + 1));
            pathResults.Add(FindPath(matrix, i, j + 1));
            pathResults.Add(FindPath(matrix, i + 1, j));

            return pathResults.Min() + matrix[i, j];
        }
    }
}
