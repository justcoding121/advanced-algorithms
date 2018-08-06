using Advanced.Algorithms.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Advanced.Algorithms.Tests.DataStructures
{
    [TestClass]
    public class IntervalTree_Tests
    {
        /// </summary>
        [TestMethod]
        public void IntervalTree_1D_Smoke_Test()
        {
            var tree = new IntervalTree<int>(1);

            tree.Insert(new int[] { 1 }, new int[] { 2 });
            tree.Insert(new int[] { 3 }, new int[] { 4 });
            tree.Insert(new int[] { 5 }, new int[] { 6 });
            tree.Insert(new int[] { 7 }, new int[] { 8 });
            tree.Insert(new int[] { 9 }, new int[] { 10 });
            tree.Insert(new int[] { 11 }, new int[] { 12 });

            //IEnumerable test
            Assert.AreEqual(tree.Count, tree.Count());

            Assert.AreEqual(tree.Count, 6);

            Assert.IsTrue(tree.DoOverlap(new int[] { 1 }, new int[] { 10 }));
            tree.Delete(new int[] { 1 }, new int[] { 2 });
            Assert.IsFalse(tree.DoOverlap(new int[] { 1 }, new int[] { 2 }));

            tree.Delete(new int[] { 3 }, new int[] { 4 });
            tree.Delete(new int[] { 5 }, new int[] { 6 });
            tree.Delete(new int[] { 7 }, new int[] { 8 });
            tree.Delete(new int[] { 9 }, new int[] { 10 });

            //IEnumerable test
            Assert.AreEqual(tree.Count, tree.Count());

            Assert.IsTrue(tree.DoOverlap(new int[] { 11 }, new int[] { 12 }));
            tree.Delete(new int[] { 11 }, new int[] { 12 });
            Assert.IsFalse(tree.DoOverlap(new int[] { 11 }, new int[] { 12 }));


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

            for (int i = 0; i < nodeCount; i++)
            {
                var start = i - 1000 + rnd.Next(1, 10);
                var interval = new Tuple<int[], int[]>(new int[] { start }, new int[] { start + rnd.Next(1, 10) });
                intervals.Add(interval);
            }

            foreach(var interval in intervals)
            {
                tree.Insert(interval.Item1, interval.Item2);
            }

            //IEnumerable test
            Assert.AreEqual(tree.Count, tree.Count());

            foreach(var interval in intervals)
            {
                Assert.IsTrue(tree.DoOverlap(interval.Item1,
                                                       interval.Item2));
                var testStart = cloneArray(interval.Item1);
                var testEnd = cloneArray(interval.Item2);

                testStart[0] = testStart[0] - rnd.Next(1, 5);
                testEnd[0] = testEnd[0] + rnd.Next(1, 5);

                Assert.IsTrue(tree.DoOverlap(testStart,
                        testEnd));
            }

            //IEnumerable test
            Assert.AreEqual(tree.Count, tree.Count());

            var notDeleted = new HashSet<Tuple<int[], int[]>>(new IntervalComparer<int>());

            foreach (var interval in intervals)
            {
                notDeleted.Add(interval);
            }

            foreach (var interval in intervals)
            {
                tree.Delete(interval.Item1, interval.Item2);
                notDeleted.Remove(interval);

                foreach (var existingInterval in notDeleted)
                {
                    var testStart = cloneArray(existingInterval.Item1);
                    var testEnd = cloneArray(existingInterval.Item2);

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

            for (int i = 0; i < nodeCount; i++)
            {
                var startx = i - 1000 + rnd.Next(1, 10);
                var starty = i + 15 + rnd.Next(1, 10);
                //(x1,y1) and (x2, y2)
                var interval = new Tuple<int[], int[]> (new int[dimension] { startx, starty },
                    new int[dimension] { startx + rnd.Next(1, 10), starty + rnd.Next(1, 10) });

                if(intervals.Add(interval))
                {
                    tree.Insert(interval.Item1, interval.Item2);
                }
               
            }

            foreach (var interval in intervals)
            {
                Assert.IsTrue(tree.DoOverlap(interval.Item1,
                                                   interval.Item2));

                var testStart = cloneArray(interval.Item1);
                var testEnd = cloneArray(interval.Item2);

                testStart[0] = testStart[0] - rnd.Next(1, 5);
                testStart[1] = testStart[1] - rnd.Next(1, 5);

                testEnd[0] = testEnd[0] + rnd.Next(1, 5);
                testEnd[1] = testEnd[1] + rnd.Next(1, 5);

                Assert.IsTrue(tree.DoOverlap(testStart, testEnd));
            }

            var notDeleted = new HashSet<Tuple<int[], int[]>>(new IntervalComparer<int>());

            foreach(var interval in intervals)
            {
                notDeleted.Add(interval);
            }

            foreach (var interval in intervals)
            {
                tree.Delete(interval.Item1, interval.Item2);
                notDeleted.Remove(interval);

                foreach (var existingInterval in notDeleted)
                {
                    var testStart = cloneArray(existingInterval.Item1);
                    var testEnd = cloneArray(existingInterval.Item2);

                    Assert.IsTrue(tree.DoOverlap(testStart, testEnd));
                }
            }
        }

        private int[] cloneArray(int[] array)
        {
            var newArray = new int[array.Length];

            for (int i = 0; i < array.Length; i++)
            {
                newArray[i] = array[i];
            }

            return newArray;
        }


        private int[][] cloneArray(int[][] array)
        {
            var newArray = new int[array.Length][];

            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array.Length; j++)

                {
                    newArray[i][j] = array[i][j];
                }

            }

            return newArray;
        }
    }
}
