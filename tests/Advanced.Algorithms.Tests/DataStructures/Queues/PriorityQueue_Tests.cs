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
    }
}
