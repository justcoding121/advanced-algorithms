using Algorithm.Sandbox.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Algorithm.Sandbox.Tests.DataStructures
{
    [TestClass]
    public class PriorityQueue_Tests
    {
        /// <summary>
        /// A dynamic array test
        /// </summary>
        [TestMethod]
        public void PriorityQueue_Test()
        {
            var Queue = new AsPriorityQueue<int>();

            Queue.Enqueue(10);
            Queue.Enqueue(9);
            Queue.Enqueue(1);
            Queue.Enqueue(21);

            Assert.AreEqual(Queue.Dequeue(), 1);
            Assert.AreEqual(Queue.Dequeue(), 9);
            Assert.AreEqual(Queue.Dequeue(), 10);
            Assert.AreEqual(Queue.Dequeue(), 21);

        }
    }
}
