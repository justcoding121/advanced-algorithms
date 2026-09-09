using System;
using System.Collections.Generic;
using System.Linq;
using Advanced.Algorithms.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.DataStructures
{
    [TestClass]
    public class PriorityQueueTests
    {
        [TestMethod]
        public void Min_PriorityQueue_Test()
        {
            var queue = new PriorityQueue<int>();

            queue.Enqueue(10);
            queue.Enqueue(9);
            queue.Enqueue(1);
            queue.Enqueue(21);

            Assert.AreEqual(queue.Dequeue(), 1);
            Assert.AreEqual(queue.Dequeue(), 9);
            Assert.AreEqual(queue.Dequeue(), 10);
            Assert.AreEqual(queue.Dequeue(), 21);
        }

        [TestMethod]
        public void Max_PriorityQueue_Test()
        {
            var queue = new PriorityQueue<int>(SortDirection.Descending);

            queue.Enqueue(10);
            queue.Enqueue(9);
            queue.Enqueue(1);
            queue.Enqueue(21);

            Assert.AreEqual(queue.Dequeue(), 21);
            Assert.AreEqual(queue.Dequeue(), 10);
            Assert.AreEqual(queue.Dequeue(), 9);
            Assert.AreEqual(queue.Dequeue(), 1);
        }

        [TestMethod]
        public void Min_PriorityQueue_Enumerate()
        {
            var queue = new PriorityQueue<int>();
            queue.Enqueue(3);
            queue.Enqueue(1);
            queue.Enqueue(2);

            var items = queue.ToArray();
            Assert.AreEqual(3, items.Length);
            CollectionAssert.Contains(items, 1);
            CollectionAssert.Contains(items, 2);
            CollectionAssert.Contains(items, 3);
        }

        [TestMethod]
        public void Min_PriorityQueue_Peek()
        {
            var queue = new PriorityQueue<int>();
            queue.Enqueue(5);
            queue.Enqueue(2);
            Assert.AreEqual(2, queue.Peek());
            Assert.AreEqual(2, queue.Dequeue());
        }

        /// <summary>
        ///     Extract order oracle vs sorted List (min and max heaps).
        /// </summary>
        [TestMethod]
        public void PriorityQueue_SortedListOracle_RandomOps()
        {
            foreach (var descending in new[] { false, true })
            {
                var rng = new Random(31 + (descending ? 1 : 0));
                var ours = new PriorityQueue<int>(descending
                    ? SortDirection.Descending
                    : SortDirection.Ascending);
                var oracle = new List<int>();

                for (var step = 0; step < 400; step++)
                {
                    if (oracle.Count == 0 || rng.Next(2) == 0)
                    {
                        var v = rng.Next(200);
                        ours.Enqueue(v);
                        oracle.Add(v);
                    }
                    else
                    {
                        oracle.Sort();
                        if (descending) oracle.Reverse();
                        Assert.AreEqual(oracle[0], ours.Peek());
                        Assert.AreEqual(oracle[0], ours.Dequeue());
                        oracle.RemoveAt(0);
                    }
                }

                // drain
                oracle.Sort();
                if (descending) oracle.Reverse();
                foreach (var expected in oracle)
                    Assert.AreEqual(expected, ours.Dequeue());
            }
        }
    }
}
