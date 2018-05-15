using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using Advanced.Algorithms.DataStructures;

namespace Advanced.Algorithms.Tests.DataStructures
{
    [TestClass]
    public class BPTree_Tests
    {
        /// </summary>
        [TestMethod]
        public void BPTree_Smoke_Test()
        {
            //insert test
            var tree = new BpTree<int>(3);

            tree.Insert(5);
            tree.Insert(3);
            tree.Insert(21);
            tree.Insert(9);
            tree.Insert(1);
            tree.Insert(5);
            tree.Insert(13);
            tree.Insert(2);
            tree.Insert(7);
            tree.Insert(10);
            tree.Insert(12);
            tree.Insert(4);
            tree.Insert(8);


            //////delete
            tree.Delete(2);
            Assert.IsFalse(tree.HasItem(2));

            tree.Delete(21);
            Assert.IsFalse(tree.HasItem(21));

            tree.Delete(10);
            Assert.IsFalse(tree.HasItem(10));

            tree.Delete(3);
            Assert.IsFalse(tree.HasItem(3));

            tree.Delete(4);
            Assert.IsFalse(tree.HasItem(4));

            tree.Delete(7);
            Assert.IsFalse(tree.HasItem(7));

            tree.Delete(9);
            Assert.IsFalse(tree.HasItem(9));

            tree.Delete(1);
            Assert.IsFalse(tree.HasItem(1));

            tree.Delete(5);
            Assert.IsTrue(tree.HasItem(5));

            tree.Delete(5);
            Assert.IsFalse(tree.HasItem(5));

            tree.Delete(8);
            Assert.IsFalse(tree.HasItem(8));

            tree.Delete(13);
            Assert.IsFalse(tree.HasItem(13));

            tree.Delete(12);
            Assert.IsFalse(tree.HasItem(12));

            Assert.AreEqual(tree.Count, 0);


        }

        [TestMethod]
        public void BPTree_AccuracyTest()
        {

            var nodeCount = 1000;

            var rnd = new Random();
            var randomNumbers = Enumerable.Range(1, nodeCount)
                            .OrderBy(x => rnd.Next())
                            .ToList();

            var order = 5;
            var tree = new BpTree<int>(order);

            for (int i = 0; i < nodeCount; i++)
            {

                tree.Insert(randomNumbers[i]);

                var actualMaxHeight = BTreeTester.GetMaxHeight(tree.Root);
                var actualMinHeight = BTreeTester.GetMinHeight(tree.Root);

                Assert.IsTrue(actualMaxHeight == actualMinHeight);

                //https://en.wikipedia.org/wiki/B-tree#Best_case_and_worst_case_heights
                var theoreticalMaxHeight = Math.Ceiling(Math.Log((i + 2) / 2, (int)Math.Ceiling((double)order / 2))) + 1;

                Assert.IsTrue(actualMaxHeight <= theoreticalMaxHeight);
                Assert.IsTrue(tree.Count == i + 1);

                Assert.IsTrue(tree.HasItem(randomNumbers[i]));
            }

            for (int i = 0; i < nodeCount; i++)
            {
                Assert.IsTrue(tree.HasItem(randomNumbers[i]));
            }

            Assert.AreEqual(tree.Max, randomNumbers.Max());
            Assert.AreEqual(tree.Min, randomNumbers.Min());

            //check that the elements are in sorted order
            //since B+ tree stores all elements in leaves in sorted order from left to right
            int j = 1;
            foreach(var element in tree)
            {
                Assert.AreEqual(j, element);
                j++;
            }

            //shuffle again before deletion tests
            randomNumbers = Enumerable.Range(1, nodeCount)
                            .OrderBy(x => rnd.Next())
                            .ToList();

            for (int i = 0; i < nodeCount; i++)
            {

                tree.Delete(randomNumbers[i]);
                Assert.IsFalse(tree.HasItem(randomNumbers[i]));

                var actualMaxHeight = BTreeTester.GetMaxHeight(tree.Root);
                var actualMinHeight = BTreeTester.GetMinHeight(tree.Root);

                Assert.IsTrue(actualMaxHeight == actualMinHeight);

                //https://en.wikipedia.org/wiki/B-tree#Best_case_and_worst_case_heights
                var theoreticalMaxHeight = i == nodeCount - 1 ? 0 : Math.Ceiling(Math.Log((nodeCount - i - 1 + 1) / 2, (int)Math.Ceiling((double)order / 2))) + 1;

                Assert.IsTrue(actualMaxHeight <= theoreticalMaxHeight);
                Assert.IsTrue(tree.Count == nodeCount - 1 - i);
            }

            Assert.IsTrue(BTreeTester.GetMaxHeight(tree.Root) == 0);
            Assert.IsTrue(tree.Count == 0);

        }

        [TestMethod]
        public void BPTree_StressTest()
        {
            var nodeCount = 1000 * 10;

            var rnd = new Random();
            var randomNumbers = Enumerable.Range(1, nodeCount)
                                .OrderBy(x => rnd.Next())
                                .ToList();

            var tree = new BpTree<int>(12);

            for (int i = 0; i < nodeCount; i++)
            {
                tree.Insert(randomNumbers[i]);
                Assert.IsTrue(tree.Count == i + 1);
            }

            ////shuffle again before deletion tests
            randomNumbers = Enumerable.Range(1, nodeCount)
                               .OrderBy(x => rnd.Next())
                               .ToList();

            //check that the elements are in sorted order
            //since B+ tree stores all elements in leaves in sorted order from left to right
            int j = 1;
            foreach (var element in tree)
            {
                Assert.AreEqual(j, element);
                j++;
            }

            for (int i = 0; i < nodeCount; i++)
            {
                tree.Delete(randomNumbers[i]);
                Assert.IsTrue(tree.Count == nodeCount - 1 - i);
            }

            Assert.IsTrue(tree.Count == 0);

        }

        [TestMethod]
        public void BPTree_Empty_Enumerator_Test()
        {
            var tree = new BpTree<int>(10);
            Assert.IsFalse(tree.GetEnumerator().MoveNext());
        }
    }
}
