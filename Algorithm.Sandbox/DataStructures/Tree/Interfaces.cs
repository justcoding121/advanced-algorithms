using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.DataStructures.Tree
{
    public interface AsIBSTNode<T> where T : IComparable
    {
        AsIBSTNode<T> Left { get;  }
        AsIBSTNode<T> Right { get;  }

        T Value { get;  }
    }
}
