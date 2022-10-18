using System;
using System.Collections.Generic;
using System.Linq;
using Advanced.Algorithms.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.DataStructures
{
    [TestClass]
    public class DAryHeapTests
    {
        [TestMethod]
        public void Min_D_ary_Heap_Test()
        {
            var rnd = new Random();
            var initial = Enumerable.Range(0, 51).OrderBy(x => rnd.Next()).ToList();


            var minHeap = new DaryHeap<int>(4, SortDirection.Ascending, initial);

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
        public void Max_D_ary_Heap_Test()
        {
            var rnd = new Random();

            var initial = new List<int>(Enumerable.Range(0, 51)
                .OrderBy(x => rnd.Next()));

            var maxHeap = new DaryHeap<int>(4, SortDirection.Descending, initial);
            for (var i = 51; i <= 99; i++) maxHeap.Insert(i);

            for (var i = 99; i >= 0; i--)
            {
                var max = maxHeap.Extract();
                Assert.AreEqual(max, i);
            }

            //IEnumerable tests.
            Assert.AreEqual(maxHeap.Count, maxHeap.Count());

            var testSeries = Enumerable.Range(1, 49).OrderBy(x => rnd.Next()).ToList();

            foreach (var item in testSeries) maxHeap.Insert(item);

            for (var i = 49; i > 0; i--)
            {
                var max = maxHeap.Extract();
                Assert.AreEqual(i, max);
            }

            //IEnumerable tests.
            Assert.AreEqual(maxHeap.Count, maxHeap.Count());
        }
    }
}