using System;

namespace Advanced.Algorithms.DataStructures;

internal class FibonacciHeapNode<T> : IComparable where T : IComparable
{
    internal int Degree;
    internal FibonacciHeapNode<T> Next;

    internal FibonacciHeapNode<T> Previous;

    internal FibonacciHeapNode(T value)
    {
        Value = value;
    }

    internal T Value { get; set; }
    internal FibonacciHeapNode<T> ChildrenHead { get; set; }

    internal FibonacciHeapNode<T> Parent { get; set; }
    internal bool LostChild { get; set; }

    public int CompareTo(object obj)
    {
        return Value.CompareTo(((FibonacciHeapNode<T>)obj).Value);
    }
}