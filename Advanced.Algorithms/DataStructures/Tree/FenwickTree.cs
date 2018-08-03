using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.DataStructures
{
    /// <summary>
    /// A Fenwick Tree (binary indexed tree) implementation for prefix sum.
    /// </summary>
    public class FenwickTree<T> : IEnumerable<T>
    {
        private int length => tree.Length - 1;
        private T[] tree;

        private readonly T[] input;

        /// <summary>
        /// Add operation on generic type.
        /// </summary>
        private readonly Func<T, T, T> sumOperation;

        /// <summary>
        /// constructs a Fenwick tree using the specified sum operation function.
        /// Time complexity: O(nLog(n)).
        /// </summary>
        public FenwickTree(T[] input, Func<T, T, T> sumOperation)
        {
            if (input == null || sumOperation == null)
            {
                throw new ArgumentNullException();
            }

            this.input = input.Clone() as T[];

            this.sumOperation = sumOperation;
            construct(input);
        }

        /// <summary>
        /// Construct Fenwick tree from input array.
        /// </summary>
        private void construct(T[] input)
        {
            tree = new T[input.Length + 1];

            for (var i = 0; i < input.Length; i++)
            {
                var j = i + 1;
                while (j < input.Length)
                {
                    tree[j] = sumOperation(tree[j], input[i]);
                    j = getNextIndex(j);
                }
            }
        }

        /// <summary>
        /// Gets the prefix sum from 0 till the given end index.
        /// Time complexity: O(log(n)).
        /// </summary>
        public T PrefixSum(int endIndex)
        {
            if (endIndex < 0 || endIndex > length - 1)
            {
                throw new ArgumentException();
            }

            var sum = default(T);

            var currentIndex = endIndex + 1;

            while (currentIndex > 0)
            {
                sum = sumOperation(sum, tree[currentIndex]);
                currentIndex = getParentIndex(currentIndex);
            }

            return sum;
        }

        /// <summary>
        /// Get index of next sibling .
        /// </summary>
        private int getNextIndex(int currentIndex)
        {
            //add current index with
            //twos complimant of currentIndex AND with currentIndex
            return currentIndex + (currentIndex & (-currentIndex));
        }

        /// <summary>
        /// Get parent node index.
        /// </summary>
        private int getParentIndex(int currentIndex)
        {
            //substract current index with
            //twos complimant of currentIndex AND with currentIndex
            return currentIndex - (currentIndex & (-currentIndex));
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return input.Select(x=>x).GetEnumerator();
        }
    }
}
