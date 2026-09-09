using System;
using System.Collections.Generic;
using System.Linq;
using Advanced.Algorithms.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.DataStructures
{
    [TestClass]
    public class FibonacciHeapTests
    {
        [TestMethod]
        public void Min_FibonacciHeap_Test()
        {
            var nodeCount = 1000 * 10;

            var minHeap = new FibonacciHeap<int>();

            for (var i = 0; i <= nodeCount; i++) minHeap.Insert(i);

            for (var i = 0; i <= nodeCount; i++) minHeap.UpdateKey(i, i - 1);

            var min = 0;
            for (var i = 0; i <= nodeCount; i++)
            {
                min = minHeap.Extract();
                Assert.AreEqual(min, i - 1);
            }

            //IEnumerable tests.
            Assert.AreEqual(minHeap.Count, minHeap.Count());

            var rnd = new Random();
            var testSeries = Enumerable.Range(0, nodeCount - 1).OrderBy(x => rnd.Next()).ToList();

            foreach (var item in testSeries) minHeap.Insert(item);

            for (var i = 0; i < testSeries.Count; i++)
            {
                var decremented = testSeries[i] - rnd.Next(0, 1000);
                minHeap.UpdateKey(testSeries[i], decremented);
                testSeries[i] = decremented;
            }

            testSeries.Sort();

            for (var i = 0; i < nodeCount - 2; i++)
            {
                min = minHeap.Extract();
                Assert.AreEqual(testSeries[i], min);
            }

            //IEnumerable tests.
            Assert.AreEqual(minHeap.Count, minHeap.Count());
        }


        [TestMethod]
        public void Max_FibonacciHeap_Test()
        {
            var nodeCount = 1000 * 10;

            var maxHeap = new FibonacciHeap<int>(SortDirection.Descending);

            for (var i = 0; i <= nodeCount; i++) maxHeap.Insert(i);

            for (var i = 0; i <= nodeCount; i++) maxHeap.UpdateKey(i, i + 1);
            var max = 0;
            for (var i = nodeCount; i >= 0; i--)
            {
                max = maxHeap.Extract();
                Assert.AreEqual(max, i + 1);
            }

            //IEnumerable tests.
            Assert.AreEqual(maxHeap.Count, maxHeap.Count());

            var rnd = new Random();
            var testSeries = Enumerable.Range(0, nodeCount - 1).OrderBy(x => rnd.Next()).ToList();

            foreach (var item in testSeries) maxHeap.Insert(item);

            for (var i = 0; i < testSeries.Count; i++)
            {
                var incremented = testSeries[i] + rnd.Next(0, 1000);
                maxHeap.UpdateKey(testSeries[i], incremented);
                testSeries[i] = incremented;
            }

            testSeries = testSeries.OrderByDescending(x => x).ToList();

            for (var i = 0; i < nodeCount - 2; i++)
            {
                max = maxHeap.Extract();
                Assert.AreEqual(testSeries[i], max);
            }

            //IEnumerable tests.
            Assert.AreEqual(maxHeap.Count, maxHeap.Count());
        }

        [TestMethod]
        public void FibonacciHeap_Empty_And_Invalid_Update()
        {
            var heap = new FibonacciHeap<int>();

            Assert.AreEqual(0, heap.Count);
            Assert.AreEqual(0, heap.Count());
            Assert.ThrowsException<InvalidOperationException>(() => heap.Extract());
            Assert.ThrowsException<InvalidOperationException>(() => heap.Peek());
            Assert.ThrowsException<KeyNotFoundException>(() => heap.UpdateKey(1, 0));

            heap.Insert(5);
            heap.Insert(3);
            heap.Insert(7);
            Assert.AreEqual(3, heap.Peek());
            Assert.ThrowsException<ArgumentException>(() => heap.UpdateKey(5, 6));
            Assert.ThrowsException<KeyNotFoundException>(() => heap.UpdateKey(99, 1));

            heap.UpdateKey(5, 2);
            Assert.AreEqual(2, heap.Extract());
            Assert.AreEqual(3, heap.Extract());
            Assert.AreEqual(7, heap.Extract());
            Assert.AreEqual(0, heap.Count);

            var left = new FibonacciHeap<int>();
            left.Insert(2);
            var right = new FibonacciHeap<int>();
            right.Insert(4);
            left.Merge(right);
            Assert.AreEqual(2, left.Count);

            var maxHeap = new FibonacciHeap<int>(SortDirection.Descending);
            maxHeap.Insert(1);
            maxHeap.Insert(3);
            Assert.ThrowsException<ArgumentException>(() => maxHeap.UpdateKey(3, 2));
            maxHeap.UpdateKey(1, 5);
            Assert.AreEqual(5, maxHeap.Peek());
            Assert.AreEqual(5, maxHeap.Extract());
            Assert.AreEqual(3, maxHeap.Extract());
        }
    }
}