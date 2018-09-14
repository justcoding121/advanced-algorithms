using System;

namespace Advanced.Algorithms.DataStructures
{
    internal static class BSTExtensions
    {
        //find the node with the given identifier among descendants of parent and parent
        //uses pre-order traversal
        //O(log(n)) worst O(n) for unbalanced tree
        internal static BSTNodeBase<T> Find<T>(this BSTNodeBase<T> current, T value) where T : IComparable
        {
            while (true)
            {
                if (current == null)
                {
                    return null;
                }

                var compareResult = current.Value.CompareTo(value);

                if (compareResult == 0)
                {
                    return current;
                }

                if (compareResult > 0)
                {
                    current = current.Left;
                }
                else
                {
                    current = current.Right;
                }
            }
        }

        //O(log(n)) worst O(n) for unbalanced tree
        internal static int GetHeight<T>(this BSTNodeBase<T> node) where T : IComparable
        {
            if (node == null)
            {
                return -1;
            }

            return Math.Max(GetHeight(node.Left), GetHeight(node.Right)) + 1;
        }

        internal static BSTNodeBase<T> FindMax<T>(this BSTNodeBase<T> node) where T : IComparable
        {
            if (node == null)
            {
                return null;
            }

            while (true)
            {
                if (node.Right == null) return node;
                node = node.Right;
            }
        }

        internal static BSTNodeBase<T> FindMin<T>(this BSTNodeBase<T> node) where T : IComparable
        {
            if (node == null)
            {
                return null;
            }

            while (true)
            {
                if (node.Left == null) return node;
                node = node.Left;
            }
        }

        internal static BSTNodeBase<T> NextLower<T>(this BSTNodeBase<T> node) where T : IComparable
        {
            //root or left child
            if (node.Parent == null || node.IsLeftChild)
            {
                if (node.Left != null)
                {
                    node = node.Left;

                    while (node.Right != null)
                    {
                        node = node.Right;
                    }

                    return node;
                }
                else
                {
                    while (node.Parent != null && node.IsLeftChild)
                    {
                        node = node.Parent;
                    }

                    return node?.Parent;
                }
            }
            //right child
            else
            {
                if (node.Left != null)
                {
                    node = node.Left;

                    while (node.Right != null)
                    {
                        node = node.Right;
                    }

                    return node;
                }
                else
                {
                    return node.Parent;
                }
            }

        }

        internal static BSTNodeBase<T> NextHigher<T>(this BSTNodeBase<T> node) where T : IComparable
        {
            //root or left child
            if (node.Parent == null || node.IsLeftChild)
            {
                if (node.Right != null)
                {
                    node = node.Right;

                    while (node.Left != null)
                    {
                        node = node.Left;
                    }

                    return node;
                }
                else
                {
                    return node?.Parent;
                }
            }
            //right child
            else
            {
                if (node.Right != null)
                {
                    node = node.Right;

                    while (node.Left != null)
                    {
                        node = node.Left;
                    }

                    return node;
                }
                else
                {
                    while (node.Parent != null && node.IsRightChild)
                    {
                        node = node.Parent;
                    }

                    return node?.Parent;
                }
            }
        }
    }
}
