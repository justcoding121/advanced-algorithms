using System;
using Advanced.Algorithms.DataStructures;

namespace Advanced.Algorithms.Tests.DataStructures
{
    internal class BinarySearchTreeTester<T> where T:IComparable
    {
        
        public static bool VerifyIsBinarySearchTree(BSTNodeBase<T> node, T lowerBound, T upperBound)
        {
            if (node == null)
            {
                return true;
            }

             if (node.Value.CompareTo(upperBound)>= 0 || node.Value.CompareTo(lowerBound) <= 0)
            {
                return false;
            }
     
            return VerifyIsBinarySearchTree(node.Left, lowerBound, node.Value) &&
                VerifyIsBinarySearchTree(node.Right, node.Value, upperBound);
        }
    }
}
