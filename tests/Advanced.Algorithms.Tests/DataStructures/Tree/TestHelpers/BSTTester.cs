using System;
using Advanced.Algorithms.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.DataStructures
{
    internal static class BSTTester 
    {
        internal static bool IsBinarySearchTree<T>(this BSTNodeBase<T> node, T lowerBound, T upperBound) where T:IComparable
        {
            if (node == null)
            {
                return true;
            }

             if (node.Value.CompareTo(upperBound)>= 0 || node.Value.CompareTo(lowerBound) <= 0)
            {
                return false;
            }
     
            return IsBinarySearchTree(node.Left, lowerBound, node.Value) &&
                IsBinarySearchTree(node.Right, node.Value, upperBound);
        }

        public static int VerifyCount<T>(this BSTNodeBase<T> node) where T : IComparable
        {
            if (node == null)
            {
                return 0;
            }

            var count = VerifyCount(node.Left) + VerifyCount(node.Right) + 1;

            Assert.AreEqual(count, node.Count);

            return count;
        }


        //O(log(n)) worst O(n) for unbalanced tree
        internal static int GetHeight<T>(this BSTNodeBase<T> node) where T : IComparable
        {
            if (node == null)
            {
                return -1;
            }

            return Math.Max(GetHeight(node.Left), GetHeight(node.Right)) + 1;
        }

    }
}
