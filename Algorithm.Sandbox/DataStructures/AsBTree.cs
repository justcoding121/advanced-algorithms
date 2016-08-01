using System;

namespace Algorithm.Sandbox.DataStructures
{
    public class AsBTreeNode<I, V> where I : IComparable
    {
        public I Identifier { get; set; }
        public V Value { get; set; }

        public AsBTree<I, V> Left { get; set; }
        public AsBTree<I, V> Right { get; set; }

        public bool IsLeaf => Left == null && Right == null;

        public AsBTreeNode(I identifier, V value)
        {
            this.Identifier = identifier;
            this.Value = value;
        }

    }

    public class AsBTree<I, V> where I : IComparable
    {
        public AsBTreeNode<I, V> Root { get; set; }

    }
}
