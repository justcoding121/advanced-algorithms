using System;
using System.Collections.Generic;
using System.Linq;
using Advanced.Algorithms.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.DataStructures
{
    [TestClass]
    public class BHeapTests
    {
        [TestMethod]
        public void Min_BHeap_Test()
        {
            var rnd = new Random();
            var initial = Enumerable.Range(0, 51).OrderBy(x => rnd.Next()).ToList();

            var minHeap = new BHeap<int>(SortDirection.Ascending, initial);

            for (var i = 51; i <= 99; i++) minHeap.Insert(i);

            for (var i = 0; i <= 99; i++)
            {
                var min = minHeap.Extract();
                Assert.AreEqual(min, i);
            }

            //IEnumerable tests.
            Assert.AreEqual(minHeap.Count, minHeap.Count());

            var testSeries = Enumerable.Range(1, 49).OrderBy(x => rnd.Next()).ToList();

            foreach (var item in testSeries) minHeap.Insert(item);

            for (var i = 1; i <= 49; i++)
            {
                var min = minHeap.Extract();
                Assert.AreEqual(min, i);
            }

            //IEnumerable tests.
            Assert.AreEqual(minHeap.Count, minHeap.Count());
        }


        [TestMethod]
        public void Max_BHeap_Test()
        {
            var rnd = new Random();

            var initial = new List<int>(Enumerable.Range(0, 51)
                .OrderBy(x => rnd.Next()));


            var maxHeap = new BHeap<int>(SortDirection.Descending, initial);

            for (var i = 51; i <= 99; i++) maxHeap.Insert(i);

            for (var i = 0; i <= 99; i++)
            {
                var max = maxHeap.Extract();
                Assert.AreEqual(max, 99 - i);
            }

            //IEnumerable tests.
            Assert.AreEqual(maxHeap.Count, maxHeap.Count());

            var testSeries = Enumerable.Range(1, 49)
                .OrderBy(x => rnd.Next()).ToList();

            foreach (var item in testSeries) maxHeap.Insert(item);

            for (var i = 1; i <= 49; i++)
            {
                var max = maxHeap.Extract();
                Assert.AreEqual(max, 49 - i + 1);
            }

            //IEnumerable tests.
            Assert.AreEqual(maxHeap.Count, maxHeap.Count());
        }

        [TestMethod]
        public void BHeap_Empty_Delete_Exists_Peek()
        {
            var heap = new BHeap<int>();

            Assert.AreEqual(0, heap.Count);
            Assert.AreEqual(0, heap.Count());
            Assert.IsFalse(heap.Exists(1));
            Assert.ThrowsException<InvalidOperationException>(() => heap.Extract());
            Assert.ThrowsException<InvalidOperationException>(() => heap.Peek());
            Assert.ThrowsException<ArgumentException>(() => heap.Delete(1));

            heap.Insert(5);
            heap.Insert(1);
            heap.Insert(3);
            Assert.IsTrue(heap.Exists(3));
            Assert.IsFalse(heap.Exists(9));
            Assert.AreEqual(1, heap.Peek());

            heap.Delete(3);
            Assert.IsFalse(heap.Exists(3));
            Assert.AreEqual(1, heap.Extract());
            Assert.AreEqual(5, heap.Extract());
            Assert.AreEqual(0, heap.Count);

            var maxHeap = new BHeap<int>(SortDirection.Descending);
            maxHeap.Insert(1);
            maxHeap.Insert(4);
            maxHeap.Insert(2);
            Assert.AreEqual(4, maxHeap.Peek());
            maxHeap.Delete(4);
            Assert.AreEqual(2, maxHeap.Extract());
            Assert.AreEqual(1, maxHeap.Extract());
        }
    }
}