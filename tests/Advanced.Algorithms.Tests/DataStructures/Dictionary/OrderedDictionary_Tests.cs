using System;
using System.Linq;
using Advanced.Algorithms.DataStructures.Foundation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.DataStructures
{
    [TestClass]
    public class OrderedDictionaryTests
    {
        [TestMethod]
        public void OrderedDictionary_Test()
        {
            var dictionary = new OrderedDictionary<int, int>();

            var nodeCount = 1000;

            for (var i = 0; i <= nodeCount; i++)
            {
                dictionary.Add(i, i);
                Assert.AreEqual(true, dictionary.ContainsKey(i));
            }

            Assert.AreEqual(dictionary.Count, dictionary.Count());
            Assert.AreEqual(dictionary.Count, dictionary.AsEnumerableDesc().Count());

            for (var i = 0; i <= nodeCount; i++)
            {
                dictionary.Remove(i);
                Assert.AreEqual(false, dictionary.ContainsKey(i));
            }

            Assert.AreEqual(dictionary.Count, dictionary.Count());
            Assert.AreEqual(dictionary.Count, dictionary.AsEnumerableDesc().Count());

            var rnd = new Random();
            var testSeries = Enumerable.Range(1, nodeCount).OrderBy(x => rnd.Next()).ToList();

            foreach (var item in testSeries)
            {
                dictionary.Add(item, item);
                Assert.AreEqual(true, dictionary.ContainsKey(item));
            }

            Assert.AreEqual(dictionary.Count, dictionary.Count());
            Assert.AreEqual(dictionary.Count, dictionary.AsEnumerableDesc().Count());

            for (var i = 1; i <= nodeCount; i++)
            {
                dictionary.Remove(i);
                Assert.AreEqual(false, dictionary.ContainsKey(i));
            }

            Assert.AreEqual(dictionary.Count, dictionary.Count());
            Assert.AreEqual(dictionary.Count, dictionary.AsEnumerableDesc().Count());
        }

        [TestMethod]
        public void OrderedDictionary_CornerCases()
        {
            var empty = new OrderedDictionary<int, int>();
            Assert.AreEqual(0, empty.Count);
            Assert.IsFalse(empty.ContainsKey(1));
            Assert.AreEqual(0, empty.Count());
            Assert.AreEqual(0, empty.AsEnumerableDesc().Count());
            Assert.AreEqual(-1, empty.Remove(1));
            Assert.AreEqual(-1, empty.IndexOf(1));
            Assert.AreEqual(default(System.Collections.Generic.KeyValuePair<int, int>), empty.Min());
            Assert.AreEqual(default(System.Collections.Generic.KeyValuePair<int, int>), empty.Max());
            Assert.AreEqual(default(System.Collections.Generic.KeyValuePair<int, int>), empty.NextHigher(1));
            Assert.AreEqual(default(System.Collections.Generic.KeyValuePair<int, int>), empty.NextLower(1));
            Assert.ThrowsException<ArgumentException>(() => { var _ = empty[1]; });

            empty.Clear();
            Assert.AreEqual(0, empty.Count);

            var fromSorted = new OrderedDictionary<int, string>(new[]
            {
                new System.Collections.Generic.KeyValuePair<int, string>(1, "a"),
                new System.Collections.Generic.KeyValuePair<int, string>(2, "b"),
                new System.Collections.Generic.KeyValuePair<int, string>(3, "c")
            });

            Assert.AreEqual(3, fromSorted.Count);
            Assert.AreEqual("a", fromSorted[1]);
            Assert.AreEqual(new System.Collections.Generic.KeyValuePair<int, string>(1, "a"), fromSorted.Min());
            Assert.AreEqual(new System.Collections.Generic.KeyValuePair<int, string>(3, "c"), fromSorted.Max());
            Assert.AreEqual(new System.Collections.Generic.KeyValuePair<int, string>(2, "b"), fromSorted.ElementAt(1));
            Assert.AreEqual(1, fromSorted.IndexOf(2));
            Assert.AreEqual(new System.Collections.Generic.KeyValuePair<int, string>(3, "c"), fromSorted.NextHigher(2));
            Assert.AreEqual(new System.Collections.Generic.KeyValuePair<int, string>(1, "a"), fromSorted.NextLower(2));

            fromSorted[2] = "B";
            Assert.AreEqual("B", fromSorted[2]);

            Assert.AreEqual(1, fromSorted.Remove(2));
            Assert.IsFalse(fromSorted.ContainsKey(2));
            Assert.AreEqual(2, fromSorted.Count);

            var seen = new System.Collections.Generic.List<int>();
            foreach (var pair in fromSorted)
            {
                seen.Add(pair.Key);
            }

            CollectionAssert.AreEqual(new System.Collections.Generic.List<int> { 1, 3 }, seen);

            var desc = fromSorted.AsEnumerableDesc().Select(x => x.Key).ToList();
            CollectionAssert.AreEqual(new System.Collections.Generic.List<int> { 3, 1 }, desc);
        }

        [TestMethod]
        public void OrderedDictionary_Oracle_Vs_SortedDictionary()
        {
            var aa = new OrderedDictionary<int, int>();
            var oracle = new System.Collections.Generic.SortedDictionary<int, int>();
            var rnd = new Random(3);

            for (var t = 0; t < 2000; t++)
            {
                var k = rnd.Next(-80, 80);
                var v = rnd.Next(1000);
                var op = rnd.Next(4);
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
                    if (oracle.ContainsKey(k))
                    {
                        aa.Remove(k);
                        oracle.Remove(k);
                    }
                    else
                    {
                        Assert.AreEqual(-1, aa.Remove(k));
                    }
                }
                else if (op == 2)
                {
                    aa[k] = v;
                    oracle[k] = v;
                }
                else
                {
                    Assert.AreEqual(oracle.ContainsKey(k), aa.ContainsKey(k));
                }

                Assert.AreEqual(oracle.Count, aa.Count);
            }

            CollectionAssert.AreEqual(oracle.Keys.ToList(), aa.Select(x => x.Key).ToList());
        }
    }
}
