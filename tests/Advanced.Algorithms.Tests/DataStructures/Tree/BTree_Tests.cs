﻿using System;
using System.Linq;
using Advanced.Algorithms.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.DataStructures
{
    [TestClass]
    public class BTreeTests
    {
        /// </summary>
        [TestMethod]
        public void BTree_Smoke_Test()
        {
            //insert test
            var tree = new BTree<int>(3);

            tree.Insert(5);
            tree.Insert(5);
            tree.Insert(3);
            tree.Insert(21);
            tree.Insert(9);
            tree.Insert(1);
            tree.Insert(13);
            tree.Insert(2);
            tree.Insert(7);
            tree.Insert(10);
            tree.Insert(12);
            tree.Insert(4);
            tree.Insert(8);

            //IEnumerable test using linq count()
            Assert.AreEqual(tree.Count, tree.Count());

            ////delete
            tree.Delete(2);
            tree.Delete(5);
            tree.Delete(21);
            tree.Delete(10);
            tree.Delete(3);
            tree.Delete(4);
            tree.Delete(7);
            tree.Delete(9);
            tree.Delete(1);
            tree.Delete(5);
            tree.Delete(8);
            tree.Delete(13);
            tree.Delete(12);

            Assert.AreEqual(tree.Count, 0);
            Assert.AreEqual(tree.Count, tree.Count());
        }

        [TestMethod]
        public void BTree_AccuracyTest()
        {
            var nodeCount = 1000;

            var rnd = new Random();
            var randomNumbers = Enumerable.Range(1, nodeCount)
                .OrderBy(x => rnd.Next())
                .ToList();

            var order = 5;
            var tree = new BTree<int>(order);

            for (var i = 0; i < nodeCount; i++)
            {
                tree.Insert(randomNumbers[i]);

                var actualMaxHeight = BTreeTester.GetMaxHeight(tree.Root);
                var actualMinHeight = BTreeTester.GetMinHeight(tree.Root);

                Assert.IsTrue(actualMaxHeight == actualMinHeight);

                //https://en.wikipedia.org/wiki/B-tree#Best_case_and_worst_case_heights
                var theoreticalMaxHeight = Math.Ceiling(Math.Log((i + 2) / 2, (int)Math.Ceiling((double)order / 2)));

                Assert.IsTrue(actualMaxHeight <= theoreticalMaxHeight);
                Assert.IsTrue(tree.Count == i + 1);

                Assert.IsTrue(tree.HasItem(randomNumbers[i]));
                //IEnumerable test using linq count()
                Assert.AreEqual(tree.Count, tree.Count());
            }

            for (var i = 0; i < nodeCount; i++) Assert.IsTrue(tree.HasItem(randomNumbers[i]));

            //IEnumerable test using linq count()
            Assert.AreEqual(tree.Count, tree.Count());

            Assert.AreEqual(tree.Max, randomNumbers.Max());
            Assert.AreEqual(tree.Min, randomNumbers.Min());

            //shuffle again before deletion tests
            randomNumbers = Enumerable.Range(1, nodeCount)
                .OrderBy(x => rnd.Next())
                .ToList();

            for (var i = 0; i < nodeCount; i++)
            {
                tree.Delete(randomNumbers[i]);
                Assert.IsFalse(tree.HasItem(randomNumbers[i]));

                var actualMaxHeight = BTreeTester.GetMaxHeight(tree.Root);
                var actualMinHeight = BTreeTester.GetMinHeight(tree.Root);

                Assert.IsTrue(actualMaxHeight == actualMinHeight);

                //https://en.wikipedia.org/wiki/B-tree#Best_case_and_worst_case_heights
                var theoreticalMaxHeight =
                    Math.Ceiling(Math.Log((nodeCount - i + 2) / 2, (int)Math.Ceiling((double)order / 2)));

                Assert.IsTrue(actualMaxHeight <= theoreticalMaxHeight);
                Assert.IsTrue(tree.Count == nodeCount - 1 - i);
                //IEnumerable test using linq count()
                Assert.AreEqual(tree.Count, tree.Count());
            }

            Assert.IsTrue(tree.Count == 0);
            //IEnumerable test using linq count()
            Assert.AreEqual(tree.Count, tree.Count());
        }


        [TestMethod]
        public void BTree_StressTest()
        {
            var nodeCount = 1000 * 10;

            var rnd = new Random();
            var randomNumbers = Enumerable.Range(1, nodeCount)
                .OrderBy(x => rnd.Next())
                .ToList();

            var tree = new BTree<int>(12);

            for (var i = 0; i < nodeCount; i++)
            {
                tree.Insert(randomNumbers[i]);
                Assert.IsTrue(tree.Count == i + 1);
            }

            //shuffle again before deletion tests
            randomNumbers = Enumerable.Range(1, nodeCount)
                .OrderBy(x => rnd.Next())
                .ToList();


            for (var i = 0; i < nodeCount; i++)
            {
                tree.Delete(randomNumbers[i]);
                Assert.IsTrue(tree.Count == nodeCount - 1 - i);
            }


            Assert.IsTrue(tree.Count == 0);
        }
    }
}