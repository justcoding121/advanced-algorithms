using Algorithm.Sandbox.DataStructures.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.Tests.DataStructures.Tree.TestHelpers
{
    internal static class BTreeTester
    {

        /// <summary>
        /// find max height by recursively visiting children
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        internal static int GetMaxHeight<T>(BTreeNode<T> node) where T : IComparable
        {
            var max = 0;

            for (int i = 0; i <= node.KeyCount; i++)
            {
                if (node.Children[i] != null)
                {
                    max = Math.Max(GetMaxHeight(node.Children[i]) + 1, max);
                }
            }

            return max;
        }

        /// <summary>
        /// find max height by recursively visiting children
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        internal static int GetMinHeight<T>(BTreeNode<T> node) where T : IComparable
        {
            var min = int.MaxValue;

            if(node.IsLeaf)
            {
                min = 0;
            }

            for (int i = 0; i <= node.KeyCount; i++)
            {
                if (node.Children[i] != null)
                {
                    min = Math.Min(GetMinHeight(node.Children[i]) + 1, min);
                }
            }

            return min;
        }
    }
}
