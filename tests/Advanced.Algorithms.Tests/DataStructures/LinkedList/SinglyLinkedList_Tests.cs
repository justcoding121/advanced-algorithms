using System;
using System.Collections.Generic;
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
                Assert.AreEqual("first", enumerator.Current);
                enumerator.Reset();
                Assert.IsTrue(enumerator.MoveNext());
                Assert.AreEqual("first", enumerator.Current);
            }

            list.Clear();
            Assert.IsTrue(list.IsEmpty());
            Assert.AreEqual(0, list.Count());
        }

        /// <summary>
        ///     Adversarial ops oracle vs List&lt;T&gt; (head/tail insert-delete).
        /// </summary>
        [TestMethod]
        public void SinglyLinkedList_ListOracle_RandomOps()
        {
            var rng = new Random(11);
            var list = new SinglyLinkedList<int>();
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
            }
        }
    }
}
