using System;
using System.Linq;
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

        [TestMethod]
        public void ArrayQueue_Empty_Dequeue_Throws()
        {
            var queue = new Queue<int>();
            Assert.ThrowsException<InvalidOperationException>(() => queue.Dequeue());
        }

        [TestMethod]
        public void LinkedListQueue_Empty_Dequeue_Throws()
        {
            var queue = new Queue<int>(QueueType.LinkedList);
            Assert.ThrowsException<InvalidOperationException>(() => queue.Dequeue());
        }

        [TestMethod]
        public void ArrayQueue_Enumerate_Fifo_Order()
        {
            var queue = new Queue<int>();
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);

            CollectionAssert.AreEqual(new[] { 3, 2, 1 }, queue.ToArray());
        }

        [TestMethod]
        public void LinkedListQueue_Enumerate_Fifo_Order()
        {
            var queue = new Queue<int>(QueueType.LinkedList);
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);

            CollectionAssert.AreEqual(new[] { 3, 2, 1 }, queue.ToArray());
        }

        [TestMethod]
        public void ArrayQueue_Single_Element()
        {
            var queue = new Queue<string>();
            queue.Enqueue("only");
            Assert.AreEqual(1, queue.Count);
            Assert.AreEqual("only", queue.Dequeue());
            Assert.AreEqual(0, queue.Count);
        }
    }
}
