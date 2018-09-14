using Advanced.Algorithms.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.Tests.DataStructures
{
    [TestClass]
    public class PairingMaxHeap_Tests
    {
        /// <summary>
        /// A tree test
        /// </summary>
        [TestMethod]
        public void PairingMaxHeap_Test()
        {
            int nodeCount = 1000 * 10;
            //insert test
            var tree = new PairingMaxHeap<int>();

            for (int i = 0; i <= nodeCount; i++)
            {
                tree.Insert(i);
            }

            for (int i = 0; i <= nodeCount; i++)
            {
                tree.IncrementKey(i, i + 1);
            }

            //IEnumerable tests.
            Assert.AreEqual(tree.Count, tree.Count());

            int max = 0;
            for (int i = nodeCount; i >= 0; i--)
            {
                max = tree.ExtractMax();
                Assert.AreEqual(max, i + 1);
            }

            var rnd = new Random();
            var testSeries = Enumerable.Range(0, nodeCount - 1).OrderBy(x => rnd.Next()).ToList();

            foreach (var item in testSeries)
            {
                tree.Insert(item);
            }

            for (int i = 0; i < testSeries.Count; i++)
            {
                var incremented = testSeries[i] + rnd.Next(0, 1000);
                tree.IncrementKey(testSeries[i], incremented);
                testSeries[i] = incremented;
            }

            testSeries = testSeries.OrderByDescending(x => x).ToList();

            for (int i = 0; i < nodeCount - 2; i++)
            {
                max = tree.ExtractMax();
                Assert.AreEqual(testSeries[i], max);
            }

            //IEnumerable tests.
            Assert.AreEqual(tree.Count, tree.Count());

        }
    }
}
