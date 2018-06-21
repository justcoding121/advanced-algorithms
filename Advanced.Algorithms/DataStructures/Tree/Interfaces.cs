using System;

namespace Advanced.Algorithms.DataStructures
{
    internal interface IBSTNode<T> where T : IComparable
    {
        IBSTNode<T> Parent { get;  }

        IBSTNode<T> Left { get;  }
        IBSTNode<T> Right { get;  }

        T Value { get;  }

        bool IsLeftChild { get; }
        bool IsRightChild { get; }
    }
}
