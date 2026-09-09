using System;
using System.Linq;
using Advanced.Algorithms.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.DataStructures
{
    [TestClass]
    public class SinglyLinkedListTests
    {
        /// <summary>
        ///     singly linked list tests
        /// </summary>
        [TestMethod]
        public void SinglyLinkedList_Test()
        {
            var list = new SinglyLinkedList<string>();

            list.InsertFirst("a");
            list.InsertLast("b");
            list.InsertFirst("c");
            list.InsertLast("d");

            //{c,a,b,c}
            Assert.AreEqual(list.Count(), 4);
            Assert.AreEqual(list.Head.Data, "c");

            list.Delete("c");

            //{a,b,c}
            Assert.AreEqual(list.Count(), 3);
            Assert.AreEqual(list.Head.Data, "a");

            //{b}
            list.DeleteFirst();
            list.DeleteLast();

            Assert.AreEqual(list.Count(), 1);
            Assert.AreEqual(list.Head.Data, "b");

            list.Delete("b");
            Assert.AreEqual(list.Count(), 0);
        }

        [TestMethod]
        public void SinglyLinkedList_DeleteLast_Single_And_Empty_Throws()
        {
            var list = new SinglyLinkedList<string>();
            list.InsertFirst("only");

            Assert.AreEqual("only", list.DeleteLast());
            Assert.AreEqual(0, list.Count());
            Assert.IsTrue(list.IsEmpty());

            Assert.ThrowsException<InvalidOperationException>(() => list.DeleteLast());
            Assert.ThrowsException<InvalidOperationException>(() => list.DeleteFirst());
            Assert.ThrowsException<InvalidOperationException>(() => list.Delete("x"));
            Assert.ThrowsException<InvalidOperationException>(() => list.Clear());
        }

        [TestMethod]
        public void SinglyLinkedList_InsertLast_Clear_Enumerate()
        {
            var list = new SinglyLinkedList<string>();
            list.InsertLast("only");
            Assert.AreEqual("only", list.Head.Data);

            list.InsertFirst(new SinglyLinkedListNode<string>("first"));
            Assert.AreEqual("first", list.Head.Data);
            Assert.AreEqual(2, list.Count());

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
