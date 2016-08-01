using System;

namespace Algorithm.Sandbox.DataStructures
{
    public class AsBMinHeapNode<T> where T : IComparable
    {
        public T Value { get; set; }

        public AsBMinHeap<T> Left { get; set; }
        public AsBMinHeap<T> Right { get; set; }

        public bool IsLeaf => Left == null && Right == null;

        public AsBMinHeapNode(T value)
        {
            this.Value = value;
        }

    }

    public class AsBMinHeap<T> where T : IComparable
    {
        public AsBMinHeapNode<T> Root { get; set; }

    }
}
