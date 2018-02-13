using Advanced.Algorithms.DataStructures.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced.Algorithms.Tests.DataStructures.Tree.TestHelpers
{
    public class BinarySearchTreeTester<T> where T:IComparable
    {

        public static bool VerifyIsBinarySearchTree(IBSTNode<T> node)
        {
            return VerifyIsBinarySearchTree(node, int.MinValue, int.MaxValue);
        }
        
        public static bool VerifyIsBinarySearchTree(IBSTNode<T> node, int lowerBound, int upperBound)
        {
            if (node == null)
            {
                return true;
            }

             if (node.Value >= upperBound || node.Value <= lowerBound)
            {
                return false;
            }
     
            return VerifyIsBinarySearchTree(node.Left, lowerBound, node.Value) &&
                VerifyIsBinarySearchTree(node.Right, node.Value, upperBound);
        }
    }
}
