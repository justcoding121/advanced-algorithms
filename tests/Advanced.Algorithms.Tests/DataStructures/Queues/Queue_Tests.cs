using Advanced.Algorithms.DataStructures.Foundation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.DataStructures
{
    [TestClass]
    public class QueueTests
    {
        [TestMethod]
        public void ArrayQueue_Test()
        {
            var queue = new Queue<string>();

            queue.Enqueue("a");
            queue.Enqueue("b");
            queue.Enqueue("c");

            Assert.AreEqual(queue.Count, 3);
            Assert.AreEqual(queue.Dequeue(), "a");


            Assert.AreEqual(queue.Count, 2);
            Assert.AreEqual(queue.Dequeue(), "b");

            Assert.AreEqual(queue.Count, 1);
            Assert.AreEqual(queue.Dequeue(), "c");

            Assert.AreEqual(queue.Count, 0);

            queue.Enqueue("a");

            Assert.AreEqual(queue.Count, 1);
            Assert.AreEqual(queue.Dequeue(), "a");
        }

        [TestMethod]
        public void LinkedListQueue_Test()
        {
            var queue = new Queue<string>(QueueType.LinkedList);

            queue.Enqueue("a");
            queue.Enqueue("b");
            queue.Enqueue("c");

            Assert.AreEqual(queue.Count, 3);
            Assert.AreEqual(queue.Dequeue(), "a");


            Assert.AreEqual(queue.Count, 2);
            Assert.AreEqual(queue.Dequeue(), "b");

            Assert.AreEqual(queue.Count, 1);
            Assert.AreEqual(queue.Dequeue(), "c");

            Assert.AreEqual(queue.Count, 0);

            queue.Enqueue("a");

            Assert.AreEqual(queue.Count, 1);
            Assert.AreEqual(queue.Dequeue(), "a");
        }
    }
}