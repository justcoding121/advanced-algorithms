using Advanced.Algorithms.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.DataStructures
{
    [TestClass]
    public class MinPriorityQueue_Tests
    {
        /// <summary>
        /// A dynamic array test
        /// </summary>
        [TestMethod]
        public void PriorityQueue_Test()
        {
            var queue = new MinPriorityQueue<int>();

            queue.Enqueue(10);
            queue.Enqueue(9);
            queue.Enqueue(1);
            queue.Enqueue(21);

            Assert.AreEqual(queue.Dequeue(), 1);
            Assert.AreEqual(queue.Dequeue(), 9);
            Assert.AreEqual(queue.Dequeue(), 10);
            Assert.AreEqual(queue.Dequeue(), 21);

        }
    }
}
