using Advanced.Algorithms.DataStructures;
using Advanced.Algorithms.DataStructures.Queues;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.DataStructures.Queues
{
    [TestClass]
    public class Queue_Tests
    {

        [TestMethod]
        public void ArrayQueue_Test()
        {
            var Queue = new AsQueue<string>();

            Queue.Enqueue("a");
            Queue.Enqueue("b");
            Queue.Enqueue("c");

            Assert.AreEqual(Queue.Count, 3);
            Assert.AreEqual(Queue.Dequeue(), "a");


            Assert.AreEqual(Queue.Count, 2);
            Assert.AreEqual(Queue.Dequeue(), "b");

            Assert.AreEqual(Queue.Count, 1);
            Assert.AreEqual(Queue.Dequeue(), "c");

            Assert.AreEqual(Queue.Count, 0);

            Queue.Enqueue("a");

            Assert.AreEqual(Queue.Count, 1);
            Assert.AreEqual(Queue.Dequeue(), "a");

        }

        [TestMethod]
        public void LinkedListQueue_Test()
        {
            var Queue = new AsQueue<string>(QueueType.LinkedList);

            Queue.Enqueue("a");
            Queue.Enqueue("b");
            Queue.Enqueue("c");

            Assert.AreEqual(Queue.Count, 3);
            Assert.AreEqual(Queue.Dequeue(), "a");


            Assert.AreEqual(Queue.Count, 2);
            Assert.AreEqual(Queue.Dequeue(), "b");

            Assert.AreEqual(Queue.Count, 1);
            Assert.AreEqual(Queue.Dequeue(), "c");

            Assert.AreEqual(Queue.Count, 0);

            Queue.Enqueue("a");

            Assert.AreEqual(Queue.Count, 1);
            Assert.AreEqual(Queue.Dequeue(), "a");

        }
    }
}
