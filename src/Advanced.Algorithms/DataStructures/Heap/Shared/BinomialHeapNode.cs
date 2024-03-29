﻿using System;
using System.Collections.Generic;

namespace Advanced.Algorithms.DataStructures;

internal class BinomialHeapNode<T> : IComparable where T : IComparable
{
    internal BinomialHeapNode(T value)
    {
        Value = value;

        Children = new List<BinomialHeapNode<T>>();
    }

    internal T Value { get; set; }
    internal int Degree => Children.Count;

    internal BinomialHeapNode<T> Parent { get; set; }
    internal List<BinomialHeapNode<T>> Children { get; set; }

    public int CompareTo(object obj)
    {
        return Value.CompareTo(((BinomialHeapNode<T>)obj).Value);
    }
}