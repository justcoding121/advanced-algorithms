using System;

namespace Algorithm.Sandbox.DataStructures
{
    public class AsBTreeNode<T> where T : IComparable
    {
        public T Value { get; set; }

        public AsBTree<T> Left { get; set; }
        public AsBTree<T> Right { get; set; }

        public bool IsLeaf => Left == null && Right == null;

        public AsBTreeNode(T value)
        {
            this.Value = value;
        }

    }

    public class AsBTree<T> where T : IComparable
    {
        public AsBTreeNode<T> Root { get; set; }

    }
}
