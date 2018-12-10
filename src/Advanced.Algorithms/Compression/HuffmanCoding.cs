using System;
using System.Collections.Generic;
using Advanced.Algorithms.DataStructures;

namespace Advanced.Algorithms.Compression
{
    /// <summary>
    /// A huffman coding implementation using Fibonacci Min Heap.
    /// </summary>
    public class HuffmanCoding<T>
    {
        /// <summary>
        /// Returns a dictionary of chosen encoding bytes for each distinct T.
        /// </summary>
        public Dictionary<T, byte[]> Compress(T[] input)
        {
            var frequencies = computeFrequency(input);

            var minHeap = new BHeap<FrequencyWrap>();

            foreach (var frequency in frequencies)
            {
                minHeap.Insert(new FrequencyWrap(
                    frequency.Key, frequency.Value));
            }

            while (minHeap.Count > 1)
            {
                var a = minHeap.Extract();
                var b = minHeap.Extract();

                var newNode = new FrequencyWrap(
                    default(T), a.Frequency + b.Frequency);

                newNode.Left = a;
                newNode.Right = b;

                minHeap.Insert(newNode);
            }

            var root = minHeap.Extract();

            var result = new Dictionary<T, byte[]>();

            dfs(root, new List<byte>(), result);

            return result;

        }

        /// <summary>
        /// Now gather the codes.
        /// </summary>  
        private void dfs(FrequencyWrap currentNode, List<byte> pathStack, Dictionary<T, byte[]> result)
        {
            if(currentNode.IsLeaf)
            {
                result.Add(currentNode.Item, pathStack.ToArray());
                return;
            }

            if (currentNode.Left != null)
            {
                pathStack.Add(0);
                dfs(currentNode.Left, pathStack, result);
                pathStack.RemoveAt(pathStack.Count - 1);
            }

            if (currentNode.Right != null)
            {
                pathStack.Add(1);
                dfs(currentNode.Right, pathStack, result);
                pathStack.RemoveAt(pathStack.Count - 1);
            }
        }

        /// <summary>
        /// Computes frequencies of each of T in given input.
        /// </summary>
        private Dictionary<T, int> computeFrequency(T[] input)
        {
            var result = new Dictionary<T, int>();

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
                return Frequency.CompareTo(((FrequencyWrap) obj).Frequency);
            }
        }

    }
}
