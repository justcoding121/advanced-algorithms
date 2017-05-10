using Algorithm.Sandbox.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;

namespace Algorithm.Sandbox.Tests.DataStructures.Tree
{
    [TestClass]
    public class IntervalTree_Tests
    {
        /// </summary>
        [TestMethod]
        public void IntervalTree_1D_Smoke_Test()
        {
            var intTree = new AsDIntervalTree<int>(1);

            intTree.Insert(new int[] { 1 }, new int[] { 2 });
            intTree.Insert(new int[] { 3 }, new int[] { 4 });
            intTree.Insert(new int[] { 5 }, new int[] { 6 });
            intTree.Insert(new int[] { 7 }, new int[] { 8 });
            intTree.Insert(new int[] { 9 }, new int[] { 10 });
            intTree.Insert(new int[] { 11 }, new int[] { 12 });

            Assert.AreEqual(intTree.Count, 6);

            Assert.IsTrue(intTree.DoOverlap(new int[] { 1 }, new int[] { 10 }));
            intTree.Delete(new int[] { 1 }, new int[] { 2 });
            Assert.IsFalse(intTree.DoOverlap(new int[] { 1 }, new int[] { 2 }));

            intTree.Delete(new int[] { 3 }, new int[] { 4 });
            intTree.Delete(new int[] { 5 }, new int[] { 6 });
            intTree.Delete(new int[] { 7 }, new int[] { 8 });
            intTree.Delete(new int[] { 9 }, new int[] { 10 });

            Assert.IsTrue(intTree.DoOverlap(new int[] { 11 }, new int[] { 12 }));
            intTree.Delete(new int[] { 11 }, new int[] { 12 });
            Assert.IsFalse(intTree.DoOverlap(new int[] { 11 }, new int[] { 12 }));


            Assert.AreEqual(intTree.Count, 0);
        }

        /// </summary>
        [TestMethod]
        public void IntervalTree_1D_Accuracy_Test()
        {
            var nodeCount = 100;
            var intTree = new AsDIntervalTree<int>(1);

            var rnd = new Random();
            var intervals = new List<AsDInterval<int>>();

            for (int i = 0; i < nodeCount; i++)
            {
                var start = i - 1000 + rnd.Next(1, 10);
                var interval = new AsDInterval<int>(new int[] { start }, new int[] { start + rnd.Next(1, 10) });
                intervals.Add(interval);
                intTree.Insert(interval.Start, interval.End);

                for (int j = i; j >= 0; j--)
                {
                    Assert.IsTrue(intTree.DoOverlap(intervals[j].Start,
                                                      intervals[j].End));

                    var testStart = cloneArray(intervals[j].Start);
                    var testEnd = cloneArray(intervals[j].End);

                    testStart[0] = testStart[0] - rnd.Next(1, 5);
                    testEnd[0] = testEnd[0] + rnd.Next(1, 5);

                    Assert.IsTrue(intTree.DoOverlap(testStart,
                            testEnd));
                }
            }

            for (int i = 0; i < nodeCount; i++)
            {
                Assert.IsTrue(intTree.DoOverlap(intervals[i].Start,
                                                       intervals[i].End));
                var testStart = cloneArray(intervals[i].Start);
                var testEnd = cloneArray(intervals[i].End);

                testStart[0] = testStart[0] - rnd.Next(1, 5);
                testEnd[0] = testEnd[0] + rnd.Next(1, 5);

                Assert.IsTrue(intTree.DoOverlap(testStart,
                        testEnd));
            }


            for (int i = 0; i < intervals.Count; i++)
            {
                intTree.Delete(intervals[i].Start, intervals[i].End);

                for (int j = i + 1; j < nodeCount; j++)
                {
                    Assert.IsTrue(intTree.DoOverlap(intervals[j].Start,
                        intervals[j].End));
                }

            }
        }

