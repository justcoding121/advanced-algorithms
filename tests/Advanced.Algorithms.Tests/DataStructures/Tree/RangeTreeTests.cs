using System;
using System.Collections.Generic;
using System.Linq;
using Advanced.Algorithms.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.DataStructures
{
    [TestClass]
    public class RangeTreeTests
    {
        /// <summary>
        ///     Smoke test
        /// </summary>
        [TestMethod]
        public void RangeTree1D_Smoke_Test()
        {
            var tree = new RangeTree<int>(1);

            tree.Insert(new[] { 0 });
            tree.Insert(new[] { 1 });
            tree.Insert(new[] { 2 });
            tree.Insert(new[] { 3 });
            tree.Insert(new[] { 4 });
            tree.Insert(new[] { 5 });
            tree.Insert(new[] { 6 });
            tree.Insert(new[] { 7 });

            //IEnumerable test
            Assert.AreEqual(tree.Count, tree.Count());

            var rangeResult = tree.RangeSearch(new[] { 2 }, new[] { 6 });
            Assert.IsTrue(rangeResult.Count == 5);

            //IEnumerable test
            Assert.AreEqual(tree.Count, tree.Count());

            tree.Delete(new[] { 4 });
            rangeResult = tree.RangeSearch(new[] { -1 }, new[] { 6 });
            Assert.IsTrue(rangeResult.Count == 6);

            tree.Delete(new[] { 0 });
            tree.Delete(new[] { 1 });
            tree.Delete(new[] { 2 });
            tree.Delete(new[] { 3 });
            tree.Delete(new[] { 5 });
            tree.Delete(new[] { 6 });
            tree.Delete(new[] { 7 });
        }

        [TestMethod]
        public void RangeTree2D_Smoke_Test()
        {
            var tree = new RangeTree<int>(2);

            tree.Insert(new[] { 0, 1 });
            tree.Insert(new[] { 1, 1 });
            tree.Insert(new[] { 2, 5 });
            tree.Insert(new[] { 3, 6 });
            tree.Insert(new[] { 4, 5 });
            tree.Insert(new[] { 4, 7 });
            tree.Insert(new[] { 5, 8 });
            tree.Insert(new[] { 6, 9 });
            tree.Insert(new[] { 7, 10 });

            //IEnumerable test
            Assert.AreEqual(tree.Count, tree.Count());

            var rangeResult = tree.RangeSearch(new[] { 1, 1 }, new[] { 3, 7 });
            Assert.IsTrue(rangeResult.Count == 3);

            tree.Delete(new[] { 2, 5 });
            rangeResult = tree.RangeSearch(new[] { 1, 1 }, new[] { 3, 7 });
            Assert.IsTrue(rangeResult.Count == 2);

            //IEnumerable test
            Assert.AreEqual(tree.Count, tree.Count());

            tree.Delete(new[] { 3, 6 });
            rangeResult = tree.RangeSearch(new[] { 1, 1 }, new[] { 3, 7 });
            Assert.IsTrue(rangeResult.Count == 1);

            tree.Delete(new[] { 0, 1 });
            tree.Delete(new[] { 1, 1 });
            tree.Delete(new[] { 4, 5 });
            tree.Delete(new[] { 4, 7 });
            tree.Delete(new[] { 5, 8 });
            tree.Delete(new[] { 6, 9 });
            tree.Delete(new[] { 7, 10 });

            //IEnumerable test
            Assert.AreEqual(tree.Count, tree.Count());
        }

        [TestMethod]
        public void RangeTree_Corner_Cases()
        {
            Assert.ThrowsException<ArgumentException>(() => new RangeTree<int>(0));

            var tree = new RangeTree<int>(1);
            Assert.AreEqual(0, tree.Count);
            Assert.AreEqual(0, tree.Count());

            Assert.ThrowsException<ArgumentNullException>(() => tree.Insert(null));
            Assert.ThrowsException<ArgumentException>(() => tree.Insert(new[] { 1, 2 }));

            tree.Insert(new[] { 1 });
            Assert.ThrowsException<ArgumentException>(() => tree.Insert(new[] { 1 }));
            Assert.ThrowsException<ArgumentException>(() => tree.Delete(new[] { 9 }));

            Assert.AreEqual(1, tree.RangeSearch(new[] { 1 }, new[] { 1 }).Count);
            tree.Delete(new[] { 1 });
            Assert.AreEqual(0, tree.Count);
            Assert.AreEqual(0, tree.RangeSearch(new[] { 0 }, new[] { 10 }).Count);
        }

        [TestMethod]
        public void RangeTree_Brute_Range_Oracle()
        {
            var tree = new RangeTree<int>(2);
            var pts = new List<int[]>
            {
                new[] { 1, 2 }, new[] { 3, 4 }, new[] { 5, 1 }, new[] { 2, 8 }, new[] { 7, 7 }
            };

            foreach (var p in pts) tree.Insert(p);

            var start = new[] { 0, 0 };
            var end = new[] { 4, 5 };
            var expected = pts.Count(p => p[0] >= start[0] && p[0] <= end[0] && p[1] >= start[1] && p[1] <= end[1]);
            Assert.AreEqual(expected, tree.RangeSearch(start, end).Count);

            tree.Delete(new[] { 3, 4 });
            Assert.AreEqual(4, tree.Count);
            Assert.AreEqual(1, tree.RangeSearch(start, end).Count);
        }
    }
}