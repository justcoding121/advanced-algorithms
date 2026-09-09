using System;
using System.Linq;
using Advanced.Algorithms.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.DataStructures
{
    [TestClass]
    public class CircularLinkedListTests
    {
        /// <summary>
        ///     doubly linked list tests
        /// </summary>
        [TestMethod]
        public void CircularLinkedList_Test()
        {
            var list = new CircularLinkedList<string>();

            list.Insert("a");
            list.Insert("b");
            list.Insert("c");
            list.Insert("c");

            Assert.AreEqual(list.Count(), 4);

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

        [TestMethod]
        public void CircularLinkedList_Empty_And_NotFound_Throws()
        {
            var list = new CircularLinkedList<string>();

            Assert.ThrowsException<InvalidOperationException>(() => list.Delete("x"));
            Assert.ThrowsException<InvalidOperationException>(() => list.Clear());

            list.Insert("a");
            Assert.ThrowsException<ArgumentException>(() => list.Delete("missing"));
            Assert.AreEqual(1, list.Count());
        }

        [TestMethod]
        public void CircularLinkedList_Union_Clear_Enumerate()
        {
            var list = new CircularLinkedList<string>();
            var node = list.Insert("a");
            list.Insert("b");
            list.Insert("c");

            var other = new CircularLinkedList<string>();
            other.Insert("x");
            other.Insert("y");
            list.Union(other);

            Assert.IsTrue(list.Count() >= 5);
            Assert.IsFalse(list.IsEmpty());

            list.Delete(node);
            Assert.IsTrue(list.Count() >= 4);

            using (var enumerator = list.GetEnumerator())
            {
                Assert.IsTrue(enumerator.MoveNext());
                enumerator.Reset();
                Assert.IsTrue(enumerator.MoveNext());
            }

            list.Clear();
            Assert.IsTrue(list.IsEmpty());
            Assert.AreEqual(0, list.Count());
        }
    }
}
