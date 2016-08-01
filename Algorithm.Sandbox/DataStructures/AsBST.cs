using System;

namespace Algorithm.Sandbox.DataStructures
{
    public class AsBSTNode<T> where T : IComparable
    {
        public T Value { get; set; }

        public AsBST<T> Left { get; set; }
        public AsBST<T> Right { get; set; }

        public bool IsLeaf => Left == null && Right == null;

        public AsBSTNode(T value)
        {
            this.Value = value;
        }

    }

    public class AsBST<T> where T : IComparable
    {
        public AsBSTNode<T> Root { get; set; }

    }
}
