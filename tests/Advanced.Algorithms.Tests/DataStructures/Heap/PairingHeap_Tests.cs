using Advanced.Algorithms.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Advanced.Algorithms.Tests.DataStructures
{
    [TestClass]
    public class PairingHeap_Tests
    {

        [TestMethod]
        public void Min_PairingHeap_Test()
        {
            int nodeCount = 1000 * 10;

            var minHeap = new PairingHeap<int>();

            for (int i = 0; i <= nodeCount; i++)
            {
                minHeap.Insert(i);
            }

            for (int i = 0; i <= nodeCount; i++)
            {
                minHeap.UpdateKey(i, i - 1);
            }

            int min = 0;
            for (int i = 0; i <= nodeCount; i++)
            {
                min = minHeap.Extract();
                Assert.AreEqual(min, i - 1);
            }

            //IEnumerable tests.
            Assert.AreEqual(minHeap.Count, minHeap.Count());

            var rnd = new Random();
            var testSeries = Enumerable.Range(0, nodeCount - 1).OrderBy(x => rnd.Next()).ToList();

            foreach (var item in testSeries)
            {
                minHeap.Insert(item);
            }

            for (int i = 0; i < testSeries.Count; i++)
            {
                var decremented = testSeries[i] - rnd.Next(0, 1000);
                minHeap.UpdateKey(testSeries[i], decremented);
                testSeries[i] = decremented;
            }

            testSeries.Sort();

            for (int i = 0; i < nodeCount - 2; i++)
            {
                min = minHeap.Extract();
                Assert.AreEqual(testSeries[i], min);
            }

            //IEnumerable tests.
            Assert.AreEqual(minHeap.Count, minHeap.Count());
        }

        [TestMethod]
        public void Max_PairingHeap_Test()
        {
            int nodeCount = 1000 * 10;

            var maxHeap = new PairingHeap<int>(SortDirection.Descending);

            for (int i = 0; i <= nodeCount; i++)
            {
                maxHeap.Insert(i);
            }

            for (int i = 0; i <= nodeCount; i++)
            {
                maxHeap.UpdateKey(i, i + 1);
            }

            //IEnumerable tests.
            Assert.AreEqual(maxHeap.Count, maxHeap.Count());

            int max = 0;
            for (int i = nodeCount; i >= 0; i--)
            {
                max = maxHeap.Extract();
                Assert.AreEqual(max, i + 1);
            }

            var rnd = new Random();
            var testSeries = Enumerable.Range(0, nodeCount - 1).OrderBy(x => rnd.Next()).ToList();

            foreach (var item in testSeries)
            {
                maxHeap.Insert(item);
            }

            for (int i = 0; i < testSeries.Count; i++)
            {
                var incremented = testSeries[i] + rnd.Next(0, 1000);
                maxHeap.UpdateKey(testSeries[i], incremented);
                testSeries[i] = incremented;
            }

            testSeries = testSeries.OrderByDescending(x => x).ToList();

            for (int i = 0; i < nodeCount - 2; i++)
            {
                max = maxHeap.Extract();
                Assert.AreEqual(testSeries[i], max);
            }

            //IEnumerable tests.
            Assert.AreEqual(maxHeap.Count, maxHeap.Count());

        }
    }
}
