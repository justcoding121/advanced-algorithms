using Algorithm.Sandbox.DataStructures;
using Algorithm.Sandbox.DataStructures.Heap.Min;
using System;

namespace Algorithm.Sandbox.NumericalMethods
{
    /// <summary>
    /// Returns the kth smallest element in given input
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class KthSmallest<T> where T : IComparable
    {
        /// <summary>
        /// Find smallest by first constructing a BMinHeap in O(n) time
        /// & then extracting out k -1 times
        /// Finally do one more extract to return the kth smallest
        /// Total O(n+k) complexity
        /// Better algorithms do exist with O(n) time
        /// </summary>
        /// <param name="input"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public T FindKthSmallest(T[] input, int k)
        {
            if (k > input.Length)
            {
                throw new Exception("K exceeds input length.");
            }

            var minHeap = new AsBMinHeap<T>(input);

            //0,1,2...(k-1) min extraction
            for (int i = 0; i < k - 1; i++)
            {
                minHeap.ExtractMin();
            }

            //kth extraction
            return minHeap.ExtractMin();
        }
    }
}
