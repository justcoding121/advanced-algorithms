using Algorithm.Sandbox.DataStructures;
using System;
using System.Collections.Generic;

namespace Algorithm.Sandbox.Sorting
{
    /// <summary>
    /// Problem statement below
    /// http://www.geeksforgeeks.org/nearly-sorted-algorithm/
    /// </summary>
    public class SortAlmostSorted
    {
        /// <summary>
        /// Sort the given input
        /// where elements are misplaced almost by a distance k
        /// </summary>
        /// <param name="input"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static int[] Sort(int[] input, int k)
        {
            if (k > input.Length)
            {
                throw new Exception("K cannot exceed array length.");
            }

            var firstKElements = new int[k];
            Array.Copy(input, firstKElements, k);

            var minHeap = new BMinHeap<int>(firstKElements);

            var result = new List<int>();

            for (int i = k; i < input.Length; i++)
            {
                result.Add(minHeap.ExtractMin());
                minHeap.Insert(input[i]);
            }

            while (minHeap.Count > 0)
            {
                result.Add(minHeap.ExtractMin());
            }

            return result.ToArray();
        }
    }
}
