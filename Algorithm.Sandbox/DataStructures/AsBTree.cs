using System;

namespace Algorithm.Sandbox.DataStructures
{
    public class AsBTreeNode<T> : IComparable where T : IComparable
    {
        public T Value { get; set; }

        public AsBTreeNode<T> Parent { get; set; }

        public AsBTreeNode<T> Left { get; set; }
        public AsBTreeNode<T> Right { get; set; }

        public bool IsLeaf => Left == null && Right == null;

        public AsBTreeNode(AsBTreeNode<T> parent, T value)
        {
            this.Parent = parent;
            this.Value = value;
        }

        public int CompareTo(object obj)
        {
            return CompareTo(obj as AsBTreeNode<T>);
        }

        public int CompareTo(AsBTreeNode<T> node)
        {
            return Value.CompareTo(node.Value);
        }
    }

    public class AsBTree<T> where T : IComparable
    {
        public AsBTreeNode<T> Root { get; set; }

        //constructor
        public AsBTree(T value)
        {
            Root = new AsBTreeNode<T>(null, value);
        }

        //O(n)
        public bool HasItem(T value)
        {
            if (Root == null)
            {
                return false;
            }

            return Find(Root, value) != null;
        }

        public int GetHeight()
        {
            return GetHeight(Root);
        }

        public int GetHeight(AsBTreeNode<T> node)
        {
            if (node == null)
            {
                return -1;
            }

            return Math.Max(GetHeight(node.Left), GetHeight(node.Right)) + 1;
        }

        public void Insert(T value)
        {
            //todo
           
        }

        //remove the node with the given identifier from the descendants 
        public void Delete(T value)
        {
            //todo
        }

        //O(n)
        public AsBTreeNode<T> Find(T value)
        {
            if (Root == null)
            {
                return null;
            }

            return Find(Root, value);
        }


        //find the node with the given identifier among descendants of parent and parent
        //uses pre-order traversal
        public AsBTreeNode<T> Find(AsBTreeNode<T> parent, T value)
        {
            if (parent.Value.CompareTo(value) == 0)
            {
                return parent;
            }

            var left = Find(parent.Left, value);

            if (left != null)
            {
                return left;
            }

            var right = Find(parent.Right, value);

            if (right != null)
            {
                return right;
            }

            return null;

        }

    }
}
