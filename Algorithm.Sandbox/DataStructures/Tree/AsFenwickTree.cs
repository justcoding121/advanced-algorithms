using System;

namespace Algorithm.Sandbox.DataStructures
{
    /// <summary>
    /// Fenwick Tree (Binary Indexed Tree) for prefix sum
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AsFenwickTree<T>
    {
        private int length => FenwickTree.Length - 1;
        private T[] FenwickTree;

        /// <summary>
        /// add operation on generic type
        /// </summary>
        private Func<T, T, T> sumOperation;

        /// <summary>
        /// constructs a Fenwick tree using the specified sum operation function
        /// </summary>
        /// <param name="input"></param>
        /// <param name="sumOperation"></param>
        public AsFenwickTree(T[] input, Func<T, T, T> sumOperation)
        {
            if (input == null || sumOperation == null)
            {
                throw new ArgumentNullException();
            }

            this.sumOperation = sumOperation;
            Construct(input);
        }

        /// <summary>
        /// construct fenwick tree from input array
        /// </summary>
        /// <param name="input"></param>
        private void Construct(T[] input)
        {
            FenwickTree = new T[input.Length + 1];

            for (int i = 0; i < input.Length; i++)
            {
                var j = i + 1;
                while (j < input.Length)
                {
                    FenwickTree[j] = sumOperation(FenwickTree[j], input[i]);
                    j = getNextIndex(j);
                }
            }

        }

        /// <summary>
        /// get prefix sum from 0 till end index
        /// </summary>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        public T GetPrefixSum(int endIndex)
        {
            if (endIndex < 0 || endIndex > length - 1)
            {
                throw new ArgumentException();
            }

            var sum = default(T);

            var currentIndex = endIndex + 1;

            while (currentIndex > 0)
            {
                sum = sumOperation(sum, FenwickTree[currentIndex]);

                currentIndex = getParentIndex(currentIndex);
            }

            return sum;
        }

        /// <summary>
        /// Get index of next sibling 
        /// </summary>
        /// <param name="currentIndex"></param>
        /// <returns></returns>
        private int getNextIndex(int currentIndex)
        {
            //add current index with
            //twos complimant of currentIndex AND with currentIndex
            return currentIndex + (currentIndex & (-currentIndex));
        }

        /// <summary>
        /// Get parent node index
        /// </summary>
        /// <param name="currentIndex"></param>
        /// <returns></returns>
        private int getParentIndex(int currentIndex)
        {
            //substract current index with
            //twos complimant of currentIndex AND with currentIndex
            return currentIndex - (currentIndex & (-currentIndex));
        }

    }
}
