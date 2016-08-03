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
            //insert and check height
            var tree = new AsBTree<int>(0);
            Assert.AreEqual(tree.GetHeight(), 0);

            tree.Insert(1);
            Assert.AreEqual(tree.GetHeight(), 1);

            tree.Insert(2);
            Assert.AreEqual(tree.GetHeight(), 1);

            tree.Insert(3);
            Assert.AreEqual(tree.GetHeight(), 2);

            tree.Insert(4);
            Assert.AreEqual(tree.GetHeight(), 2);

            tree.Insert(5);
            Assert.AreEqual(tree.GetHeight(), 2);

            tree.Insert(6);
            Assert.AreEqual(tree.GetHeight(), 2);

            tree.Insert(7);
            Assert.AreEqual(tree.GetHeight(), 3);

            tree.Insert(8);
            Assert.AreEqual(tree.GetHeight(), 3);

            tree.Insert(9);
            Assert.AreEqual(tree.GetHeight(), 3);

            tree.Insert(10);
            Assert.AreEqual(tree.GetHeight(), 3);

            tree.Insert(11);
            Assert.AreEqual(tree.GetHeight(), 3);

            tree.Insert(12);
            Assert.AreEqual(tree.GetHeight(), 3);

            tree.Insert(13);
            Assert.AreEqual(tree.GetHeight(), 3);

            tree.Insert(14);
            Assert.AreEqual(tree.GetHeight(), 3);

            tree.Insert(15);
            Assert.AreEqual(tree.GetHeight(), 4);


            //delete and check height
            tree.Delete(0);
            Assert.AreEqual(tree.GetHeight(), 3);

            tree.Delete(1);
            Assert.AreEqual(tree.GetHeight(), 3);

            tree.Delete(2);
            Assert.AreEqual(tree.GetHeight(), 3);

            tree.Delete(3);
            Assert.AreEqual(tree.GetHeight(), 3);

            tree.Delete(4);
            Assert.AreEqual(tree.GetHeight(), 3);

            tree.Delete(5);
            Assert.AreEqual(tree.GetHeight(), 3);

            tree.Delete(6);
            Assert.AreEqual(tree.GetHeight(), 3);

            tree.Delete(7);
            Assert.AreEqual(tree.GetHeight(), 3);

            tree.Delete(8);
            Assert.AreEqual(tree.GetHeight(), 2);

            tree.Delete(9);
            Assert.AreEqual(tree.GetHeight(), 2);

            tree.Delete(10);
            Assert.AreEqual(tree.GetHeight(), 2);

            tree.Delete(11);
            Assert.AreEqual(tree.GetHeight(), 2);

            tree.Delete(12);
            Assert.AreEqual(tree.GetHeight(), 1);

            tree.Delete(13);
            Assert.AreEqual(tree.GetHeight(), 1);

            tree.Delete(14);
            Assert.AreEqual(tree.GetHeight(), 0);

            tree.Delete(15);
            Assert.AreEqual(tree.GetHeight(), -1);

            for (int i = 1; i < 100; i++)
            {   
                tree.Insert(i);

                var height = tree.GetHeight();
                var expectedHeight = Math.Log(tree.Count, 2);

                Assert.IsTrue(height <= expectedHeight);
            }

            Assert.AreEqual(tree.Count, 99);
        
            for (int i = 99; i >= 1; i--)
            {
                var height = tree.GetHeight();
                var expectedHeight = Math.Log(tree.Count, 2);

                Assert.IsTrue(height <= expectedHeight);

                tree.Delete(i);
               
            }

            Assert.AreEqual(tree.Count, 0);
        }
    }
}
