using System;

namespace Algorithm.Sandbox.DataStructures
{
    public class AsBMinHeapNode<I, V> where I : IComparable
    {
        public I Identifier { get; set; }
        public V Value { get; set; }

        public AsBMinHeap<I, V> Left { get; set; }
        public AsBMinHeap<I, V> Right { get; set; }

        public bool IsLeaf => Left == null && Right == null;

        public AsBMinHeapNode(I identifier, V value)
        {
            this.Identifier = identifier;
            this.Value = value;
        }

    }

    public class AsBMinHeap<I, V> where I : IComparable
    {
        public AsBMinHeapNode<I, V> Root { get; set; }

    }
}
