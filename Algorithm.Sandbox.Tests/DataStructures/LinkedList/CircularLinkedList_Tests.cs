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

            list.Insert("a");
            list.Insert("b");
            list.Insert("c");
            list.Insert("c");

            Assert.AreEqual(list.Count(), 4);
            Assert.AreEqual(list.GetAllNodes().Count, 4);

            list.Delete("a");
            Assert.AreEqual(list.Count(), 3);

            list.Delete("b");
            Assert.AreEqual(list.Count(), 2);

            list.Delete("c");
            Assert.AreEqual(list.Count(), 1);

            list.Insert("a");
            Assert.AreEqual(list.Count(), 2);

            list.Delete("a");
            Assert.AreEqual(list.Count(), 1);

            list.Delete("c");
            Assert.AreEqual(list.Count(), 0);

            list.Insert("a");
            list.Insert("b");
            list.Insert("c");
            list.Insert("c");

            Assert.AreEqual(list.Count(), 4);
            Assert.AreEqual(list.GetAllNodes().Count, 4);

            list.Delete("a");
            Assert.AreEqual(list.Count(), 3);

            list.Delete("b");
            Assert.AreEqual(list.Count(), 2);

            list.Delete("c");
            Assert.AreEqual(list.Count(), 1);

            list.Insert("a");
            Assert.AreEqual(list.Count(), 2);

            list.Delete("a");
            Assert.AreEqual(list.Count(), 1);

            list.Delete("c");
            Assert.AreEqual(list.Count(), 0);
        }
    }
}
