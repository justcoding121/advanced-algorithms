using System;
using System.Linq;
using Advanced.Algorithms.DataStructures.Foundation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.DataStructures
{
    [TestClass]
    public class HashSetTests
    {
        [TestMethod]
        public void HashSet_SeparateChaining_Test()
        {
            var hashSet = new HashSet<int>();
            var nodeCount = 1000;

            for (var i = 0; i <= nodeCount; i++)
            {
                hashSet.Add(i);
                Assert.AreEqual(true, hashSet.Contains(i));
            }

            Assert.AreEqual(hashSet.Count, hashSet.Count());

            for (var i = 0; i <= nodeCount; i++)
            {
                hashSet.Remove(i);
                Assert.AreEqual(false, hashSet.Contains(i));
            }

            Assert.AreEqual(hashSet.Count, hashSet.Count());

            var rnd = new Random();
            var testSeries = Enumerable.Range(1, nodeCount).OrderBy(x => rnd.Next()).ToList();

            foreach (var item in testSeries)
            {
                hashSet.Add(item);
                Assert.AreEqual(true, hashSet.Contains(item));
            }

            Assert.AreEqual(hashSet.Count, hashSet.Count());

            foreach (var item in testSeries) Assert.AreEqual(true, hashSet.Contains(item));

            for (var i = 1; i <= nodeCount; i++)
            {
                hashSet.Remove(i);
                Assert.AreEqual(false, hashSet.Contains(i));
            }

            Assert.AreEqual(hashSet.Count, hashSet.Count());
        }

        [TestMethod]
        public void HashSet_OpenAddressing_Test()
        {
            var hashSet = new HashSet<int>(HashSetType.OpenAddressing);
            var nodeCount = 1000;

            for (var i = 0; i <= nodeCount; i++)
            {
                hashSet.Add(i);
                Assert.AreEqual(true, hashSet.Contains(i));
            }

            Assert.AreEqual(hashSet.Count, hashSet.Count());

            for (var i = 0; i <= nodeCount; i++)
            {
                hashSet.Remove(i);
                Assert.AreEqual(false, hashSet.Contains(i));
            }

            Assert.AreEqual(hashSet.Count, hashSet.Count());

            var rnd = new Random();
            var testSeries = Enumerable.Range(1, nodeCount).OrderBy(x => rnd.Next()).ToList();

            foreach (var item in testSeries)
            {
                hashSet.Add(item);
                Assert.AreEqual(true, hashSet.Contains(item));
            }

            Assert.AreEqual(hashSet.Count, hashSet.Count());

            foreach (var item in testSeries) Assert.AreEqual(true, hashSet.Contains(item));

            for (var i = 1; i <= nodeCount; i++)
            {
                hashSet.Remove(i);
                Assert.AreEqual(false, hashSet.Contains(i));
            }

            Assert.AreEqual(hashSet.Count, hashSet.Count());
        }

        [TestMethod]
        public void HashSet_Invalid_BucketSize_Throws()
        {
            Assert.ThrowsException<ArgumentException>(() => new HashSet<int>(HashSetType.SeparateChaining, 1));
            Assert.ThrowsException<ArgumentException>(() => new HashSet<int>(HashSetType.OpenAddressing, 0));
        }

        [TestMethod]
        public void HashSet_SeparateChaining_CornerCases()
        {
            var hashSet = new HashSet<CollisionItem>(HashSetType.SeparateChaining, 2);

            Assert.AreEqual(0, hashSet.Count);
            Assert.IsFalse(hashSet.Contains(new CollisionItem(1, 0)));
            Assert.AreEqual(0, hashSet.Count());

            hashSet.Clear();
            Assert.AreEqual(0, hashSet.Count);

            var a = new CollisionItem(1, 0);
            var b = new CollisionItem(2, 0);
            var c = new CollisionItem(3, 0);

            hashSet.Add(a);
            hashSet.Add(b);
            hashSet.Add(c);

            Assert.IsTrue(hashSet.Contains(a));
            Assert.IsTrue(hashSet.Contains(b));
            Assert.IsTrue(hashSet.Contains(c));
            Assert.AreEqual(3, hashSet.Count);

            Assert.ThrowsException<ArgumentException>(() => hashSet.Add(a));
            Assert.ThrowsException<ArgumentException>(() => hashSet.Remove(new CollisionItem(99, 0)));
            Assert.ThrowsException<ArgumentException>(() => hashSet.Remove(new CollisionItem(99, 1)));

            var seen = new System.Collections.Generic.List<int>();
            foreach (var item in hashSet)
            {
                seen.Add(item.Id);
            }

            Assert.AreEqual(3, seen.Count);
            Assert.IsTrue(seen.Contains(1));
            Assert.IsTrue(seen.Contains(2));
            Assert.IsTrue(seen.Contains(3));

            hashSet.Remove(b);
            Assert.IsFalse(hashSet.Contains(b));
            Assert.AreEqual(2, hashSet.Count);

            for (var i = 4; i <= 20; i++)
            {
                hashSet.Add(new CollisionItem(i, i % 2));
            }

            Assert.IsTrue(hashSet.Count >= 18);

            hashSet.Clear();
            Assert.AreEqual(0, hashSet.Count);
            Assert.IsFalse(hashSet.Contains(a));
        }

        [TestMethod]
        public void HashSet_OpenAddressing_CornerCases()
        {
            var hashSet = new HashSet<CollisionItem>(HashSetType.OpenAddressing, 2);

            Assert.AreEqual(0, hashSet.Count);
            Assert.IsFalse(hashSet.Contains(new CollisionItem(1, 0)));
            Assert.AreEqual(0, hashSet.Count());

            hashSet.Clear();
            Assert.AreEqual(0, hashSet.Count);

            var a = new CollisionItem(1, 0);
            var b = new CollisionItem(2, 0);
            var c = new CollisionItem(3, 0);

            hashSet.Add(a);
            hashSet.Add(b);
            hashSet.Add(c);

            Assert.IsTrue(hashSet.Contains(a));
            Assert.IsTrue(hashSet.Contains(b));
            Assert.IsTrue(hashSet.Contains(c));
            Assert.AreEqual(3, hashSet.Count);

            Assert.ThrowsException<ArgumentException>(() => hashSet.Add(a));
            Assert.ThrowsException<ArgumentException>(() => hashSet.Remove(new CollisionItem(99, 0)));
            Assert.ThrowsException<ArgumentException>(() => hashSet.Remove(new CollisionItem(99, 1)));

            var seen = new System.Collections.Generic.List<int>();
            foreach (var item in hashSet)
            {
                seen.Add(item.Id);
            }

            Assert.AreEqual(3, seen.Count);
            Assert.IsTrue(seen.Contains(1));
            Assert.IsTrue(seen.Contains(2));
            Assert.IsTrue(seen.Contains(3));

            hashSet.Remove(a);
            Assert.IsFalse(hashSet.Contains(a));
            Assert.IsTrue(hashSet.Contains(b));
            Assert.IsTrue(hashSet.Contains(c));

            for (var i = 4; i <= 40; i++)
            {
                hashSet.Add(new CollisionItem(i, i));
            }

            Assert.IsTrue(hashSet.Count >= 39);

            while (hashSet.Count > 2)
            {
                var first = hashSet.First();
                hashSet.Remove(first);
            }

            Assert.AreEqual(2, hashSet.Count);

            hashSet.Clear();
            Assert.AreEqual(0, hashSet.Count);
        }

        [TestMethod]
        public void HashSet_SeparateChaining_Grow_Shrink_Enumerator()
        {
            var hashSet = new HashSet<CollisionItem>(HashSetType.SeparateChaining, 3);

            Assert.AreEqual(0, hashSet.Count);
            Assert.IsFalse(hashSet.Contains(new CollisionItem(1, 0)));

            for (var i = 0; i < 30; i++)
            {
                hashSet.Add(new CollisionItem(i, i));
            }

            Assert.IsTrue(hashSet.Count >= 30);

            using (var enumerator = hashSet.GetEnumerator())
            {
                Assert.IsTrue(enumerator.MoveNext());
                enumerator.Reset();
                Assert.IsTrue(enumerator.MoveNext());
                Assert.IsNotNull(enumerator.Current);
            }

            var a = new CollisionItem(100, 0);
            var b = new CollisionItem(101, 0);
            hashSet.Add(a);
            hashSet.Add(b);
            Assert.IsTrue(hashSet.Contains(a));
            hashSet.Remove(a);
            Assert.IsFalse(hashSet.Contains(a));
            Assert.IsTrue(hashSet.Contains(b));

            while (hashSet.Count > 3)
            {
                hashSet.Remove(hashSet.First());
            }

            Assert.IsTrue(hashSet.Count <= 3);
            hashSet.Clear();
            Assert.AreEqual(0, hashSet.Count);
        }

        private sealed class CollisionItem
        {
            private readonly int hash;

            public CollisionItem(int id, int hash)
            {
                Id = id;
                this.hash = hash;
            }

            public int Id { get; }

            public override int GetHashCode()
            {
                return hash;
            }

            public override bool Equals(object obj)
            {
                return obj is CollisionItem other && Id == other.Id;
            }
        }
    }
}
