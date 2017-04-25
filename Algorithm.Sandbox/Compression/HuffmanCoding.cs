using Algorithm.Sandbox.DataStructures;
using System;
using System.Linq;

namespace Algorithm.Sandbox.Compression
{
    /// <summary>
    /// A huffman coding implementation using Fibornacci Min Heap
    /// </summary>
    public class HuffmanCoding<T>
    {
        /// <summary>
        /// Returns a dictionary of chosen encoding bytes for each distinct T
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public AsDictionary<T, byte[]> Compress(T[] input)
        {
            var frequencies = computeFrequency(input);

            var minHeap = new AsFibornacciMinHeap<FrequencyWrap>();

            foreach (var frequency in frequencies)
            {
                minHeap.Insert(new FrequencyWrap(
                    frequency.Key, frequency.Value));
            }

            while (minHeap.Count > 1)
            {
                var a = minHeap.ExtractMin();
                var b = minHeap.ExtractMin();

                var newNode = new FrequencyWrap(
                    default(T), a.Frequency + b.Frequency);

                newNode.Left = a;
                newNode.Right = b;

                minHeap.Insert(newNode);
            }

            var root = minHeap.ExtractMin();

            var result = new AsDictionary<T, byte[]>();

            DFS(root, new AsArrayList<byte>(), result);

            return result;

        }

        /// <summary>
        /// Now gather the codes
        /// </summary>
        /// <param name="currentNode"></param>
        /// <param name="pathStack"></param>
        /// <param name="result"></param>
        private void DFS(FrequencyWrap currentNode, AsArrayList<byte> pathStack,
            AsDictionary<T, byte[]> result)
        {
            if(currentNode.IsLeaf)
            {
                result.Add(currentNode.Item, pathStack.ToArray());
                return;
            }

            if (currentNode.Left != null)
            {
                pathStack.Add(0);
                DFS(currentNode.Left, pathStack, result);
                pathStack.RemoveItem(pathStack.Length - 1);
            }

            if (currentNode.Right != null)
            {
                pathStack.Add(1);
                DFS(currentNode.Right, pathStack, result);
                pathStack.RemoveItem(pathStack.Length - 1);
            }
        }

        /// <summary>
        /// Computes frequencies of each of T in given input
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private AsDictionary<T, int> computeFrequency(T[] input)
        {
            var result = new AsDictionary<T, int>();

            foreach (var item in input)
            {
                if (!result.ContainsKey(item))
                {
                    result.Add(item, 1);
                    continue;
                }

                result[item]++;
            }

            return result;
        }

        private class FrequencyWrap : IComparable
        {
            public T Item { get; }
            public int Frequency { get; }

            public FrequencyWrap Left { get; set; }

            public FrequencyWrap Right { get; set; }

            public bool IsLeaf => Left == null && Right == null;

            public FrequencyWrap(T item, int frequency)
            {
                Item = item;
                Frequency = frequency;
            }

            public int CompareTo(object obj)
            {
                return Frequency.CompareTo((obj as FrequencyWrap).Frequency);
            }
        }

    }
}
