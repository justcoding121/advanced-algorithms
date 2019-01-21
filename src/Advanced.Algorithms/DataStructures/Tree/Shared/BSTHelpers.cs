using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced.Algorithms.DataStructures
{
    internal class BSTHelpers
    {
        internal static void ValidateSortedCollection<T>(IEnumerable<T> sortedCollection) where T : IComparable
        {
            if (!isSorted(sortedCollection))
            {
                throw new ArgumentException("Initial collection should have unique keys and be in sorted order.");
            }
        }

        internal static BSTNodeBase<T> ToBST<T>(BSTNodeBase<T>[] sortedNodes) where T : IComparable
        {
            return toBST(sortedNodes, 0, sortedNodes.Length - 1);
        }

        internal static int AssignCount<T>(BSTNodeBase<T> node) where T : IComparable
        {
            if (node == null)
            {
                return 0;
            }

            node.Count = AssignCount(node.Left) + AssignCount(node.Right) + 1;

            return node.Count;
        }

        private static BSTNodeBase<T> toBST<T>(BSTNodeBase<T>[] sortedNodes, int start, int end) where T : IComparable
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

        private static bool isSorted<T>(IEnumerable<T> collection) where T : IComparable
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
