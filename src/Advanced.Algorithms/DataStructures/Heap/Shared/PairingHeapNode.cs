using System;

namespace Advanced.Algorithms.DataStructures;

internal class PairingHeapNode<T> : IComparable where T : IComparable
{
    internal PairingHeapNode<T> Next;

    internal PairingHeapNode<T> Previous;

    internal PairingHeapNode(T value)
    {
        Value = value;
    }

    internal T Value { get; set; }

    internal PairingHeapNode<T> ChildrenHead { get; set; }
    internal bool IsHeadChild => Previous != null && Previous.ChildrenHead == this;

    public int CompareTo(object obj)
    {
        return Value.CompareTo(((PairingHeapNode<T>)obj).Value);
    }
}