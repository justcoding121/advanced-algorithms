using Algorithm.Sandbox.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Algorithm.Sandbox.Tests.DataStructures
{
    [TestClass]
    public class Queue_Tests
    {
        /// <summary>
        /// A dynamic array test
        /// </summary>
        [TestMethod]
        public void Queue_Test()
        {
            var Queue = new AsQueue<string>();

            Queue.Enqueue("a");
            Queue.Enqueue("b");
            Queue.Enqueue("c");

            Assert.AreEqual(Queue.Count(), 3);
            Assert.AreEqual(Queue.Dequeue(), "a");


            Assert.AreEqual(Queue.Count(), 2);
            Assert.AreEqual(Queue.Dequeue(), "b");

            Assert.AreEqual(Queue.Count(), 1);
            Assert.AreEqual(Queue.Dequeue(), "c");

            Assert.AreEqual(Queue.Count(), 0);

            Queue.Enqueue("a");

            Assert.AreEqual(Queue.Count(), 1);
            Assert.AreEqual(Queue.Dequeue(), "a");

        }
    }
}
