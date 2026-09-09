using System;
using System.Collections.Generic;
using System.Linq;
using Advanced.Algorithms.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.DataStructures
{
    [TestClass]
    public class DoublyLinkedListTests
    {
        /// <summary>
        ///     doubly linked list tests
        /// </summary>
        [TestMethod]
        public void DoublyLinkedList_Test()
        {
            var list = new DoublyLinkedList<string>();

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

            list.InsertFirst("a");
            list.InsertLast("a");
            list.InsertFirst("c");
            list.InsertLast("a");

            list.Delete("c");
            list.Delete("a");
            list.Delete("a");
            list.Delete("a");
            Assert.AreEqual(list.Count(), 0);
        }

        [TestMethod]
        public void DoublyLinkedList_Empty_And_Null_Node_Throws()
        {
            var list = new DoublyLinkedList<string>();

            Assert.ThrowsException<InvalidOperationException>(() => list.DeleteFirst());
            Assert.ThrowsException<InvalidOperationException>(() => list.DeleteLast());
            Assert.ThrowsException<InvalidOperationException>(() => list.Delete("x"));
            Assert.ThrowsException<InvalidOperationException>(() => list.Clear());
            Assert.ThrowsException<ArgumentException>(() =>
                list.InsertAfter(null, new DoublyLinkedListNode<string>("a")));
            Assert.ThrowsException<ArgumentException>(() =>
                list.InsertBefore(null, new DoublyLinkedListNode<string>("a")));
        }

        [TestMethod]
        public void DoublyLinkedList_InsertAfter_Before_Clear_Enumerate()
        {
            var list = new DoublyLinkedList<string>();
            var first = list.InsertFirst("b");
            list.InsertBefore(first, new DoublyLinkedListNode<string>("a"));
            list.InsertAfter(first, new DoublyLinkedListNode<string>("c"));
            list.InsertLast("d");

            CollectionAssert.AreEqual(new[] { "a", "b", "c", "d" }, list.ToList());

            list.Delete(first);
            Assert.AreEqual(3, list.Count());
            Assert.AreEqual("a", list.Head.Data);
            Assert.AreEqual("d", list.Tail.Data);

            using (var enumerator = list.GetEnumerator())
            {
                Assert.IsTrue(enumerator.MoveNext());
                Assert.AreEqual("a", enumerator.Current);
                enumerator.Reset();
                Assert.IsTrue(enumerator.MoveNext());
                Assert.AreEqual("a", enumerator.Current);
            }

            list.Clear();
            Assert.IsTrue(list.IsEmpty());
        }

        /// <summary>
        ///     Adversarial ops oracle vs List&lt;T&gt;.
        /// </summary>
        [TestMethod]
        public void DoublyLinkedList_ListOracle_RandomOps()
        {
            var rng = new Random(13);
            var list = new DoublyLinkedList<int>();
            var oracle = new List<int>();

            for (var step = 0; step < 500; step++)
            {
                var op = rng.Next(5);

                if (op == 0 || oracle.Count == 0)
                {
                    var v = rng.Next(100);
                    list.InsertFirst(v);
                    oracle.Insert(0, v);
                }
                else if (op == 1)
                {
                    var v = rng.Next(100);
                    list.InsertLast(v);
                    oracle.Add(v);
                }
                else if (op == 2)
                {
                    Assert.AreEqual(oracle[0], list.DeleteFirst());
                    oracle.RemoveAt(0);
                }
                else if (op == 3)
                {
                    Assert.AreEqual(oracle[oracle.Count - 1], list.DeleteLast());
                    oracle.RemoveAt(oracle.Count - 1);
                }
                else
                {
                    var v = oracle[rng.Next(oracle.Count)];
                    list.Delete(v);
                    oracle.Remove(v);
                }

                CollectionAssert.AreEqual(oracle, list.ToList());
                if (oracle.Count > 0)
                {
                    Assert.AreEqual(oracle[0], list.Head.Data);
                    Assert.AreEqual(oracle[oracle.Count - 1], list.Tail.Data);
                }
            }
        }
    }
}
