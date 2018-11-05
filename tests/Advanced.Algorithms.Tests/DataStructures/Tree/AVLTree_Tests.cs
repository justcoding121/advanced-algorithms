using Advanced.Algorithms.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Advanced.Algorithms.Tests.DataStructures
{
    [TestClass]
    public class AvlTreeTests
    {
        /// <summary>
        /// Smoke test
        /// </summary>
        [TestMethod]
        public void AVLTree_Smoke_Test()
        {
            //insert test
            var tree = new AVLTree<int>();
            Assert.AreEqual(-1, tree.GetHeight());

            tree.Insert(1);
            Assert.AreEqual(0, tree.GetHeight());

            tree.Insert(2);
            Assert.AreEqual(1, tree.GetHeight());

            tree.Insert(3);
            Assert.AreEqual(1, tree.GetHeight());

            tree.Insert(4);
            Assert.AreEqual(2, tree.GetHeight());

            tree.Insert(5);
            Assert.AreEqual(2, tree.GetHeight());

            tree.Insert(6);
            Assert.AreEqual(2, tree.GetHeight());

            tree.Insert(7);
            Assert.AreEqual(2, tree.GetHeight());

            tree.Insert(8);
            Assert.AreEqual(3, tree.GetHeight());

            tree.Insert(9);
            Assert.AreEqual(3, tree.GetHeight());

            tree.Insert(10);
            Assert.AreEqual(3, tree.GetHeight());

            tree.Insert(11);
            Assert.AreEqual(3, tree.GetHeight());

            //IEnumerable test using linq
            Assert.AreEqual(tree.Count, tree.Count());

            //delete
            tree.Delete(1);
            Assert.AreEqual(3, tree.GetHeight());

            tree.Delete(2);
            Assert.AreEqual(3, tree.GetHeight());

            tree.Delete(3);
            Assert.AreEqual(3, tree.GetHeight());

            tree.Delete(4);
            Assert.AreEqual(2, tree.GetHeight());

            tree.Delete(5);
            Assert.AreEqual(2, tree.GetHeight());

            tree.Delete(6);
            Assert.AreEqual(2, tree.GetHeight());

            tree.Delete(7);
            Assert.AreEqual(2, tree.GetHeight());

            tree.Delete(8);
            Assert.AreEqual(1, tree.GetHeight());

            tree.Delete(9);
            Assert.AreEqual(1, tree.GetHeight());

            tree.Delete(10);
            Assert.AreEqual(0, tree.GetHeight());

            tree.Delete(11);
            Assert.AreEqual(tree.GetHeight(), -1);

            Assert.AreEqual(tree.Count, 0);

            tree.Insert(31);
        }

        [TestMethod]
        public void AVLTree_Accuracy_Test()
        {
            var nodeCount = 1000;

            var rnd = new Random();
            var sorted = Enumerable.Range(1, nodeCount).ToList();
            var randomNumbers = sorted
                                .OrderBy(x => rnd.Next())
                                .ToList();

            var tree = new AVLTree<int>(true);

            for (int i = 0; i < nodeCount; i++)
            {
                tree.Insert(randomNumbers[i]);

                Assert.IsTrue(tree.HasItem(randomNumbers[i]));
                Assert.IsTrue(tree.Root.IsBinarySearchTree(int.MinValue, int.MaxValue));
                tree.Root.VerifyCount();

                var actualHeight = tree.GetHeight();

                //http://stackoverflow.com/questions/30769383/finding-the-minimum-and-maximum-height-in-a-avl-tree-given-a-number-of-nodes
                var maxHeight = 1.44 * Math.Log(nodeCount + 2, 2) - 0.328;

                Assert.IsTrue(actualHeight < maxHeight);
                Assert.IsTrue(tree.Count == i + 1);
            }

            for (int i = 0; i < sorted.Count; i++)
            {
                Assert.AreEqual(sorted[i], tree.ElementAt(i));
                Assert.AreEqual(i, tree.IndexOf(sorted[i]));
            }

            randomNumbers = Enumerable.Range(1, nodeCount)
                                .OrderBy(x => rnd.Next())
                                .ToList();

            //IEnumerable test using linq
            Assert.AreEqual(tree.Count, tree.Count());
            Assert.AreEqual(tree.Count, tree.AsEnumerableDesc().Count());

            for (int i = 0; i < nodeCount; i++)
            {
                if (rnd.NextDouble() >= 0.5)
                {
                    tree.Delete(randomNumbers[i]);
                }
                else
                {
                    var index = tree.IndexOf(randomNumbers[i]);
                    Assert.AreEqual(tree.ElementAt(index), randomNumbers[i]);
                    tree.RemoveAt(index);
                }

                Assert.IsTrue(tree.Root.IsBinarySearchTree(int.MinValue, int.MaxValue));
                tree.Root.VerifyCount();

                var actualHeight = tree.GetHeight();

                //http://stackoverflow.com/questions/30769383/finding-the-minimum-and-maximum-height-in-a-avl-tree-given-a-number-of-nodes
                var maxHeight = 1.44 * Math.Log(nodeCount + 2, 2) - 0.328;

                Assert.IsTrue(actualHeight < maxHeight);
            }

            Assert.IsTrue(tree.Count == 0);
        }

        [TestMethod]
        public void AVLTree_BulkInit_Test()
        {
            var nodeCount = 1000;

            var rnd = new Random();
            var randomNumbers = Enumerable.Range(1, nodeCount).ToList();

            var tree = new AVLTree<int>(randomNumbers);

            Assert.IsTrue(tree.Root.IsBinarySearchTree(int.MinValue, int.MaxValue));
            Assert.AreEqual(tree.Count, tree.Count());

            tree.Root.VerifyCount();

            for (int i = 0; i < nodeCount; i++)
            {
                tree.Delete(randomNumbers[i]);

                tree.Root.VerifyCount();
                Assert.IsTrue(tree.Root.IsBinarySearchTree(int.MinValue, int.MaxValue));

                var actualHeight = tree.GetHeight();

                //http://stackoverflow.com/questions/30769383/finding-the-minimum-and-maximum-height-in-a-avl-tree-given-a-number-of-nodes
                var maxHeight = 1.44 * Math.Log(nodeCount + 2, 2) - 0.328;

                Assert.IsTrue(actualHeight < maxHeight);

                Assert.IsTrue(tree.Count == nodeCount - 1 - i);
            }

            Assert.IsTrue(tree.Count == 0);
        }

        [TestMethod]
        public void AVLTree_Stress_Test()
        {
            var nodeCount = 1000 * 10;

            var rnd = new Random();
            var randomNumbers = Enumerable.Range(1, nodeCount)
                                .OrderBy(x => rnd.Next())
                                .ToList();

            var tree = new AVLTree<int>();

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
