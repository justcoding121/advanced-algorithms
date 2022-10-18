using System;

namespace Advanced.Algorithms.DataStructures;

internal static class BstExtensions
{
    //find the node with the given identifier among descendants of parent and parent
    //uses pre-order traversal
    //O(log(n)) worst O(n) for unbalanced tree
    internal static (BstNodeBase<T>, int) Find<T>(this BstNodeBase<T> current, T value) where T : IComparable
    {
        var position = 0;

        while (true)
        {
            if (current == null) return (null, -1);

            var compareResult = current.Value.CompareTo(value);

            if (compareResult == 0)
            {
                position += current.Left != null ? current.Left.Count : 0;
                return (current, position);
            }

            if (compareResult > 0)
            {
                current = current.Left;
            }
            else
            {
                position += (current.Left != null ? current.Left.Count : 0) + 1;
                current = current.Right;
            }
        }
    }

    internal static BstNodeBase<T> FindMax<T>(this BstNodeBase<T> node) where T : IComparable
    {
        if (node == null) return null;

        while (true)
        {
            if (node.Right == null) return node;
            node = node.Right;
        }
    }

    internal static BstNodeBase<T> FindMin<T>(this BstNodeBase<T> node) where T : IComparable
    {
        if (node == null) return null;

        while (true)
        {
            if (node.Left == null) return node;
            node = node.Left;
        }
    }

    internal static BstNodeBase<T> NextLower<T>(this BstNodeBase<T> node) where T : IComparable
    {
        //root or left child
        if (node.Parent == null || node.IsLeftChild)
        {
            if (node.Left != null)
            {
                node = node.Left;

                while (node.Right != null) node = node.Right;

                return node;
            }

            while (node.Parent != null && node.IsLeftChild) node = node.Parent;

            return node?.Parent;
        }
        //right child

        if (node.Left != null)
        {
            node = node.Left;

            while (node.Right != null) node = node.Right;

            return node;
        }

        return node.Parent;
    }

    internal static BstNodeBase<T> NextHigher<T>(this BstNodeBase<T> node) where T : IComparable
    {
        //root or left child
        if (node.Parent == null || node.IsLeftChild)
        {
            if (node.Right != null)
            {
                node = node.Right;

                while (node.Left != null) node = node.Left;

                return node;
            }

            return node?.Parent;
        }
        //right child

        if (node.Right != null)
        {
            node = node.Right;

            while (node.Left != null) node = node.Left;

            return node;
        }

        while (node.Parent != null && node.IsRightChild) node = node.Parent;

        return node?.Parent;
    }

    internal static void UpdateCounts<T>(this BstNodeBase<T> node, bool spiralUp = false) where T : IComparable
    {
        while (node != null)
        {
            var leftCount = node.Left?.Count ?? 0;
            var rightCount = node.Right?.Count ?? 0;

            node.Count = leftCount + rightCount + 1;

            node = node.Parent;

            if (!spiralUp) break;
        }
    }

    //get the kth smallest element under given node
    internal static BstNodeBase<T> KthSmallest<T>(this BstNodeBase<T> node, int k) where T : IComparable
    {
        var leftCount = node.Left != null ? node.Left.Count : 0;

        if (k == leftCount) return node;

        if (k < leftCount) return KthSmallest(node.Left, k);

        return KthSmallest(node.Right, k - leftCount - 1);
    }

    //get the sorted order position of given item under given node
    internal static int Position<T>(this BstNodeBase<T> node, T item) where T : IComparable
    {
        if (node == null) return -1;

        var leftCount = node.Left != null ? node.Left.Count : 0;

        if (node.Value.CompareTo(item) == 0) return leftCount;

        if (item.CompareTo(node.Value) < 0) return Position(node.Left, item);

        var position = Position(node.Right, item);

        return position < 0 ? position : position + leftCount + 1;
    }
}