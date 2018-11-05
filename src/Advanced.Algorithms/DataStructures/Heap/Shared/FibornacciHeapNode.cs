using System;

namespace Advanced.Algorithms.DataStructures
{
    internal class FibornacciHeapNode<T> : IComparable where T : IComparable
    {
        internal T Value { get; set; }

        internal int Degree;
        internal FibornacciHeapNode<T> ChildrenHead { get; set; }

        internal FibornacciHeapNode<T> Parent { get; set; }
        internal bool LostChild { get; set; }

        internal FibornacciHeapNode<T> Previous;
        internal FibornacciHeapNode<T> Next;

        internal FibornacciHeapNode(T value)
        {
            Value = value;
        }

        public int CompareTo(object obj)
        {
            return Value.CompareTo(((FibornacciHeapNode<T>) obj).Value);
        }
    }

}
