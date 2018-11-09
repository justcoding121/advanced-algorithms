using Advanced.Algorithms.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.DataStructures
{
    [TestClass]
    public class PriorityQueue_Tests
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
    }
}
