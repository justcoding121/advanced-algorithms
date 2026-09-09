using System;
using System.Collections.Generic;
using System.Linq;
using Advanced.Algorithms.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.DataStructures
{
    [TestClass]
    public class IntervalTreeTests
    {
        /// </summary>
        [TestMethod]
        public void IntervalTree_1D_Smoke_Test()
        {
            var tree = new IntervalTree<int>(1);

            tree.Insert(new[] { 1 }, new[] { 2 });
            tree.Insert(new[] { 3 }, new[] { 4 });
            tree.Insert(new[] { 5 }, new[] { 6 });
            tree.Insert(new[] { 7 }, new[] { 8 });
            tree.Insert(new[] { 9 }, new[] { 10 });
            tree.Insert(new[] { 11 }, new[] { 12 });

            //IEnumerable test
            Assert.AreEqual(tree.Count, tree.Count());

            Assert.AreEqual(tree.Count, 6);

            Assert.IsTrue(tree.DoOverlap(new[] { 1 }, new[] { 10 }));
            tree.Delete(new[] { 1 }, new[] { 2 });
            Assert.IsFalse(tree.DoOverlap(new[] { 1 }, new[] { 2 }));

            tree.Delete(new[] { 3 }, new[] { 4 });
            tree.Delete(new[] { 5 }, new[] { 6 });
            tree.Delete(new[] { 7 }, new[] { 8 });
            tree.Delete(new[] { 9 }, new[] { 10 });

            //IEnumerable test
            Assert.AreEqual(tree.Count, tree.Count());

            Assert.IsTrue(tree.DoOverlap(new[] { 11 }, new[] { 12 }));
            tree.Delete(new[] { 11 }, new[] { 12 });
            Assert.IsFalse(tree.DoOverlap(new[] { 11 }, new[] { 12 }));


            Assert.AreEqual(tree.Count, 0);
            //IEnumerable test
            Assert.AreEqual(tree.Count, tree.Count());
        }

        /// </summary>
        [TestMethod]
        public void IntervalTree_1D_Accuracy_Test()
        {
            var nodeCount = 100;
            var tree = new IntervalTree<int>(1);

            var rnd = new Random();
            var intervals = new HashSet<Tuple<int[], int[]>>(new IntervalComparer<int>());

            for (var i = 0; i < nodeCount; i++)
            {
                var start = i - 1000 + rnd.Next(1, 10);
                var interval = new Tuple<int[], int[]>(new[] { start }, new[] { start + rnd.Next(1, 10) });
                intervals.Add(interval);
            }

            foreach (var interval in intervals) tree.Insert(interval.Item1, interval.Item2);

            //IEnumerable test
            Assert.AreEqual(tree.Count, tree.Count());

            foreach (var interval in intervals)
            {
                Assert.IsTrue(tree.DoOverlap(interval.Item1,
                    interval.Item2));
                var testStart = CloneArray(interval.Item1);
                var testEnd = CloneArray(interval.Item2);

                testStart[0] = testStart[0] - rnd.Next(1, 5);
                testEnd[0] = testEnd[0] + rnd.Next(1, 5);

                Assert.IsTrue(tree.DoOverlap(testStart,
                    testEnd));
            }

            //IEnumerable test
            Assert.AreEqual(tree.Count, tree.Count());

            var notDeleted = new HashSet<Tuple<int[], int[]>>(new IntervalComparer<int>());

            foreach (var interval in intervals) notDeleted.Add(interval);

            foreach (var interval in intervals)
            {
                tree.Delete(interval.Item1, interval.Item2);
                notDeleted.Remove(interval);

                foreach (var existingInterval in notDeleted)
                {
                    var testStart = CloneArray(existingInterval.Item1);
                    var testEnd = CloneArray(existingInterval.Item2);

                    Assert.IsTrue(tree.DoOverlap(testStart, testEnd));
                }
            }

            //IEnumerable test
            Assert.AreEqual(tree.Count, tree.Count());
        }

        /// </summary>
        [TestMethod]
        public void IntervalTree_2D_Accuracy_Test()
        {
            var nodeCount = 100;
            const int dimension = 2;

            var tree = new IntervalTree<int>(dimension);

            var rnd = new Random();
            var intervals = new HashSet<Tuple<int[], int[]>>(new IntervalComparer<int>());

            for (var i = 0; i < nodeCount; i++)
            {
                var startx = i - 1000 + rnd.Next(1, 10);
                var starty = i + 15 + rnd.Next(1, 10);
                //(x1,y1) and (x2, y2)
                var interval = new Tuple<int[], int[]>(new int[dimension] { startx, starty },
                    new int[dimension] { startx + rnd.Next(1, 10), starty + rnd.Next(1, 10) });

                if (intervals.Add(interval)) tree.Insert(interval.Item1, interval.Item2);
            }

            foreach (var interval in intervals)
            {
                Assert.IsTrue(tree.DoOverlap(interval.Item1,
                    interval.Item2));

                var testStart = CloneArray(interval.Item1);
                var testEnd = CloneArray(interval.Item2);

                testStart[0] = testStart[0] - rnd.Next(1, 5);
                testStart[1] = testStart[1] - rnd.Next(1, 5);

                testEnd[0] = testEnd[0] + rnd.Next(1, 5);
                testEnd[1] = testEnd[1] + rnd.Next(1, 5);

                Assert.IsTrue(tree.DoOverlap(testStart, testEnd));
            }

            var notDeleted = new HashSet<Tuple<int[], int[]>>(new IntervalComparer<int>());

            foreach (var interval in intervals) notDeleted.Add(interval);

            foreach (var interval in intervals)
            {
                tree.Delete(interval.Item1, interval.Item2);
                notDeleted.Remove(interval);

                foreach (var existingInterval in notDeleted)
                {
                    var testStart = CloneArray(existingInterval.Item1);
                    var testEnd = CloneArray(existingInterval.Item2);

                    Assert.IsTrue(tree.DoOverlap(testStart, testEnd));
                }
            }
        }

        private int[] CloneArray(int[] array)
        {
            var newArray = new int[array.Length];

            for (var i = 0; i < array.Length; i++) newArray[i] = array[i];

            return newArray;
        }


        private int[][] CloneArray(int[][] array)
        {
            var newArray = new int[array.Length][];

            for (var i = 0; i < array.Length; i++)
            for (var j = 0; j < array.Length; j++)
                newArray[i][j] = array[i][j];

            return newArray;
        }

        [TestMethod]
        public void IntervalTree_Corner_Cases()
        {
            Assert.ThrowsException<ArgumentException>(() => new IntervalTree<int>(0));
            Assert.ThrowsException<ArgumentException>(() => new IntervalTree<int>(-1));

            var tree = new IntervalTree<int>(1);
            Assert.AreEqual(0, tree.Count);
            Assert.AreEqual(0, tree.Count());
            Assert.IsFalse(tree.DoOverlap(new[] { 1 }, new[] { 2 }));
            Assert.AreEqual(0, tree.GetOverlaps(new[] { 1 }, new[] { 2 }).Count);

            Assert.ThrowsException<ArgumentNullException>(() => tree.Insert(null, new[] { 1 }));
            Assert.ThrowsException<ArgumentNullException>(() => tree.Insert(new[] { 1 }, null));
            Assert.ThrowsException<ArgumentException>(() => tree.Insert(new[] { 1, 2 }, new[] { 3 }));
            Assert.ThrowsException<ArgumentException>(() => tree.Insert(new[] { int.MinValue }, new[] { 1 }));
            Assert.ThrowsException<ArgumentException>(() => tree.Insert(new[] { 1 }, new[] { int.MinValue }));

            // reverse endpoints are sorted internally
            tree.Insert(new[] { 5 }, new[] { 3 });
            Assert.IsTrue(tree.DoOverlap(new[] { 3 }, new[] { 5 }));
            Assert.AreEqual(1, tree.GetOverlaps(new[] { 4 }, new[] { 4 }).Count);
            tree.Delete(new[] { 5 }, new[] { 3 });
            Assert.AreEqual(0, tree.Count);

            tree.Insert(new[] { 1 }, new[] { 2 });
            Assert.ThrowsException<ArgumentException>(() => tree.Insert(new[] { 1 }, new[] { 2 }));
            Assert.ThrowsException<ArgumentException>(() => tree.Delete(new[] { 9 }, new[] { 10 }));

            // same start, different ends shares a node
            tree.Insert(new[] { 1 }, new[] { 4 });
            Assert.IsTrue(tree.DoOverlap(new[] { 3 }, new[] { 3 }));
            Assert.IsFalse(tree.DoOverlap(new[] { 10 }, new[] { 12 }));

            var overlaps = tree.GetOverlaps(new[] { 1 }, new[] { 4 });
            Assert.IsTrue(overlaps.Count >= 1);

            tree.Delete(new[] { 1 }, new[] { 2 });
            tree.Delete(new[] { 1 }, new[] { 4 });
            Assert.AreEqual(0, tree.Count);
        }

        [TestMethod]
        public void IntervalTree_2D_Overlap_Delete_Empty()
        {
            var tree = new IntervalTree<int>(2);

            tree.Insert(new[] { 1, 1 }, new[] { 3, 3 });
            tree.Insert(new[] { 5, 5 }, new[] { 7, 7 });

            Assert.IsTrue(tree.DoOverlap(new[] { 2, 2 }, new[] { 2, 2 }));
            Assert.IsFalse(tree.DoOverlap(new[] { 10, 10 }, new[] { 11, 11 }));

            var hits = tree.GetOverlaps(new[] { 2, 2 }, new[] { 6, 6 });
            Assert.IsTrue(hits.Count >= 1);

            Assert.AreEqual(2, tree.Count());
            tree.Delete(new[] { 1, 1 }, new[] { 3, 3 });
            Assert.AreEqual(1, tree.Count);
            Assert.ThrowsException<ArgumentException>(() => tree.Delete(new[] { 1, 1 }, new[] { 3, 3 }));

            tree.Delete(new[] { 5, 5 }, new[] { 7, 7 });
            Assert.AreEqual(0, tree.Count);
            Assert.IsFalse(tree.DoOverlap(new[] { 2, 2 }, new[] { 2, 2 }));
        }

        [TestMethod]
        public void IntervalTree_OneDimensional_Helpers_And_Comparer()
        {
            var defaultValue = new Lazy<int>(() => int.MinValue);
            var oneD = new OneDimentionalIntervalTree<int>(defaultValue);

            Assert.IsFalse(oneD.DoOverlap(new OneDimentionalInterval<int>(1, 2, defaultValue)));
            Assert.IsNull(oneD.GetOverlap(new OneDimentionalInterval<int>(1, 2, defaultValue)));

            oneD.Insert(new OneDimentionalInterval<int>(10, 20, defaultValue));
            oneD.Insert(new OneDimentionalInterval<int>(15, 25, defaultValue));
            oneD.Insert(new OneDimentionalInterval<int>(30, 40, defaultValue));
            oneD.Insert(new OneDimentionalInterval<int>(10, 12, defaultValue));

            Assert.IsTrue(oneD.DoOverlap(new OneDimentionalInterval<int>(18, 19, defaultValue)));
            Assert.IsNotNull(oneD.GetOverlap(new OneDimentionalInterval<int>(18, 19, defaultValue)));
            Assert.IsFalse(oneD.DoOverlap(new OneDimentionalInterval<int>(100, 110, defaultValue)));
            Assert.AreEqual(2, oneD.GetOverlaps(new OneDimentionalInterval<int>(10, 22, defaultValue)).Count);

            oneD.Delete(new OneDimentionalInterval<int>(10, 12, defaultValue));
            oneD.Delete(new OneDimentionalInterval<int>(10, 20, defaultValue));
            Assert.ThrowsException<ArgumentException>(() =>
                oneD.Delete(new OneDimentionalInterval<int>(99, 100, defaultValue)));

            var a = new OneDimentionalInterval<int>(1, 2, defaultValue);
            var b = new OneDimentionalInterval<int>(1, 3, defaultValue);
            var c = new OneDimentionalInterval<int>(2, 3, defaultValue);

            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);
            Assert.IsTrue(a < c);
            Assert.IsTrue(c > a);
            Assert.IsTrue(a <= b);
            Assert.IsTrue(c >= a);
            Assert.IsTrue(a.Equals(b));
            Assert.IsFalse(a.Equals("x"));
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());
            Assert.IsFalse(a == null);
            Assert.IsFalse(null == a);
            Assert.IsTrue((OneDimentionalInterval<int>)null == null);

            var comparer = new IntervalComparer<int>();
            var t1 = Tuple.Create(new[] { 1 }, new[] { 2 });
            var t2 = Tuple.Create(new[] { 1 }, new[] { 2 });
            var t3 = Tuple.Create(new[] { 1 }, new[] { 3 });
            Assert.IsTrue(comparer.Equals(t1, t1));
            Assert.IsTrue(comparer.Equals(t1, t2));
            Assert.IsFalse(comparer.Equals(t1, t3));
            Assert.AreEqual(0, comparer.GetHashCode(null));
            Assert.AreEqual(comparer.GetHashCode(t1), comparer.GetHashCode(t2));
        }

        [TestMethod]
        public void IntervalTree_MultiEnd_MaxEnd_Search()
        {
            var tree = new IntervalTree<int>(1);

            // force both left/right children and MaxEnd updates during insert/delete
            tree.Insert(new[] { 50 }, new[] { 55 });
            tree.Insert(new[] { 10 }, new[] { 40 });
            tree.Insert(new[] { 70 }, new[] { 80 });
            tree.Insert(new[] { 20 }, new[] { 25 });
            tree.Insert(new[] { 60 }, new[] { 65 });
            tree.Insert(new[] { 10 }, new[] { 15 });

            Assert.IsTrue(tree.DoOverlap(new[] { 22 }, new[] { 23 }));
            Assert.IsTrue(tree.DoOverlap(new[] { 12 }, new[] { 14 }));
            Assert.IsFalse(tree.DoOverlap(new[] { 90 }, new[] { 95 }));

            // search that must consult left MaxEnd before finding an overlap
            Assert.IsTrue(tree.DoOverlap(new[] { 35 }, new[] { 36 }));

            tree.Delete(new[] { 10 }, new[] { 40 });
            Assert.IsFalse(tree.DoOverlap(new[] { 35 }, new[] { 36 }));
            Assert.IsTrue(tree.DoOverlap(new[] { 12 }, new[] { 14 }));
            tree.Delete(new[] { 10 }, new[] { 15 });
            Assert.IsFalse(tree.DoOverlap(new[] { 12 }, new[] { 14 }));
            Assert.AreEqual(4, tree.Count);
        }
    }
}