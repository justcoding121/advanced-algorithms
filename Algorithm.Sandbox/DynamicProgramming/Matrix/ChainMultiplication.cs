using System;
using System.Collections.Generic;

namespace Algorithm.Sandbox.DynamicProgramming.Matrix
{
    /// <summary>
    /// Problem statement here
    /// http://www.geeksforgeeks.org/dynamic-programming-set-8-matrix-chain-multiplication/
    /// </summary>
    public class ChainMultiplication
    {
        //Matrices (R1 x C1) and (R2 x C2) can be multiplied
        //only if C1 = R2 and the result will be of size R1 x C2
        //total operations will be R1 x C1 x C2
        //Given Matrix Ai has dimensions size[i-1] x size[i] for i = 1..n
        public static int FindMinMultiplications(int[] size)
        {
            return FindMinMultiplications(size, 1, size.Length - 1, new Dictionary<string, int>());
        }

        /// <summary>
        /// DP top down
        /// </summary>
        /// <param name="size"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        private static int FindMinMultiplications(int[] size, 
            int i, int j,
            Dictionary<string, int> cache)
        {
            if (i == j)
            {
                return 0;
            }

            var cacheKey = string.Concat(i, j);

            if (cache.ContainsKey(cacheKey))
            {
                return cache[cacheKey];
            }

            var min = int.MaxValue;

            for (int k = i; k < j; k++)
            {
                // total operations = min operations to multiply from (i-1 to k) 
                // + min operations to multiply from (k+1 to j)
                // + size[i-1] * size[k] * size[j] 
                //(expected operations = R1 x C1 x C2)
                //here R1 = size[i-1], C1 = size[k], R2 = size[k+1] & C2 = size[j]
                var operations = FindMinMultiplications(size, i, k, cache) +
                                FindMinMultiplications(size, k + 1, j, cache) 
                                + size[i-1] * size[k] * size[j];

                min = Math.Min(operations, min);
            }

            cache.Add(cacheKey, min);

            return min;
        }
    }
}
