using System;

namespace Algorithm.Sandbox.DataStructures
{
    public class AsSplayTreeNode<T> where T : IComparable
    {
        public T Value { get; set; }

        public AsSplayTree<T> Left { get; set; }
        public AsSplayTree<T> Right { get; set; }

        public bool IsLeaf => Left == null && Right == null;

        public AsSplayTreeNode(T value)
        {
            this.Value = value;
        }

    }

    public class AsSplayTree<T> where T : IComparable
    {
        public AsSplayTreeNode<T> Root { get; set; }

    }
}
