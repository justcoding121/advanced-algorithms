using System;
using System.Collections.Generic;
using System.Linq;
using Advanced.Algorithms.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.DataStructures
{
    [TestClass]
    public class SkipListTests
    {
        [TestMethod]
        public void SkipList_Test()
        {
            var skipList = new SkipList<int>();

            for (var i = 1; i < 100; i++) skipList.Insert(i);

            for (var i = 1; i < 100; i++) Assert.AreEqual(i, skipList.Find(i));

            Assert.AreEqual(0, skipList.Find(101));

            for (var i = 1; i < 100; i++)
            {
                skipList.Delete(i);
                Assert.AreEqual(0, skipList.Find(i));
            }

            for (var i = 1; i < 50; i++) skipList.Insert(i);

            Assert.ThrowsException<ArgumentException>(() => skipList.Insert(25));
            Assert.ThrowsException<ArgumentException>(() => skipList.Delete(52));

            //IEnumerable test using linq
            Assert.AreEqual(skipList.Count, skipList.Count());

            for (var i = 1; i < 50; i++) Assert.AreEqual(i, skipList.Find(i));

            for (var i = 1; i < 50; i++)
            {
                skipList.Delete(i);
                Assert.AreEqual(0, skipList.Find(i));
            }
        }

        [TestMethod]
        public void SkipList_Duplicate_And_Missing_Throws()
        {
            var skipList = new SkipList<int>();
            skipList.Insert(1);

            Assert.ThrowsException<ArgumentException>(() => skipList.Insert(1));
            Assert.ThrowsException<ArgumentException>(() => skipList.Delete(2));
            Assert.AreEqual(1, skipList.Find(1));
        }

        /// <summary>
        ///     default(T) must be insertable once and reject a second insert.
        /// </summary>
        [TestMethod]
        public void SkipList_DefaultValue_Duplicate_Throws()
        {
            var skipList = new SkipList<int>();
            skipList.Insert(0);
            Assert.AreEqual(1, skipList.Count);
            Assert.ThrowsException<ArgumentException>(() => skipList.Insert(0));
            Assert.AreEqual(1, skipList.Count);
            CollectionAssert.AreEqual(new[] { 0 }, skipList.ToList());
        }

        /// <summary>
        ///     Sorted-set oracle: insert/delete unique keys, enumerate ascending.
        /// </summary>
        [TestMethod]
        public void SkipList_SortedSetOracle_RandomOps()
        {
            var rng = new Random(7);
            var skipList = new SkipList<int>();
            var oracle = new SortedSet<int>();

            for (var step = 0; step < 400; step++)
            {
                var v = rng.Next(80);
                var op = rng.Next(3);

                if (op == 0 || oracle.Count == 0)
                {
                    if (oracle.Add(v))
                        skipList.Insert(v);
                    else
                        Assert.ThrowsException<ArgumentException>(() => skipList.Insert(v));
                }
                else if (op == 1)
                {
                    if (oracle.Remove(v))
                        skipList.Delete(v);
                    else
                        Assert.ThrowsException<ArgumentException>(() => skipList.Delete(v));
                }
                else
                {
                    if (oracle.Contains(v))
                        Assert.AreEqual(v, skipList.Find(v));
                    else if (v != 0)
                        Assert.AreEqual(0, skipList.Find(v));
                }

                Assert.AreEqual(oracle.Count, skipList.Count);
                CollectionAssert.AreEqual(oracle.ToList(), skipList.ToList());
            }
        }
    }
}
