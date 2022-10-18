using System;

namespace Advanced.Algorithms.DataStructures;

internal abstract class BstNodeBase<T> where T : IComparable
{
    //Count of nodes under this node including this node.
    //Used to fasten kth smallest computation.
    internal int Count { get; set; } = 1;

    internal virtual BstNodeBase<T> Parent { get; set; }

    internal virtual BstNodeBase<T> Left { get; set; }
    internal virtual BstNodeBase<T> Right { get; set; }

    internal T Value { get; set; }

    internal bool IsLeftChild => Parent.Left == this;
    internal bool IsRightChild => Parent.Right == this;

    internal bool IsLeaf => Left == null && Right == null;
}