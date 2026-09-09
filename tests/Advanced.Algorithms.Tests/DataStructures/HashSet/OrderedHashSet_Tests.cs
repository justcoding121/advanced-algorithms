using System;
using System.Linq;
using Advanced.Algorithms.DataStructures.Foundation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.DataStructures
{
    [TestClass]
    public class OrderedHashSetTests
    {
        [TestMethod]
        public void OrderedHashSet_Test()
        {
            var hashSet = new OrderedHashSet<int>();

            var nodeCount = 1000;

            for (var i = 0; i <= nodeCount; i++)
            {
                hashSet.Add(i);
                Assert.AreEqual(true, hashSet.Contains(i));
            }

            Assert.AreEqual(hashSet.Count, hashSet.Count());
            Assert.AreEqual(hashSet.Count, hashSet.AsEnumerableDesc().Count());

            for (var i = 0; i <= nodeCount; i++)
            {
                hashSet.Remove(i);
                Assert.AreEqual(false, hashSet.Contains(i));
            }

            Assert.AreEqual(hashSet.Count, hashSet.Count());
            Assert.AreEqual(hashSet.Count, hashSet.AsEnumerableDesc().Count());

            var rnd = new Random();
            var testSeries = Enumerable.Range(1, nodeCount).OrderBy(x => rnd.Next()).ToList();

            foreach (var item in testSeries)
            {
                hashSet.Add(item);
                Assert.AreEqual(true, hashSet.Contains(item));
            }

            Assert.AreEqual(hashSet.Count, hashSet.Count());
            Assert.AreEqual(hashSet.Count, hashSet.AsEnumerableDesc().Count());

            for (var i = 1; i <= nodeCount; i++)
            {
                hashSet.Remove(i);
                Assert.AreEqual(false, hashSet.Contains(i));
            }

            Assert.AreEqual(hashSet.Count, hashSet.Count());
            Assert.AreEqual(hashSet.Count, hashSet.AsEnumerableDesc().Count());
        }

        [TestMethod]
        public void OrderedHashSet_CornerCases()
        {
            var empty = new OrderedHashSet<int>();
            Assert.AreEqual(0, empty.Count);
            Assert.IsFalse(empty.Contains(1));
            Assert.AreEqual(0, empty.Count());
            Assert.AreEqual(0, empty.AsEnumerableDesc().Count());
            Assert.AreEqual(-1, empty.Remove(1));
            Assert.AreEqual(-1, empty.IndexOf(1));

            empty.Clear();
            Assert.AreEqual(0, empty.Count);

            var fromSorted = new OrderedHashSet<int>(new[] { 1, 2, 3 });
            Assert.AreEqual(3, fromSorted.Count);
            Assert.AreEqual(1, fromSorted.Min());
            Assert.AreEqual(3, fromSorted.Max());
            Assert.AreEqual(2, fromSorted.ElementAt(1));
            Assert.AreEqual(1, fromSorted.IndexOf(2));
            Assert.AreEqual(2, fromSorted[1]);
            Assert.AreEqual(3, fromSorted.NextHigher(2));
            Assert.AreEqual(1, fromSorted.NextLower(2));
            Assert.AreEqual(1, fromSorted.Remove(2));
            Assert.IsFalse(fromSorted.Contains(2));
            Assert.AreEqual(2, fromSorted.Count);

            var seen = new System.Collections.Generic.List<int>();
            foreach (var item in fromSorted)
            {
                seen.Add(item);
            }

            Assert.AreEqual(2, seen.Count);
            CollectionAssert.AreEqual(new System.Collections.Generic.List<int> { 1, 3 }, seen);

            var desc = fromSorted.AsEnumerableDesc().ToList();
            CollectionAssert.AreEqual(new System.Collections.Generic.List<int> { 3, 1 }, desc);
        }
    }
}
