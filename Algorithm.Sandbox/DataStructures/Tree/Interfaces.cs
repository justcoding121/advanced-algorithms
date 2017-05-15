using System;

namespace Algorithm.Sandbox.DataStructures.Tree
{
    public interface IBSTNode<T> where T : IComparable
    {
        IBSTNode<T> Left { get;  }
        IBSTNode<T> Right { get;  }

        T Value { get;  }
    }
}
