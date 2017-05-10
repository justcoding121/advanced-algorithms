using System;
using Algorithm.Sandbox.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Algorithm.Sandbox.Tests.DataStructures.Tree
{
    [TestClass]
    public class KdTree_Tests
    {
        /// <summary>
        /// A tree test
        /// </summary>
        [TestMethod]
        public void KdTree2D_Smoke_Test()
        {
            var distanceCalculator = new DistanceCalculator2D();

            var testPts = new List<int[]> { new int[2]{ 3, 6 }, new int[2] { 17, 15 },
                new int[2] { 13, 15 }, new int[2] { 6, 12 }, new int[2] { 9, 1 },
                new int[2] { 2, 7 }, new int[2] { 10, 19 } };

            var tree = new AsKDTree<int>(2);

            foreach (var pt in testPts)
            {
                tree.Insert(pt);
            }

            int j = testPts.Count - 1;
            while (testPts.Count > 0)
            {
                var testPoint = new int[] { 10, 20 };
                var nearestNeigbour = tree.FindNearestNeighbour(new DistanceCalculator2D(), testPoint);
                var actualNeigbour = GetActualNearestNeighbour(testPts, testPoint);
                Assert.IsTrue(distanceCalculator.Compare(actualNeigbour, nearestNeigbour, testPoint) == 0);

                tree.Delete(testPts[j]);
                testPts.RemoveAt(j);
                j--;
            }


        }

        /// <summary>
        /// A tree test
        /// </summary>
        [TestMethod]
        public void KdTree2D_Accuracy_Test()
        {
            var distanceCalculator = new DistanceCalculator2D();

            int nodeCount = 1000;
            var rnd = new Random();

            var testPts = new List<int[]>();

            for (int i = 0; i < nodeCount; i++)
            {
                var start = i + rnd.Next(int.MinValue, int.MaxValue - 100);
                var end = start + rnd.Next(1, 10);

                testPts.Add(new int[] { start, end });
            }

            var tree = new AsKDTree<int>(2);

            foreach (var pt in testPts)
            {
                tree.Insert(pt);
            }

            int j = testPts.Count - 1;

            while (testPts.Count > 0)
            {
                var testPoint = new int[] { rnd.Next(), rnd.Next() };

                var nearestNeigbour = tree.FindNearestNeighbour(distanceCalculator, testPoint);
                var actualNeigbour = GetActualNearestNeighbour(testPts, testPoint);

                Assert.IsTrue(distanceCalculator.Compare(actualNeigbour, nearestNeigbour, testPoint) == 0);

                tree.Delete(testPts[j]);
                testPts.RemoveAt(j);
                j--;
            }

        }

        /// <summary>
        /// A tree test
        /// </summary>
        [TestMethod]
        public void KdTree2D_Range_Smoke_Test()
        {
            var testPts = new List<int[]> { new int[2]{ 1, 1 }, new int[2] { 10, 10 },
                new int[2] { 1, 6 }, new int[2] { 6, 7 }, new int[2] { 9, 1 },
                new int[2] { 2, 7 }, new int[2] { 20, 19 } };

            var tree = new AsKDTree<int>(2);

            foreach (var pt in testPts)
            {
                tree.Insert(pt);
            }

            var rangePoints = tree.GetInRange(new int[2] { 1, 1 }, new int[2] { 9, 9 });
        }

        /// <summary>
        /// A tree test
        /// </summary>
        [TestMethod]
        public void KdTree2D_Range_Accuracy_Test()
        {
            var distanceCalculator = new DistanceCalculator2D();

            int nodeCount = 1000;
            var rnd = new Random();

            var testPts = new List<int[]>();

            for (int i = 0; i < nodeCount; i++)
            {
                var start = i + rnd.Next(int.MinValue, int.MaxValue - 100);
                var end = start + rnd.Next(1, 10);

                testPts.Add(new int[] { start, end });
            }

            var tree = new AsKDTree<int>(2);

            foreach (var pt in testPts)
            {
                tree.Insert(pt);
            }


            int j = testPts.Count - 1;

            while (testPts.Count > 0)
            {
                var testStartRange = new int[] { rnd.Next(), rnd.Next() };
                var testEndRange = new int[] { rnd.Next() + 100, rnd.Next() + 200 };

                var correctResult = new List<int[]>();
                for (int i = 0; i < testPts.Count; i++)
                {
                    if (InRange(testPts[i], testStartRange, testEndRange))
                    {
                        correctResult.Add(testPts[i]);
                    }
                }

                var actualResult = tree.GetInRange(testStartRange, testEndRange);

                Assert.IsTrue(correctResult.Count == actualResult.Count);

                tree.Delete(testPts[j]);
                testPts.RemoveAt(j);
                j--;
            }


        }
        /// <summary>
        /// gets the actual nearest neighbour by brute force search
        /// </summary>
        /// <param name="points"></param>
        /// <param name="testPoint"></param>
        /// <returns></returns>
        private int[] GetActualNearestNeighbour(List<int[]> points, int[] testPoint)
        {
            int[] currentMinNode = null;
            double currentMin = double.MaxValue;
            var distanceCalculator = new DistanceCalculator2D();

            for (int i = 0; i < points.Count; i++)
            {
                var currentDistance = distanceCalculator.GetEucledianDistance(points[i], testPoint);

                if (currentMin > currentDistance)
                {
                    currentMin = currentDistance;
                    currentMinNode = points[i];
                }
            }

            return currentMinNode;
        }

        /// <summary>
        ///  point is within start & end points?
        /// </summary>
        /// <param name="node"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private bool InRange(int[] point, int[] start, int[] end)
        {
            for (int i = 0; i < point.Length; i++)
            {
                //if not (start is less than node && end is greater than node)
                if (!(start[i].CompareTo(point[i]) <= 0
                    && end[i].CompareTo(point[i]) >= 0))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Distance Calculator for 2 dimensional points
        /// </summary>
        public class DistanceCalculator2D : IDistanceCalculator<int>
        {
            /// <summary>
            /// compares distance between a to point and b to point 
            /// returns IComparer result
            /// </summary>
            /// <param name="a"></param>
            /// <param name="b"></param>
            /// <param name="point"></param>
            /// <returns></returns>
            public int Compare(int[] a, int[] b, int[] point)
            {
                var distance1 = GetEucledianDistance(a, point);
                var distance2 = GetEucledianDistance(b, point);

                return distance1 == distance2 ? 0 : distance1 < distance2 ? -1 : 1;

            }

            /// <summary>
            /// compares distance between a to b and start to end
            /// returns IComparer result
            /// </summary>
            /// <param name="a"></param>
            /// <param name="b"></param>
            /// <param name="point"></param>
            /// <returns></returns>
            public int Compare(int a, int b, int[] start, int[] end)
            {
                var distance1 = GetDistance(a, b);
                var distance2 = GetEucledianDistance(start, end);

                return distance1 == distance2 ? 0 : distance1 < distance2 ? -1 : 1;

            }

            /// <summary>
            /// returns the eucledian distance between given points
            /// </summary>
            /// <param name="a"></param>
            /// <param name="b"></param>
            /// <returns></returns>
            public double GetEucledianDistance(int[] a, int[] b)
            {
                return Math.Sqrt(Math.Pow(Math.Abs(a[0] - b[0]), 2)
                    + Math.Pow(Math.Abs(a[1] - b[1]), 2));
            }

            /// <summary>
            /// returns the absolute distance between given points
            /// </summary>
            /// <param name="a"></param>
            /// <param name="b"></param>
            /// <returns></returns>
            public int GetDistance(int a, int b)
            {
                return Math.Abs(a - b);
            }
        }


    }
}
