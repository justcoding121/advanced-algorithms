using System;
using Advanced.Algorithms.DataStructures;

namespace Advanced.Algorithms.Tests.DataStructures
{
    internal static class BTreeTester
    {

        /// <summary>
        /// find max height by recursively visiting children
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        internal static int GetMaxHeight<T>(BNode<T> node) where T : IComparable
        {
            var max = 0;

            for (int i = 0; i <= node.KeyCount; i++)
            {
                if (node.GetChildren()[i] != null)
                {
                    max = Math.Max(GetMaxHeight(node.GetChildren()[i]) + 1, max);
                }
            }

            return max;
        }

        /// <summary>
        /// find max height by recursively visiting children
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        internal static int GetMinHeight<T>(BNode<T> node) where T : IComparable
        {
            var min = int.MaxValue;

            if(node.GetChildren()[0]==null)
            {
                min = 0;
            }

            for (int i = 0; i <= node.KeyCount; i++)
            {
                if (node.GetChildren()[i] != null)
                {
                    min = Math.Min(GetMinHeight(node.GetChildren()[i]) + 1, min);
                }
            }

            return min;
        }
    }
}
