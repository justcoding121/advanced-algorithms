using System;

namespace Algorithm.Sandbox.DataStructures
{
    public class AsBMaxHeapNode<T> where T : IComparable
    {
        public T Value { get; set; }

        public AsBMaxHeap<T> Left { get; set; }
        public AsBMaxHeap<T> Right { get; set; }

        public bool IsLeaf => Left == null && Right == null;

        public AsBMaxHeapNode(T value)
        {
            this.Value = value;
        }

    }

    public class AsBMaxHeap<T> where T : IComparable
    {
        public AsBMaxHeapNode<T> Root { get; set; }

    }
}
