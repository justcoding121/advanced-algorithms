using Algorithm.Sandbox.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Algorithm.Sandbox.Tests.DataStructures
{
    [TestClass]
    public class AVLTree_Tests
    {
        /// <summary>
        /// A tree test
        /// </summary>
        [TestMethod]
        public void AVLTree_Test()
        {
            //insert test
            var tree = new AsAVLTree<int>();
            Assert.AreEqual(tree.GetHeight(), -1);

            tree.Insert(11);
            Assert.AreEqual(tree.GetHeight(), 0);

            tree.Insert(6);
            Assert.AreEqual(tree.GetHeight(), 1);

            tree.Insert(8);
            Assert.AreEqual(tree.GetHeight(), 2);

            tree.Insert(19);
            Assert.AreEqual(tree.GetHeight(), 2);

            tree.Insert(4);
            Assert.AreEqual(tree.GetHeight(), 2);

            tree.Insert(10);
            Assert.AreEqual(tree.GetHeight(), 3);

            tree.Insert(5);
            Assert.AreEqual(tree.GetHeight(), 3);

            tree.Insert(17);
            Assert.AreEqual(tree.GetHeight(), 3);

            tree.Insert(43);
            Assert.AreEqual(tree.GetHeight(), 3);

            tree.Insert(49);
            Assert.AreEqual(tree.GetHeight(), 3);

            tree.Insert(31);
            Assert.AreEqual(tree.GetHeight(), 3);

            //delete
            tree.Delete(43);
            tree.Delete(11);
            tree.Delete(6);
            tree.Delete(8);
            tree.Delete(19);
            tree.Delete(4);
            tree.Delete(10);
            tree.Delete(5);
            tree.Delete(17);
            tree.Delete(49);
            tree.Delete(31);

            Assert.AreEqual(tree.GetHeight(), -1);
            Assert.AreEqual(tree.Count, 0);

            tree.Insert(31);
        }
    }
}
