using Algorithm.Sandbox.DataStructures;
using Algorithm.Sandbox.DataStructures.Queues.PriorityQueue;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Algorithm.Sandbox.Tests.DataStructures.Queues.PriorityQueue
{
    [TestClass]
    public class MaxPriorityQueue_Tests
    {
        /// <summary>
        /// A dynamic array test
        /// </summary>
        [TestMethod]
        public void PriorityQueue_Test()
        {
            var queue = new MaxPriorityQueue<int>();

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
