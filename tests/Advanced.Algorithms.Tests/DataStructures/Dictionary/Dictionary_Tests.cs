using System;
using System.Linq;
using Advanced.Algorithms.DataStructures.Foundation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.DataStructures
{
    [TestClass]
    public class DictionaryTests
    {
        [TestMethod]
        public void Dictionary_SeparateChaining_Test()
        {
            var dictionary = new Dictionary<int, int>();
            var nodeCount = 1000;

            for (var i = 0; i <= nodeCount; i++)
            {
                dictionary.Add(i, i);
                Assert.AreEqual(true, dictionary.ContainsKey(i));
            }

            Assert.AreEqual(dictionary.Count, dictionary.Count());

            for (var i = 0; i <= nodeCount; i++)
            {
                dictionary.Remove(i);
                Assert.AreEqual(false, dictionary.ContainsKey(i));
            }

            Assert.AreEqual(dictionary.Count, dictionary.Count());

            var rnd = new Random();
            var testSeries = Enumerable.Range(1, nodeCount).OrderBy(x => rnd.Next()).ToList();

            foreach (var item in testSeries)
            {
                dictionary.Add(item, item);
                Assert.AreEqual(true, dictionary.ContainsKey(item));
            }

            Assert.AreEqual(dictionary.Count, dictionary.Count());

            foreach (var item in testSeries) Assert.AreEqual(true, dictionary.ContainsKey(item));

            for (var i = 1; i <= nodeCount; i++)
            {
                dictionary.Remove(i);
                Assert.AreEqual(false, dictionary.ContainsKey(i));
            }

            Assert.AreEqual(dictionary.Count, dictionary.Count());
        }

        [TestMethod]
        public void Dictionary_OpenAddressing_Test()
        {
            var dictionary = new Dictionary<int, int>(DictionaryType.OpenAddressing);
            var nodeCount = 1000;

            for (var i = 0; i <= nodeCount; i++)
            {
                dictionary.Add(i, i);
                Assert.AreEqual(true, dictionary.ContainsKey(i));
            }

            Assert.AreEqual(dictionary.Count, dictionary.Count());

            for (var i = 0; i <= nodeCount; i++)
            {
                dictionary.Remove(i);
                Assert.AreEqual(false, dictionary.ContainsKey(i));
            }

            Assert.AreEqual(dictionary.Count, dictionary.Count());

            var rnd = new Random();
            var testSeries = Enumerable.Range(1, nodeCount).OrderBy(x => rnd.Next()).ToList();

            foreach (var item in testSeries)
            {
                dictionary.Add(item, item);
                Assert.AreEqual(true, dictionary.ContainsKey(item));
            }

            Assert.AreEqual(dictionary.Count, dictionary.Count());

            foreach (var item in testSeries) Assert.AreEqual(true, dictionary.ContainsKey(item));

            for (var i = 1; i <= nodeCount; i++)
            {
                dictionary.Remove(i);
                Assert.AreEqual(false, dictionary.ContainsKey(i));
            }

            Assert.AreEqual(dictionary.Count, dictionary.Count());
        }

        [TestMethod]
        public void Dictionary_Invalid_BucketSize_Throws()
        {
            Assert.ThrowsException<ArgumentException>(() => new Dictionary<int, int>(DictionaryType.SeparateChaining, 1));
            Assert.ThrowsException<ArgumentException>(() => new Dictionary<int, int>(DictionaryType.OpenAddressing, 0));
        }

        [TestMethod]
        public void Dictionary_SeparateChaining_CornerCases()
        {
            var dictionary = new Dictionary<CollisionKey, int>(DictionaryType.SeparateChaining, 2);

            Assert.AreEqual(0, dictionary.Count);
            Assert.IsFalse(dictionary.ContainsKey(new CollisionKey(1, 0)));
            Assert.AreEqual(0, dictionary.Count());

            dictionary.Clear();
            Assert.AreEqual(0, dictionary.Count);

            var a = new CollisionKey(1, 0);
            var b = new CollisionKey(2, 0);
            var c = new CollisionKey(3, 0);

            dictionary.Add(a, 10);
            dictionary.Add(b, 20);
            dictionary.Add(c, 30);

            Assert.AreEqual(10, dictionary[a]);
            Assert.AreEqual(20, dictionary[b]);
            Assert.AreEqual(30, dictionary[c]);

            dictionary[a] = 11;
            Assert.AreEqual(11, dictionary[a]);

            Assert.ThrowsException<ArgumentException>(() => dictionary.Add(a, 99));
            Assert.ThrowsException<ArgumentException>(() => dictionary.Remove(new CollisionKey(99, 0)));
            Assert.ThrowsException<ArgumentException>(() => dictionary.Remove(new CollisionKey(99, 1)));
            Assert.ThrowsException<ArgumentException>(() => { var _ = dictionary[new CollisionKey(99, 1)]; });

            var keys = new System.Collections.Generic.List<int>();
            foreach (var pair in dictionary)
            {
                keys.Add(pair.Key.Id);
            }

            Assert.AreEqual(3, keys.Count);
            Assert.IsTrue(keys.Contains(1));
            Assert.IsTrue(keys.Contains(2));
            Assert.IsTrue(keys.Contains(3));

            dictionary.Remove(b);
            Assert.IsFalse(dictionary.ContainsKey(b));

            for (var i = 4; i <= 20; i++)
            {
                dictionary.Add(new CollisionKey(i, i % 2), i);
            }

            Assert.IsTrue(dictionary.Count >= 18);

            dictionary.Clear();
            Assert.AreEqual(0, dictionary.Count);
        }

        [TestMethod]
        public void Dictionary_OpenAddressing_CornerCases()
        {
            var dictionary = new Dictionary<CollisionKey, int>(DictionaryType.OpenAddressing, 2);

            Assert.AreEqual(0, dictionary.Count);
            Assert.IsFalse(dictionary.ContainsKey(new CollisionKey(1, 0)));
            Assert.AreEqual(0, dictionary.Count());

            dictionary.Clear();
            Assert.AreEqual(0, dictionary.Count);

            var a = new CollisionKey(1, 0);
            var b = new CollisionKey(2, 0);
            var c = new CollisionKey(3, 0);

            dictionary.Add(a, 10);
            dictionary.Add(b, 20);
            dictionary.Add(c, 30);

            Assert.AreEqual(10, dictionary[a]);
            dictionary[a] = 11;
            Assert.AreEqual(11, dictionary[a]);

            Assert.ThrowsException<ArgumentException>(() => dictionary.Add(a, 99));
            Assert.ThrowsException<ArgumentException>(() => dictionary.Remove(new CollisionKey(99, 0)));
            Assert.ThrowsException<ArgumentException>(() => dictionary.Remove(new CollisionKey(99, 1)));
            Assert.ThrowsException<ArgumentException>(() => { var _ = dictionary[new CollisionKey(99, 1)]; });

            var keys = new System.Collections.Generic.List<int>();
            foreach (var pair in dictionary)
            {
                keys.Add(pair.Key.Id);
            }

            Assert.AreEqual(3, keys.Count);

            dictionary.Remove(a);
            Assert.IsFalse(dictionary.ContainsKey(a));
            Assert.IsTrue(dictionary.ContainsKey(b));
            Assert.IsTrue(dictionary.ContainsKey(c));

            for (var i = 4; i <= 40; i++)
            {
                dictionary.Add(new CollisionKey(i, i), i);
            }

            Assert.IsTrue(dictionary.Count >= 39);

            while (dictionary.Count > 2)
            {
                var first = dictionary.First();
                dictionary.Remove(first.Key);
            }

            Assert.AreEqual(2, dictionary.Count);

            dictionary.Clear();
            Assert.AreEqual(0, dictionary.Count);
        }

        [TestMethod]
        public void Dictionary_SeparateChaining_Indexer_And_Enumerator()
        {
            var dictionary = new Dictionary<CollisionKey, int>(DictionaryType.SeparateChaining, 3);

            var a = new CollisionKey(1, 0);
            var collidingMissing = new CollisionKey(2, 0);
            var fresh = new CollisionKey(3, 1);

            dictionary.Add(a, 10);
            dictionary[collidingMissing] = 30;
            Assert.AreEqual(30, dictionary[collidingMissing]);
            Assert.IsTrue(dictionary.ContainsKey(collidingMissing));

            dictionary.Add(fresh, 20);
            Assert.AreEqual(20, dictionary[fresh]);
            dictionary[fresh] = 21;
            Assert.AreEqual(21, dictionary[fresh]);
            dictionary[a] = 11;
            Assert.AreEqual(11, dictionary[a]);

            using (var enumerator = dictionary.GetEnumerator())
            {
                Assert.IsTrue(enumerator.MoveNext());
                enumerator.Reset();
                Assert.IsTrue(enumerator.MoveNext());
                Assert.IsNotNull(enumerator.Current);
            }

            for (var i = 10; i < 40; i++)
            {
                dictionary.Add(new CollisionKey(i, i), i);
            }

            Assert.IsTrue(dictionary.Count >= 32);

            while (dictionary.Count > 4)
            {
                dictionary.Remove(dictionary.First().Key);
            }

            Assert.IsTrue(dictionary.Count <= 4);
        }

        [TestMethod]
        public void Dictionary_Oracle_Vs_SystemDictionary()
        {
            foreach (var type in new[] { DictionaryType.SeparateChaining, DictionaryType.OpenAddressing })
            {
                var aa = new Dictionary<int, int>(type);
                var oracle = new System.Collections.Generic.Dictionary<int, int>();
                var rnd = new Random(99);

                for (var t = 0; t < 4000; t++)
                {
                    var k = rnd.Next(-150, 150);
                    var v = rnd.Next(10000);
                    var op = rnd.Next(5);
                    if (op == 0)
                    {
                        if (oracle.ContainsKey(k))
                            Assert.ThrowsException<ArgumentException>(() => aa.Add(k, v));
                        else
                        {
                            aa.Add(k, v);
                            oracle.Add(k, v);
                        }
                    }
                    else if (op == 1)
                    {
                        if (!oracle.ContainsKey(k))
                            Assert.ThrowsException<ArgumentException>(() => aa.Remove(k));
                        else
                        {
                            aa.Remove(k);
                            oracle.Remove(k);
                        }
                    }
                    else if (op == 2)
                    {
                        Assert.AreEqual(oracle.ContainsKey(k), aa.ContainsKey(k));
                    }
                    else if (op == 3)
                    {
                        aa[k] = v;
                        oracle[k] = v;
                    }
                    else if (oracle.ContainsKey(k))
                    {
                        Assert.AreEqual(oracle[k], aa[k]);
                    }

                    Assert.AreEqual(oracle.Count, aa.Count);
                }
            }
        }

        [TestMethod]
        public void Dictionary_IntMinValue_Key_DoesNotThrow()
        {
            foreach (var type in new[] { DictionaryType.SeparateChaining, DictionaryType.OpenAddressing })
            {
                var d = new Dictionary<int, int>(type);
                d.Add(int.MinValue, 1);
                Assert.IsTrue(d.ContainsKey(int.MinValue));
                Assert.AreEqual(1, d[int.MinValue]);
                d.Remove(int.MinValue);
                Assert.IsFalse(d.ContainsKey(int.MinValue));
            }
        }

        private sealed class CollisionKey
        {
            private readonly int hash;

            public CollisionKey(int id, int hash)
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
                return obj is CollisionKey other && Id == other.Id;
            }
        }
    }
}
