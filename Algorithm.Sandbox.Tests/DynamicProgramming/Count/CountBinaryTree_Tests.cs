using Algorithm.Sandbox.DynamicProgramming.Count;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.Tests.DynamicProgramming.Count
{
    /// <summary>
    /// Problem details below
    /// http://www.geeksforgeeks.org/find-all-possible-trees-with-given-inorder-traversal/
    /// </summary>
    [TestClass]
    public class CountBinaryTree_Tests
    {
        [TestMethod]
        public void Count_Smoke_Test()
        {
            Assert.AreEqual(1, CountBinaryTree.Count(1));
            Assert.AreEqual(2, CountBinaryTree.Count(2));
            Assert.AreEqual(5, CountBinaryTree.Count(3));
            Assert.AreEqual(14, CountBinaryTree.Count(4));
            Assert.AreEqual(42, CountBinaryTree.Count(5));
        }
    }
}
