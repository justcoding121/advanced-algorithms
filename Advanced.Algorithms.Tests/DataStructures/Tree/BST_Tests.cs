using Advanced.Algorithms.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Advanced.Algorithms.Tests.DataStructures
{
    [TestClass]
    public class BST_Tests
    {
        /// <summary>
        /// A tree test
        /// </summary>
        [TestMethod]
        public void BST_Test()
        {
            //insert test
            var tree = new BST<int>();
            Assert.AreEqual(tree.getHeight(), -1);

            tree.Insert(11);
            Assert.AreEqual(tree.getHeight(), 0);

            tree.Insert(6);
            Assert.AreEqual(tree.getHeight(), 1);

            tree.Insert(8);
            Assert.AreEqual(tree.getHeight(), 2);

            tree.Insert(19);
            Assert.AreEqual(tree.getHeight(), 2);

            tree.Insert(4);
            Assert.AreEqual(tree.getHeight(), 2);

            tree.Insert(10);
            Assert.AreEqual(tree.getHeight(), 3);

            tree.Insert(5);
            Assert.AreEqual(tree.getHeight(), 3);

            tree.Insert(17);
            Assert.AreEqual(tree.getHeight(), 3);

            tree.Insert(43);
            Assert.AreEqual(tree.getHeight(), 3);

            tree.Insert(49);
            Assert.AreEqual(tree.getHeight(), 3);

            tree.Insert(31);
            Assert.AreEqual(tree.getHeight(), 3);

            Assert.IsTrue(BinarySearchTreeTester<int>.VerifyIsBinarySearchTree(tree.Root, int.MinValue, int.MaxValue));

            //IEnumerable test using linq
            Assert.AreEqual(tree.Count, tree.Count());

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

            Assert.AreEqual(tree.getHeight(), -1);
            Assert.AreEqual(tree.Count, 0);

            tree.Insert(31);
        }

        [TestMethod]
        public void BST_StressTest()
        {
            var nodeCount = 1000 * 10;

            var rnd = new Random();
            var randomNumbers = Enumerable.Range(1, nodeCount)
                                .OrderBy(x => rnd.Next())
                                .ToList();

            var tree = new BST<int>();

            for (int i = 0; i < nodeCount; i++)
            {
                tree.Insert(randomNumbers[i]);
                Assert.IsTrue(tree.Count == i + 1);
            }


            //shuffle again before deletion tests
            randomNumbers = Enumerable.Range(1, nodeCount)
                                   .OrderBy(x => rnd.Next())
                                   .ToList();

            //IEnumerable test using linq
            Assert.AreEqual(tree.Count, tree.Count());

            for (int i = 0; i < nodeCount; i++)
            {
                tree.Delete(randomNumbers[i]);
                Assert.IsTrue(tree.Count == nodeCount - 1 - i);
            }

            Assert.IsTrue(tree.Count == 0);
        }
    }
}
