using System;

namespace Advanced.Algorithms.DataStructures
{
    internal class PairingHeapNode<T> : IComparable where T : IComparable
    {
        internal T Value { get; set; }

        internal PairingHeapNode<T> ChildrenHead { get; set; }
        internal bool IsHeadChild => Previous != null && Previous.ChildrenHead == this;

        internal PairingHeapNode(T value)
        {
            this.Value = value;
        }

        internal PairingHeapNode<T> Previous;
        internal PairingHeapNode<T> Next;

        public int CompareTo(object obj)
        {
            return Value.CompareTo(((PairingHeapNode<T>) obj).Value);
        }
    }

}
