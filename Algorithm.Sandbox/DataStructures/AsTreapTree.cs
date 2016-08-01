using System;

namespace Algorithm.Sandbox.DataStructures
{
    public class AsTreapTreeNode<T> where T : IComparable
    {
        public T Value { get; set; }

        public AsTreapTree<T> Left { get; set; }
        public AsTreapTree<T> Right { get; set; }

        public bool IsLeaf => Left == null && Right == null;

        public AsTreapTreeNode(T value)
        {
            this.Value = value;
        }

    }

    public class AsTreapTree<T> where T : IComparable
    {
        public AsTreapTreeNode<T> Root { get; set; }

    }
}
