using Algorithm.Sandbox.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Algorithm.Sandbox.Tests.DataStructures
{
    [TestClass]
    public class BTree_Tests
    {
        /// <summary>
        /// A tree test
        /// </summary>
        [TestMethod]
        public void BTree_Test()
        {
            var tree = new AsBTree<int>();
            Assert.AreEqual(tree.GetHeight(), -1);

            tree.Insert(0, 0);
            Assert.AreEqual(tree.GetHeight(), 0);

            tree.Insert(0, 1);
            Assert.AreEqual(tree.GetHeight(), 1);

            tree.Insert(0, 2);
            Assert.AreEqual(tree.GetHeight(), 1);

            tree.Insert(1, 3);
            Assert.AreEqual(tree.GetHeight(), 2);

            try
            {
                tree.Delete(0);
            }
            catch(Exception e)
            {
                Assert.IsTrue(e.Message.StartsWith("Cannot delete two child node"));
            }
           
            Assert.AreEqual(tree.GetHeight(), 2);

            tree.Delete(1);
            Assert.AreEqual(tree.GetHeight(), 1);

            tree.Delete(3);
            Assert.AreEqual(tree.GetHeight(), 1);

            tree.Delete(2);
            Assert.AreEqual(tree.GetHeight(), 0);

            tree.Delete(0);
            Assert.AreEqual(tree.GetHeight(), -1);

            tree.Insert(0, 0);
            Assert.AreEqual(tree.GetHeight(), 0);

            tree.Insert(0, 1);
            Assert.AreEqual(tree.GetHeight(), 1);

            tree.Insert(0, 2);
            Assert.AreEqual(tree.GetHeight(), 1);

            tree.Insert(1, 3);
            Assert.AreEqual(tree.GetHeight(), 2);
        }
    }
}
