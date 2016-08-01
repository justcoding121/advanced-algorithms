using System;

namespace Algorithm.Sandbox.DataStructures
{
    public class AsRedBlackTreeNode<T> where T : IComparable
    {
        public T Value { get; set; }

        public AsRedBlackTree<T> Left { get; set; }
        public AsRedBlackTree<T> Right { get; set; }

        public bool IsLeaf => Left == null && Right == null;

        public AsRedBlackTreeNode(T value)
        {
            this.Value = value;
        }
    }

    public class AsRedBlackTree<T> where T : IComparable
    {
        public AsRedBlackTreeNode<T> Root { get; set; }

    }
}