        /// </summary>
        [TestMethod]
        public void IntervalTree_1D_Stress_Test()
        {
            var nodeCount = 1000;
            var intTree = new AsDIntervalTree<int>(1);

            var rnd = new Random();
            var intervals = new List<AsDInterval<int>>();

            for (int i = 0; i < nodeCount; i++)
            {
                var start = i - 1000 + rnd.Next(1, 10);
                var interval = new AsDInterval<int>(new int[] { start }, new int[] { start + rnd.Next(1, 10) });
                intervals.Add(interval);
                intTree.Insert(interval.Start, interval.End);

            }

            for (int i = 0; i < nodeCount; i++)
            {
                Assert.IsTrue(intTree.DoOverlap(intervals[i].Start,
                                                       intervals[i].End));             
            }

            for (int i = 0; i < intervals.Count; i++)
            {
                intTree.Delete(intervals[i].Start, intervals[i].End);
               
            }
        }

        /// </summary>
        [TestMethod]
        public void IntervalTree_2D_Accuracy_Test()
        {
            var nodeCount = 100;
            const int dimension = 2;

            var intTree = new AsDIntervalTree<int>(dimension);

            var rnd = new Random();
            var intervals = new List<AsDInterval<int>>();

            for (int i = 0; i < nodeCount; i++)
            {
                var startx = i - 1000 + rnd.Next(1, 10);
                var starty = i + 15 + rnd.Next(1, 10);
                //(x1,y1) and (x2, y2)
                var interval = new AsDInterval<int>(new int[dimension] { startx, starty },
                    new int[dimension] { startx + rnd.Next(1, 10), starty + rnd.Next(1, 10) });

                intervals.Add(interval);
                intTree.Insert(interval.Start, interval.End);

                for (int j = i; j >= 0; j--)
                {
                    var testStart =  cloneArray(intervals[j].Start);
                    var testEnd = cloneArray(intervals[j].End);

                    testStart[0] = testStart[0] - rnd.Next(1, 5);
                    testStart[1] = testStart[1] - rnd.Next(1, 5);

                    testEnd[0] = testEnd[0] + rnd.Next(1, 5);
                    testEnd[1] = testEnd[1] + rnd.Next(1, 5);

                    Assert.IsTrue(intTree.DoOverlap(testStart,
                          testEnd));
                }
            }

            for (int i = 0; i < nodeCount; i++)
            {
                Assert.IsTrue(intTree.DoOverlap(intervals[i].Start,
                                                   intervals[i].End));

                var testStart = cloneArray(intervals[i].Start);
                var testEnd = cloneArray(intervals[i].End);

                testStart[0] = testStart[0] - rnd.Next(1, 5);
                testStart[1] = testStart[1] - rnd.Next(1, 5);

                testEnd[0] = testEnd[0] + rnd.Next(1, 5);
                testEnd[1] = testEnd[1] + rnd.Next(1, 5);

                Assert.IsTrue(intTree.DoOverlap(testStart,
                     testEnd));
            }


            for (int i = 0; i < intervals.Count; i++)
            {
                intTree.Delete(intervals[i].Start, intervals[i].End);

                for (int j = i + 1; j < nodeCount; j++)
                {
                    var testStart = cloneArray(intervals[j].Start);
                    var testEnd = cloneArray(intervals[j].End);

                    Assert.IsTrue(intTree.DoOverlap(testStart,
                        testEnd));
                }

            }
        }

        /// </summary>
        [TestMethod]
        public void IntervalTree_2D_Stress_Test()
        {
            var nodeCount = 1000;
            const int dimension = 2;

            var intTree = new AsDIntervalTree<int>(dimension);

            var rnd = new Random();
            var intervals = new List<AsDInterval<int>>();

            for (int i = 0; i < nodeCount; i++)
            {
                var startx = i - 1000 + rnd.Next(1, 10);
                var starty = i + 15 + rnd.Next(1, 10);
                //(x1,y1) and (x2, y2)
                var interval = new AsDInterval<int>(new int[dimension] { startx, starty },
                    new int[dimension] { startx + rnd.Next(1, 10), starty + rnd.Next(1, 10) });

                intervals.Add(interval);
                intTree.Insert(interval.Start, interval.End);
               
            }

            for (int i = 0; i < nodeCount; i++)
            {
                Assert.IsTrue(intTree.DoOverlap(intervals[i].Start,
                                                   intervals[i].End));

            }


            for (int i = 0; i < intervals.Count; i++)
            {
                intTree.Delete(intervals[i].Start, intervals[i].End);
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
