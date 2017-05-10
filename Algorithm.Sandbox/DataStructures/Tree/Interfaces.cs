using System;

namespace Algorithm.Sandbox.DataStructures.Tree
{
    public interface AsIBSTNode<T> where T : IComparable
    {
        AsIBSTNode<T> Left { get;  }
        AsIBSTNode<T> Right { get;  }

        T Value { get;  }
    }
}
