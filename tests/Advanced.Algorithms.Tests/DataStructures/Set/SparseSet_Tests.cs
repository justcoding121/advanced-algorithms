using System;
using System.Linq;
using Advanced.Algorithms.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.DataStructures
{
    [TestClass]
    public class SparseSetTests
    {
        [TestMethod]
        public void SparseSet_Smoke_Test()
        {
            var set = new SparseSet(15, 10);

            set.Add(6);
            set.Add(15);
            set.Add(0);

            //IEnumerable test
            Assert.AreEqual(set.Count, set.Count());

            set.Remove(15);

            Assert.IsTrue(set.HasItem(6));
            Assert.AreEqual(2, set.Count);

            //IEnumerable test
            Assert.AreEqual(set.Count, set.Count());
        }

        [TestMethod]
        public void SparseSet_Stress_Test()
        {
            var set = new SparseSet(1000, 1000);

            var random = new Random();
            var testCollection = Enumerable.Range(0, 1000)
                .OrderBy(x => random.Next())
                .ToList();

            foreach (var element in testCollection) set.Add(element);

            //IEnumerable test
            Assert.AreEqual(set.Count, set.Count());

            foreach (var element in testCollection) Assert.IsTrue(set.HasItem(element));

            foreach (var element in testCollection)
            {
                Assert.IsTrue(set.HasItem(element));
                set.Remove(element);
                Assert.IsFalse(set.HasItem(element));
            }

            //IEnumerable test
            Assert.AreEqual(set.Count, set.Count());
        }

        [TestMethod]
        public void SparseSet_Oracle_Vs_HashSet()
        {
            var set = new SparseSet(100, 50);
            var oracle = new System.Collections.Generic.HashSet<int>();
            var rnd = new Random(5);

            for (var t = 0; t < 1000; t++)
            {
                var v = rnd.Next(0, 101);
                var op = rnd.Next(4);
                if (op == 0)
                {
                    if (oracle.Contains(v) || oracle.Count >= 50)
                        continue;
                    set.Add(v);
                    oracle.Add(v);
                }
                else if (op == 1)
                {
                    if (!oracle.Contains(v))
                        continue;
                    set.Remove(v);
                    oracle.Remove(v);
                }
                else if (op == 2)
                {
                    Assert.AreEqual(oracle.Contains(v), set.HasItem(v));
                }
                else
                {
                    set.Clear();
                    oracle.Clear();
                    for (var i = 0; i <= 100; i++)
                        Assert.IsFalse(set.HasItem(i));
                }

                Assert.AreEqual(oracle.Count, set.Count);
            }
        }

        [TestMethod]
        public void SparseSet_Duplicate_And_Clear()
        {
            var set = new SparseSet(10, 10);
            set.Add(3);
            Assert.ThrowsException<ArgumentException>(() => set.Add(3));
            Assert.AreEqual(1, set.Count);

            set.Add(7);
            set.Clear();
            Assert.AreEqual(0, set.Count);
            Assert.IsFalse(set.HasItem(3));
            Assert.IsFalse(set.HasItem(7));
            set.Add(3);
            Assert.IsTrue(set.HasItem(3));
            Assert.AreEqual(1, set.Count);
        }
    }
}
