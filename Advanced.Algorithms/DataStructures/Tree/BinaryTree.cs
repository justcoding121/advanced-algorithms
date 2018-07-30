using System;

namespace Advanced.Algorithms.DataStructures
{
    public class BinaryTreeNode<T> : IComparable where T : IComparable
    {
        public T Value { get; set; }

        public BinaryTreeNode<T> Parent { get; set; }

        public BinaryTreeNode<T> Left { get; set; }
        public BinaryTreeNode<T> Right { get; set; }

        public bool IsLeaf => Left == null && Right == null;

        public BinaryTreeNode(BinaryTreeNode<T> parent, T value)
        {
            Parent = parent;
            Value = value;
        }

        public int CompareTo(object obj)
        {
            return CompareTo(obj as BinaryTreeNode<T>);
        }

        public int CompareTo(BinaryTreeNode<T> node)
        {
            return Value.CompareTo(node.Value);
        }
    }


    /// <summary>
    /// A complete binary tree implementation using pointers
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BinaryTree<T> where T : IComparable
    {

        public BinaryTreeNode<T> Root { get; set; }
        public int Count { get; private set; }

        //O(log(n))
        public bool HasItem(T value)
        {
            if (Root == null)
            {
                return false;
            }

            return find(Root, value) != null;
        }

        //O(log(n)) worst O(n) for unbalanced tree
        public int GetHeight()
        {
            return GetHeight(Root);
        }

        //O(log(n)) worst O(n) for unbalanced tree
        private int GetHeight(BinaryTreeNode<T> node)
        {
            if (node == null)
            {
                return -1;
            }

            return Math.Max(GetHeight(node.Left), GetHeight(node.Right)) + 1;
        }

        //O(log(n)) worst O(n) for unbalanced tree
        private BinaryTreeNode<T> find(T value)
        {
            return Root == null ? null : find(Root, value);
        }


        //find the node with the given identifier among descendants of parent and parent
        //uses pre-order traversal
        //O(log(n)) worst O(n) for unbalanced tree
        private BinaryTreeNode<T> find(BinaryTreeNode<T> parent, T value)
        {
            while (true)
            {
                if (parent == null)
                {
                    return null;
                }

                if (parent.Value.CompareTo(value) == 0)
                {
                    return parent;
                }

                var left = find(parent.Left, value);

                if (left != null)
                {
                    return left;
                }

                parent = parent.Right;
            }
        }

        /// <summary>
        /// only inserts to unambiguous nodes (a node with two children cannot be inserted with a new child unambiguously)
        ///  O(log(n)) worst O(n) for unbalanced tree
        /// </summary>
        public void Insert(T parentValue, T newValue)
        {          
            if(Root == null)
            {
                Root = new BinaryTreeNode<T>(null, newValue);
                Count++;
                return;
            }

            var parent = find(parentValue);

            if (parent == null)
            {
                throw new Exception("Cannot find parent node");
            }

            var exists = find(Root, newValue) != null;

            if (exists)
            {
                throw new ArgumentNullException("value already exists");
            }

            switch (parent.Left)
            {
                case null when parent.Right == null:
                    parent.Left = new BinaryTreeNode<T>(parent, newValue);
                    break;
                case null:
                    parent.Left = new BinaryTreeNode<T>(parent, newValue);
                    break;
                default:
                    if (parent.Right == null)
                    {
                        parent.Right = new BinaryTreeNode<T>(parent, newValue);
                    }
                    else
                    {
                        throw new Exception("Cannot insert to a parent with two child node unambiguosly");
                    }

                    break;
            }

            Count++;
        }

        /// <summary>
        /// only deletes unambiguous nodes (a node with two children cannot be deleted unambiguously)
        ///  O(log(n)) worst O(n) for unbalanced tree
        /// </summary>
        /// <param name="value"></param>
        public void Delete(T value)
        {
            var node = find(value);

            if (node == null)
            {
                throw new Exception("Cannot find node");
            }

            switch (node.Left)
            {
                case null when node.Right == null:
                    if (node.Parent == null)
                    {
                        Root = null;
                    }
                    else
                    {
                        if (node.Parent.Left == node)
                        {
                            node.Parent.Left = null;
                        }
                        else
                        {
                            node.Parent.Right = null;
                        }
                    }

                    break;
                case null when node.Right != null:
                    node.Right.Parent = node.Parent;

                    if (node.Parent.Left == node)
                    {
                        node.Parent.Left = node.Right;
                    }
                    else
                    {
                        node.Parent.Right = node.Right;
                    }

                    break;
                default:
                    if (node.Right == null && node.Left != null)
                    {
                        node.Left.Parent = node.Parent;

                        if (node.Parent.Left == node)
                        {
                            node.Parent.Left = node.Left;
                        }
                        else
                        {
                            node.Parent.Right = node.Left;
                        }
                    }
                    else
                    {
                        throw new Exception("Cannot delete two child node unambiguosly");
                    }

                    break;
            }

            Count--;

        }
    }
}
