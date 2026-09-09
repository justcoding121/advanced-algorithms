using System;
using System.Collections.Generic;
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

            Assert.AreEqual(5, list.Count());
            AssertIsValidCircle(list);
            Assert.IsFalse(list.IsEmpty());

            list.Delete(node);
            Assert.AreEqual(4, list.Count());
            AssertIsValidCircle(list);

            using (var enumerator = list.GetEnumerator())
            {
                Assert.IsTrue(enumerator.MoveNext());
                var first = enumerator.Current;
                enumerator.Reset();
                Assert.IsTrue(enumerator.MoveNext());
                Assert.AreEqual(first, enumerator.Current);
            }

            list.Clear();
            Assert.IsTrue(list.IsEmpty());
            Assert.AreEqual(0, list.Count());
        }

        /// <summary>
        ///     Union must splice two circles without breaking Prev/Next links.
        /// </summary>
        [TestMethod]
        public void CircularLinkedList_Union_Preserves_Circle()
        {
            var a = new CircularLinkedList<int>();
            a.Insert(1);
            a.Insert(2);
            var b = new CircularLinkedList<int>();
            b.Insert(3);
            b.Insert(4);

            a.Union(b);
            Assert.AreEqual(4, a.Count());
            AssertIsValidCircle(a);

            var items = a.ToList();
            CollectionAssert.AreEquivalent(new[] { 1, 2, 3, 4 }, items);
        }

        /// <summary>
        ///     Insert/delete keeps a valid circle; bag matches List&lt;T&gt; multiset.
        /// </summary>
        [TestMethod]
        public void CircularLinkedList_ListOracle_InsertDelete()
        {
            var rng = new Random(17);
            var list = new CircularLinkedList<int>();
            var oracle = new List<int>();

            for (var step = 0; step < 300; step++)
            {
                if (oracle.Count == 0 || rng.Next(2) == 0)
                {
                    var v = rng.Next(50);
                    list.Insert(v);
                    oracle.Add(v);
                }
                else
                {
                    var idx = rng.Next(oracle.Count);
                    var v = oracle[idx];
                    list.Delete(v);
                    oracle.RemoveAt(idx);
                }

                AssertIsValidCircle(list);
                CollectionAssert.AreEquivalent(oracle, list.ToList());
            }
        }

        private static void AssertIsValidCircle<T>(CircularLinkedList<T> list)
        {
            if (list.ReferenceNode == null)
            {
                Assert.AreEqual(0, list.Count());
                return;
            }

            var start = list.ReferenceNode;
            var node = start;
            var n = 0;
            do
            {
                Assert.IsNotNull(node.Next);
                Assert.IsNotNull(node.Previous);
                Assert.AreSame(node, node.Next.Previous);
                Assert.AreSame(node, node.Previous.Next);
                node = node.Next;
                n++;
                Assert.IsTrue(n <= 10000, "cycle walk did not return to reference");
            } while (node != start);

            Assert.AreEqual(n, list.Count());
        }
    }
}
