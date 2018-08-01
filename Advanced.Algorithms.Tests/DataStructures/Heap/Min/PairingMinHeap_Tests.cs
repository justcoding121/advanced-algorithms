using Advanced.Algorithms.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.Tests.DataStructures
{
    [TestClass]
    public class PairingMinHeap_Tests
    {
        /// <summary>
        /// A tree test
        /// </summary>
        [TestMethod]
        public void PairingMinHeap_Test()
        {
            int nodeCount = 1000 * 10;
            //insert test
            var tree = new PairingMinHeap<int>();

            for (int i = 0; i <= nodeCount; i++)
            {
                tree.Insert(i);
            }

            for (int i = 0; i <= nodeCount; i++)
            {
                tree.DecrementKey(i, i - 1);
            }

            int min = 0;
            for (int i = 0; i <= nodeCount; i++)
            {
                min = tree.ExtractMin();
                Assert.AreEqual(min, i - 1);
            }

            //IEnumerable tests.
            Assert.AreEqual(tree.Count, tree.Count());

            var rnd = new Random();
            var testSeries = Enumerable.Range(0, nodeCount - 1).OrderBy(x => rnd.Next()).ToList();

            foreach (var item in testSeries)
            {
                tree.Insert(item);
            }

            for (int i = 0; i < testSeries.Count; i++)
            {
                var decremented = testSeries[i] - rnd.Next(0, 1000);
                tree.DecrementKey(testSeries[i], decremented);
                testSeries[i] = decremented;
            }

            testSeries.Sort();

            for (int i = 0; i < nodeCount - 2; i++)
            {
                min = tree.ExtractMin();
                Assert.AreEqual(testSeries[i], min);
            }

            //IEnumerable tests.
            Assert.AreEqual(tree.Count, tree.Count());
        }
    }
}
