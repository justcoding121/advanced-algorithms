using System;
using System.Collections.Generic;
using System.Linq;
using Advanced.Algorithms.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.DataStructures
{
    [TestClass]
    public class BstTests
    {
        /// <summary>
        ///     A tree test
        /// </summary>
        [TestMethod]
        public void BST_Test()
        {
            //insert test
            var tree = new Bst<int>();
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

            Assert.IsTrue(tree.Root.IsBinarySearchTree(int.MinValue, int.MaxValue));

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

            Assert.AreEqual(tree.GetHeight(), -1);
            Assert.AreEqual(tree.Count, 0);

            tree.Insert(31);
        }

        [TestMethod]
        public void BST_BulkInit_Test()
        {
            var nodeCount = 1000;

            var rnd = new Random();
            var sortedNumbers = Enumerable.Range(1, nodeCount).ToList();

            var tree = new Bst<int>(sortedNumbers);

            Assert.IsTrue(tree.Root.IsBinarySearchTree(int.MinValue, int.MaxValue));
            Assert.AreEqual(tree.Count, tree.Count());

            tree.Root.VerifyCount();

            for (var i = 0; i < nodeCount; i++)
            {
                Assert.IsTrue(tree.Root.IsBinarySearchTree(int.MinValue, int.MaxValue));
                tree.Delete(sortedNumbers[i]);

                Assert.IsTrue(tree.Count == nodeCount - 1 - i);
            }

            Assert.IsTrue(tree.Count == 0);
        }

        [TestMethod]
        public void BST_Accuracy_Test()
        {
            var nodeCount = 1000;

            var rnd = new Random();
            var sorted = Enumerable.Range(1, nodeCount).ToList();
            var randomNumbers = sorted
                .OrderBy(x => rnd.Next())
                .ToList();

            var tree = new Bst<int>();

            for (var i = 0; i < nodeCount; i++)
            {
                tree.Insert(randomNumbers[i]);
                tree.Root.VerifyCount();
                Assert.IsTrue(tree.Count == i + 1);
            }

            for (var i = 0; i < sorted.Count; i++)
            {
                Assert.AreEqual(sorted[i], tree.ElementAt(i));
                Assert.AreEqual(i, tree.IndexOf(sorted[i]));
            }

            //shuffle again before deletion tests
            randomNumbers = Enumerable.Range(1, nodeCount)
                .OrderBy(x => rnd.Next())
                .ToList();

            //IEnumerable test using linq
            Assert.AreEqual(tree.Count, tree.Count());
            Assert.AreEqual(tree.Count, tree.AsEnumerableDesc().Count());

            for (var i = 0; i < nodeCount; i++)
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

                tree.Root.VerifyCount();
                Assert.IsTrue(tree.Count == nodeCount - 1 - i);
            }

            Assert.IsTrue(tree.Count == 0);
        }

        [TestMethod]
        public void BST_Stress_Test()
        {
            var nodeCount = 1000 * 10;

            var rnd = new Random();
            var randomNumbers = Enumerable.Range(1, nodeCount)
                .OrderBy(x => rnd.Next())
                .ToList();

            var tree = new Bst<int>();

            for (var i = 0; i < nodeCount; i++)
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

            for (var i = 0; i < nodeCount; i++)
            {
                tree.Delete(randomNumbers[i]);
                Assert.IsTrue(tree.Count == nodeCount - 1 - i);
            }

            Assert.IsTrue(tree.Count == 0);
        }

        [TestMethod]
        public void BST_Empty_SingleNode_DeleteRoot_Enumerate()
        {
            var tree = new Bst<int>();

            Assert.AreEqual(0, tree.Count);
            Assert.AreEqual(0, tree.Count());
            Assert.IsFalse(tree.HasItem(1));
            Assert.ThrowsException<InvalidOperationException>(() => tree.Delete(1));

            tree.Insert(1);
            Assert.AreEqual(1, tree.Count);
            Assert.IsTrue(tree.HasItem(1));
            CollectionAssert.AreEqual(new[] { 1 }, tree.ToList());
            Assert.AreEqual(1, tree.FindMin());
            Assert.AreEqual(1, tree.FindMax());

            tree.Delete(1);
            Assert.AreEqual(0, tree.Count);
            Assert.AreEqual(0, tree.Count());
            Assert.IsFalse(tree.HasItem(1));
        }

        [TestMethod]
        public void BST_Index_Next_Desc_Duplicate()
        {
            var tree = new Bst<int>();

            Assert.ThrowsException<ArgumentNullException>(() => tree.ElementAt(0));
            Assert.ThrowsException<ArgumentException>(() => tree.RemoveAt(0));
            Assert.AreEqual(-1, tree.IndexOf(1));
            Assert.AreEqual(0, tree.NextLower(1));
            Assert.AreEqual(0, tree.NextHigher(1));

            tree.Insert(2);
            tree.Insert(1);
            tree.Insert(3);
            Assert.ThrowsException<ArgumentException>(() => tree.Insert(2));

            Assert.AreEqual(0, tree.IndexOf(1));
            Assert.AreEqual(1, tree.ElementAt(0));
            Assert.AreEqual(2, tree.ElementAt(1));
            Assert.AreEqual(3, tree.ElementAt(2));
            Assert.AreEqual(1, tree.NextLower(2));
            Assert.AreEqual(3, tree.NextHigher(2));
            Assert.AreEqual(0, tree.NextLower(1));
            Assert.AreEqual(0, tree.NextHigher(3));
            Assert.AreEqual(0, tree.NextHigher(99));

            CollectionAssert.AreEqual(new[] { 3, 2, 1 }, tree.AsEnumerableDesc().ToList());

            Assert.AreEqual(1, tree.RemoveAt(0));
            Assert.AreEqual(2, tree.Count);
            Assert.IsFalse(tree.HasItem(1));
            Assert.ThrowsException<ArgumentException>(() => tree.Delete(99));

            var fromSorted = new Bst<int>(new[] { 1, 2, 3, 4 });
            Assert.AreEqual(4, fromSorted.Count);
            Assert.AreEqual(1, fromSorted.FindMin());
            Assert.AreEqual(4, fromSorted.FindMax());
        }

        [TestMethod]
        public void BST_Delete_OneChild_Cases()
        {
            // root with only left child
            var leftRoot = new Bst<int>();
            leftRoot.Insert(2);
            leftRoot.Insert(1);
            leftRoot.Delete(2);
            Assert.AreEqual(1, leftRoot.Count);
            Assert.AreEqual(1, leftRoot.FindMin());

            // root with only right child
            var rightRoot = new Bst<int>();
            rightRoot.Insert(1);
            rightRoot.Insert(2);
            rightRoot.Delete(1);
            Assert.AreEqual(1, rightRoot.Count);
            Assert.AreEqual(2, rightRoot.FindMax());

            // non-root left child with only left subtree
            var leftChild = new Bst<int>();
            leftChild.Insert(3);
            leftChild.Insert(2);
            leftChild.Insert(1);
            leftChild.Delete(2);
            Assert.IsFalse(leftChild.HasItem(2));
            Assert.IsTrue(leftChild.HasItem(1));

            // non-root right child with only left subtree
            var rightAsLeft = new Bst<int>();
            rightAsLeft.Insert(1);
            rightAsLeft.Insert(3);
            rightAsLeft.Insert(2);
            rightAsLeft.Delete(3);
            Assert.IsFalse(rightAsLeft.HasItem(3));
            Assert.IsTrue(rightAsLeft.HasItem(2));

            // non-root left child with only right subtree
            var leftAsRight = new Bst<int>();
            leftAsRight.Insert(3);
            leftAsRight.Insert(1);
            leftAsRight.Insert(2);
            leftAsRight.Delete(1);
            Assert.IsFalse(leftAsRight.HasItem(1));
            Assert.IsTrue(leftAsRight.HasItem(2));

            // non-root right child with only right subtree
            var rightChild = new Bst<int>();
            rightChild.Insert(1);
            rightChild.Insert(2);
            rightChild.Insert(3);
            rightChild.Delete(2);
            Assert.IsFalse(rightChild.HasItem(2));
            Assert.IsTrue(rightChild.HasItem(3));
        }

        [TestMethod]
        public void BST_SortedSet_Oracle()
        {
            var rnd = new Random(42);
            var tree = new Bst<int>();
            var set = new SortedSet<int>();

            for (var t = 0; t < 500; t++)
            {
                var v = rnd.Next(0, 200);
                if (set.Contains(v))
                {
                    tree.Delete(v);
                    set.Remove(v);
                }
                else
                {
                    tree.Insert(v);
                    set.Add(v);
                }

                Assert.AreEqual(set.Count, tree.Count);
                CollectionAssert.AreEqual(set.ToList(), tree.ToList());
                if (tree.Root != null)
                {
                    Assert.IsTrue(tree.Root.IsBinarySearchTree(int.MinValue, int.MaxValue));
                    tree.Root.VerifyCount();
                }
            }
        }
    }
}