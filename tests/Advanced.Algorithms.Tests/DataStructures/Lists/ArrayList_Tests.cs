using System;
using System.Collections.Generic;
using System.Linq;
using Advanced.Algorithms.DataStructures.Foundation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.DataStructures
{
    [TestClass]
    public class ArrayListTests
    {
        [TestMethod]
        public void ArrayList_Test()
        {
            var arrayList = new ArrayList<int>();
            var nodeCount = 1000;

            for (var i = 0; i <= nodeCount; i++)
            {
                arrayList.Add(i);
                Assert.AreEqual(true, arrayList.Contains(i));
            }

            //IEnumerable test using linq
            Assert.AreEqual(arrayList.Length, arrayList.Count());

            for (var i = 0; i <= nodeCount; i++)
            {
                arrayList.RemoveAt(0);
                Assert.AreEqual(false, arrayList.Contains(i));
            }

            var rnd = new Random();
            var testSeries = Enumerable.Range(1, nodeCount).OrderBy(x => rnd.Next()).ToList();

            foreach (var item in testSeries)
            {
                arrayList.Add(item);
                Assert.AreEqual(true, arrayList.Contains(item));
            }

            for (var i = 1; i <= nodeCount; i++) arrayList.RemoveAt(0);
        }

        [TestMethod]
        public void ArrayList_InsertAt_Test()
        {
            var arrayList = new ArrayList<int>();
            var nodeCount = 10;

            for (var i = 0; i <= nodeCount; i++)
            {
                arrayList.InsertAt(i, i);
                Assert.AreEqual(true, arrayList.Contains(i));
            }

            arrayList.InsertAt(5, 50000);

            //IEnumerable test using linq
            Assert.AreEqual(arrayList.Length, arrayList.Count());

            Assert.AreEqual(true, arrayList.Contains(50000));
            Assert.AreEqual(nodeCount + 2, arrayList.Length);
        }

        [TestMethod]
        public void ArrayList_Invalid_Size_And_Index_Throws()
        {
            Assert.ThrowsException<ArgumentException>(() => new ArrayList<int>(1));
            Assert.ThrowsException<ArgumentException>(() => new ArrayList<int>(0));

            var arrayList = new ArrayList<int>();
            arrayList.Add(1);

            Assert.ThrowsException<ArgumentException>(() => { var _ = arrayList[1]; });
            Assert.ThrowsException<ArgumentException>(() => { var _ = arrayList[-1]; });
            Assert.ThrowsException<ArgumentException>(() => arrayList[1] = 2);
            Assert.ThrowsException<ArgumentException>(() => arrayList[-1] = 2);
            Assert.ThrowsException<ArgumentException>(() => arrayList.RemoveAt(1));
            Assert.ThrowsException<ArgumentException>(() => arrayList.RemoveAt(-1));
            Assert.ThrowsException<ArgumentException>(() => arrayList.InsertAt(2, 9));
            Assert.ThrowsException<ArgumentException>(() => arrayList.InsertAt(-1, 9));
        }

        /// <summary>
        ///     Adversarial random ops oracle vs List&lt;T&gt;.
        /// </summary>
        [TestMethod]
        public void ArrayList_ListOracle_RandomOps()
        {
            var rng = new Random(42);
            var ours = new ArrayList<int>();
            var oracle = new List<int>();

            for (var step = 0; step < 1000; step++)
            {
                var op = rng.Next(5);

                if (op == 0 || oracle.Count == 0)
                {
                    var v = rng.Next(1000);
                    ours.Add(v);
                    oracle.Add(v);
                }
                else if (op == 1)
                {
                    var idx = rng.Next(oracle.Count + 1);
                    var v = rng.Next(1000);
                    ours.InsertAt(idx, v);
                    oracle.Insert(idx, v);
                }
                else if (op == 2)
                {
                    var idx = rng.Next(oracle.Count);
                    ours.RemoveAt(idx);
                    oracle.RemoveAt(idx);
                }
                else if (op == 3)
                {
                    var idx = rng.Next(oracle.Count);
                    var v = rng.Next(1000);
                    ours[idx] = v;
                    oracle[idx] = v;
                }
                else
                {
                    ours.Clear();
                    oracle.Clear();
                }

                Assert.AreEqual(oracle.Count, ours.Length);
                CollectionAssert.AreEqual(oracle, ours.ToList());
            }
        }
    }
}