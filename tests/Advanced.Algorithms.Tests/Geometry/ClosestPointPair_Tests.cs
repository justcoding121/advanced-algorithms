using System.Collections.Generic;
using Advanced.Algorithms.Geometry;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.Geometry
{
    [TestClass]
    public class ClosestPointPairTests
    {
        [TestMethod]
        public void ClosestPointPair_Smoke_Test()
        {
            var testPoints = new List<int[]>
            {
                new[] { 2, 3 },
                new[] { 12, 30 },
                new[] { 40, 50 },
                new[] { 5, 1 },
                new[] { 12, 10 },
                new[] { 3, 4 }
            };

            Assert.AreEqual(1.414, ClosestPointPair.Find(testPoints), 0.001);
        }

        [TestMethod]
        public void ClosestPointPair_Corner_Cases_Test()
        {
            Assert.AreEqual(5, ClosestPointPair.Find(new List<int[]>
            {
                new[] { 0, 0 },
                new[] { 3, 4 }
            }), 0.001);

            Assert.AreEqual(1, ClosestPointPair.Find(new List<int[]>
            {
                new[] { 0, 0 },
                new[] { 0, 1 },
                new[] { 10, 10 }
            }), 0.001);

            Assert.AreEqual(1, ClosestPointPair.Find(new List<int[]>
            {
                new[] { 0, 0 },
                new[] { 5, 0 },
                new[] { 10, 0 },
                new[] { 0, 5 },
                new[] { 5, 5 },
                new[] { 10, 5 },
                new[] { 0, 10 },
                new[] { 5, 10 },
                new[] { 10, 10 },
                new[] { 6, 5 }
            }), 0.001);
        }

        /// <summary>
        /// Oracle: divide-and-conquer vs O(n^2) brute force on small random sets.
        /// </summary>
        [TestMethod]
        public void ClosestPointPair_Oracle_BruteForce_Test()
        {
            double Brute(List<int[]> pts)
            {
                var min = double.MaxValue;
                for (var i = 0; i < pts.Count; i++)
                for (var j = i + 1; j < pts.Count; j++)
                {
                    double dx = pts[i][0] - pts[j][0];
                    double dy = pts[i][1] - pts[j][1];
                    min = System.Math.Min(min, System.Math.Sqrt(dx * dx + dy * dy));
                }

                return min;
            }

            var rnd = new System.Random(42);
            for (var n = 2; n <= 40; n++)
            {
                var pts = new List<int[]>();
                for (var i = 0; i < n; i++)
                    pts.Add(new[] { rnd.Next(0, 100), rnd.Next(0, 100) });

                Assert.AreEqual(Brute(pts), ClosestPointPair.Find(pts), 1e-6, "n=" + n);
            }

            Assert.AreEqual(0, ClosestPointPair.Find(new List<int[]>
            {
                new[] { 1, 1 }, new[] { 1, 1 }, new[] { 5, 5 }
            }), 1e-9);
        }
    }
}