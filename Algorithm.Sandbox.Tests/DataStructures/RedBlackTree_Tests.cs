using Algorithm.Sandbox.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Algorithm.Sandbox.Tests.DataStructures
{
    [TestClass]
    public class RedBlackTree_Tests
    {
        /// <summary>
        ///  Smoke test
        /// </summary>
        [TestMethod]
        public void RedBlackTree_Test()
        {
            //insert test
            var tree = new AsRedBlackTree<int>();
            Assert.AreEqual(-1, tree.GetHeight());

            tree.Insert(1);
            Assert.AreEqual(0, tree.GetHeight());

            tree.Insert(2);
            Assert.AreEqual(1, tree.GetHeight());

            tree.Insert(3);
            tree.Insert(4);
            tree.Insert(5);
            tree.Insert(6);          
            tree.Insert(7);          
            tree.Insert(8);  
            tree.Insert(9);  
            tree.Insert(10);
            tree.Insert(11);
           
  
        }

        [TestMethod]
        public void RedBlackTree_StressTest()
        {
            var nodeCount = 1000 * 10;
           
            var tree = new AsRedBlackTree<int>();

            for (int i = 0; i < nodeCount; i++)
            {
                tree.Insert(i);
            }

            for (int i = 0; i < nodeCount; i++)
            {
                Assert.IsTrue(tree.HasItem(i));
            }

            var actualHeight = tree.GetHeight();
          
            //http://doctrina.org/maximum-height-of-red-black-tree.html
            var maxHeight = 2 * Math.Log(nodeCount + 1, 2);

            Assert.IsTrue(actualHeight < maxHeight);
        }
    }
}
