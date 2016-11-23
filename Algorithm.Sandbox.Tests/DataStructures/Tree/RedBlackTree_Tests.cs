using Algorithm.Sandbox.DataStructures;
using Algorithm.Sandbox.Tests.DataStructures.Tree.TestHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Algorithm.Sandbox.Tests.DataStructures
{
    [TestClass]
    public class RedBlackTree_Tests
    {
        /// <summary>
        ///  Smoke test
        /// </summary>
        [TestMethod]
        public void RedBlackTree_Smoke_Test()
        {
            //insert test
            var tree = new AsRedBlackTree<int>();

            Assert.AreEqual(-1, tree.GetHeight());

            tree.Insert(1);
            tree.Insert(2);
            tree.Insert(3);
            tree.Insert(4);
            tree.Insert(5);
            tree.Insert(6);
            tree.Insert(7);
            tree.Insert(8);
            tree.Insert(9);
            tree.Insert(10);
            tree.Insert(11);

            Assert.AreEqual(11, tree.Count);
            //delete
            tree.Delete(1);
            tree.Delete(2);
            tree.Delete(3);
            tree.Delete(4);
            tree.Delete(5);
            tree.Delete(6);
            tree.Delete(7);
            tree.Delete(8);
            tree.Delete(9);
            tree.Delete(10);
            tree.Delete(11);

            Assert.AreEqual(0, tree.Count);
        }

        [TestMethod]
        public void RedBlackTree_StressTest()
        {
            var nodeCount = 1000 * 10;

            var rnd = new Random();
            var randomNumbers = Enumerable.Range(1, nodeCount)
                                .OrderBy(x => rnd.Next())
                                .ToList();

            var tree = new AsRedBlackTree<int>();

            for (int i = 0; i < nodeCount; i++)
            {
                tree.Insert(randomNumbers[i]);
                Assert.IsTrue(tree.HasItem(randomNumbers[i]));
                Assert.IsTrue(BinarySearchTreeTester<int>.VerifyIsBinarySearchTree(tree.Root));

                var actualHeight = tree.GetHeight();

                //http://doctrina.org/maximum-height-of-red-black-tree.html
                var maxHeight = 2 * Math.Log(nodeCount + 1, 2);

                Assert.IsTrue(actualHeight < maxHeight);
                Assert.IsTrue(tree.Count == i + 1);
            }


            //shuffle again before deletion tests
            randomNumbers = Enumerable.Range(1, nodeCount)
                                   .OrderBy(x => rnd.Next())
                                   .ToList();

            for (int i = 0; i < nodeCount; i++)
            {
                tree.Delete(randomNumbers[i]);
                Assert.IsTrue(BinarySearchTreeTester<int>.VerifyIsBinarySearchTree(tree.Root));

                var actualHeight = tree.GetHeight();

                //http://doctrina.org/maximum-height-of-red-black-tree.html
                var maxHeight = 2 * Math.Log(nodeCount + 1, 2);

                Assert.IsTrue(actualHeight < maxHeight);
                Assert.IsTrue(tree.Count == nodeCount - 1 - i);
            }

            Assert.IsTrue(tree.Count == 0);
        }
    }
}
