using Algorithm.Sandbox.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Algorithm.Sandbox.Tests.DataStructures
{
    [TestClass]
    public class CircularLinkedList_Tests
    {
        /// <summary>
        /// doubly linked list tests 
        /// </summary>
        [TestMethod]
        public void CircularLinkedList_Test()
        {
            var list = new AsCircularLinkedList<string>();

            list.Add("a");
            list.Add("b");
            list.Add("c");

            Assert.AreEqual(list.Count(), 3);
            Assert.AreEqual(list.GetAllNodes().Length, 3);

            list.Remove("a");
            Assert.AreEqual(list.Count(), 2);

            list.Remove("b");
            Assert.AreEqual(list.Count(), 1);

            list.Remove("c");
            Assert.AreEqual(list.Count(), 0);

            list.Add("a");
            Assert.AreEqual(list.Count(), 1);
        }
    }
}
