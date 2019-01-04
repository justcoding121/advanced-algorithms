using System;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.DataStructures
{
    internal abstract class BSTNodeBase<T> where T : IComparable
    {
        //Count of nodes under this node including this node.
        //Used to fasten kth smallest computation.
        internal int Count { get; set; } = 1;

        internal virtual BSTNodeBase<T> Parent { get; set; }

        internal virtual BSTNodeBase<T> Left { get; set; }
        internal virtual BSTNodeBase<T> Right { get; set; }

        internal T Value { get; set; }

        internal bool IsLeftChild => Parent.Left == this;
        internal bool IsRightChild => Parent.Right == this;

        internal bool IsLeaf => Left == null && Right == null;

    }
}
