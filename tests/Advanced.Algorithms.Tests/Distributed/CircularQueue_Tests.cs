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
    }
}