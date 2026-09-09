using System;
using System.Linq;
using Advanced.Algorithms.Distributed;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests
{
    [TestClass]
    public class CircularQueueTests
    {
        [TestMethod]
        public void CircularQueue_Test()
        {
            var queue = new CircularQueue<int>(7);

            Assert.AreEqual(0, queue.Enqueue(1));
            Assert.AreEqual(0, queue.Enqueue(2));

            Assert.AreEqual(0, queue.Enqueue(3));
            Assert.AreEqual(0, queue.Enqueue(4));
            Assert.AreEqual(0, queue.Enqueue(5));
            Assert.AreEqual(0, queue.Enqueue(6));
            Assert.AreEqual(0, queue.Enqueue(7));
            Assert.AreEqual(1, queue.Enqueue(8));
            Assert.AreEqual(2, queue.Enqueue(9));

            Assert.AreEqual(queue.Count, 7);
            Assert.AreEqual(3, queue.Dequeue());

            Assert.AreEqual(queue.Count, 6);
            Assert.AreEqual(queue.Dequeue(), 4);

            Assert.AreEqual(queue.Count, 5);
            Assert.AreEqual(queue.Dequeue(), 5);

            Assert.AreEqual(queue.Count, 4);
            Assert.AreEqual(queue.Dequeue(), 6);

            Assert.AreEqual(queue.Count, 3);
            Assert.AreEqual(queue.Dequeue(), 7);

            Assert.AreEqual(queue.Count, 2);
            Assert.AreEqual(queue.Dequeue(), 8);

            Assert.AreEqual(queue.Count, 1);
            Assert.AreEqual(queue.Dequeue(), 9);

            Assert.AreEqual(queue.Count, 0);

            Assert.AreEqual(0, queue.Enqueue(1));
            Assert.AreEqual(0, queue.Enqueue(2));

            Assert.AreEqual(queue.Count, 2);
            Assert.AreEqual(1, queue.Dequeue());

            Assert.AreEqual(queue.Count, 1);
            Assert.AreEqual(queue.Dequeue(), 2);
        }

        [TestMethod]
        public void CircularQueue_Empty_Dequeue_Throws()
        {
            var queue = new CircularQueue<int>(3);

            Assert.ThrowsException<InvalidOperationException>(() => queue.Dequeue());
        }

        [TestMethod]
        public void CircularQueue_Bulk_Enqueue_Dequeue()
        {
            var queue = new CircularQueue<int>(3);

            var overwritten = queue.Enqueue(new[] { 1, 2, 3, 4 }).ToList();
            Assert.AreEqual(1, overwritten.Count);
            Assert.AreEqual(1, overwritten[0]);
            Assert.AreEqual(3, queue.Count);

            var deleted = queue.Dequeue(2).ToList();
            CollectionAssert.AreEqual(new[] { 2, 3 }, deleted);
            Assert.AreEqual(1, queue.Count);
            Assert.AreEqual(4, queue.Dequeue());
        }

        [TestMethod]
        public void CircularQueue_Adversarial_Wrap_Full_Empty()
        {
            var queue = new CircularQueue<int>(3);

            for (var i = 1; i <= 10; i++) queue.Enqueue(i);

            Assert.AreEqual(3, queue.Count);
            Assert.AreEqual(8, queue.Dequeue());
            Assert.AreEqual(9, queue.Dequeue());
            Assert.AreEqual(10, queue.Dequeue());
            Assert.AreEqual(0, queue.Count);
            Assert.ThrowsException<InvalidOperationException>(() => queue.Dequeue());

            var sizeOne = new CircularQueue<int>(1);
            Assert.AreEqual(0, sizeOne.Enqueue(1));
            Assert.AreEqual(1, sizeOne.Enqueue(2));
            Assert.AreEqual(1, sizeOne.Count);
            Assert.AreEqual(2, sizeOne.Dequeue());
            Assert.AreEqual(0, sizeOne.Count);

            //overwrite of default(T)=0 must be reported by bulk enqueue
            var withZero = new CircularQueue<int>(2);
            var overwrittenZeros = withZero.Enqueue(new[] { 0, 1, 2 }).ToList();
            CollectionAssert.AreEqual(new[] { 0 }, overwrittenZeros);
            CollectionAssert.AreEqual(new[] { 1, 2 }, withZero.Dequeue(2).ToList());
        }
    }
}