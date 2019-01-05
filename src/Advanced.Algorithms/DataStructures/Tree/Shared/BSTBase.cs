using System;
using System.Collections.Generic;

namespace Advanced.Algorithms.DataStructures
{
    public class BSTBase<T> where T : IComparable
    {
        internal void ValidateSortedCollection(IEnumerable<T> sortedCollection)
        {
            if (!isSorted(sortedCollection))
            {
                throw new ArgumentException("Initial collection should have unique keys and be in sorted order.");
            }
        }

        internal BSTNodeBase<T> ToBST(BSTNodeBase<T>[] sortedNodes)
        {
            return toBST(sortedNodes, 0, sortedNodes.Length - 1);
        }

        internal int assignCount(BSTNodeBase<T> node) 
        {
            if (node == null)
            {
                return 0;
            }

            node.Count = assignCount(node.Left) + assignCount(node.Right) + 1;

            return node.Count;
        }

        private BSTNodeBase<T> toBST(BSTNodeBase<T>[] sortedNodes, int start, int end)
        {
            if (start > end)
                return null;

            int mid = (start + end) / 2;
            var root = sortedNodes[mid];

            root.Left = toBST(sortedNodes, start, mid - 1);
            if (root.Left != null)
            {
                root.Left.Parent = root;
            }

            root.Right = toBST(sortedNodes, mid + 1, end);
            if (root.Right != null)
            {
                root.Right.Parent = root;
            }

            return root;
        }

        private bool isSorted(IEnumerable<T> collection)
        {
            var enumerator = collection.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                return true;
            }

            var previous = enumerator.Current;

            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;

                if (current.CompareTo(previous) <= 0)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
