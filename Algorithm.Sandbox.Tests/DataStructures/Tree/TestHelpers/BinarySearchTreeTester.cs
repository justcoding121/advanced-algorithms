using Algorithm.Sandbox.DataStructures.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.Tests.DataStructures.Tree.TestHelpers
{
    public class BinarySearchTreeTester<T> where T:IComparable
    {

        public static bool VerifyIsBinarySearchTree(IBSTNode<T> node)
        {
            if (node == null)
            {
                return true;
            }

            if (node.Left != null)
            {
                if (node.Left.Value.CompareTo(node.Value) > 0)
                {
                    return false;
                }
            }

            if (node.Right != null)
            {
                if (node.Right.Value.CompareTo(node.Value) < 0)
                {
                    return false;
                }
            }
            return VerifyIsBinarySearchTree(node.Left) &&
                VerifyIsBinarySearchTree(node.Right);
        }
    }
}
