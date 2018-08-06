using System;
using System.Collections.Generic;

namespace Advanced.Algorithms.DataStructures
{
    internal class BinomialHeapNode<T> : IComparable where T : IComparable
    {
        internal T Value { get; set; }
        internal int Degree => Children.Count;

        internal BinomialHeapNode<T> Parent { get; set; }
        internal List<BinomialHeapNode<T>> Children { get; set; }

        internal BinomialHeapNode(T value)
        {
            this.Value = value;

            Children = new List<BinomialHeapNode<T>>();
        }

        public int CompareTo(object obj)
        {
            return Value.CompareTo(((BinomialHeapNode<T>)obj).Value);
        }
    }

}
