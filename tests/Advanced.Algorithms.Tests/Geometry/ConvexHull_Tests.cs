using System.Collections.Generic;
using System.Linq;
using Advanced.Algorithms.Geometry;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.Geometry
{
    [TestClass]
    public class ConvexHullTests
    {
        [TestMethod]
        public void ConvexHull_Smoke_Test()
        {
            var testPoints = new List<int[]>
            {
                new[] { 0, 3 },
                new[] { 2, 2 },
                new[] { 1, 1 },
                new[] { 2, 1 },
                new[] { 3, 0 },
                new[] { 0, 0 },
                new[] { 3, 3 }
            };

            var result = ConvexHull.Find(testPoints);

            Assert.AreEqual(4, result.Count);
        }

        /// <summary>
        /// Oracle: known polygons / grids; hull vertices match extreme corners (no edge midpoints).
        /// </summary>
        [TestMethod]
        public void ConvexHull_Oracle_KnownPolygons_Test()
        {
            Assert.AreEqual(0, ConvexHull.Find(new List<int[]>()).Count);

            var triangle = ConvexHull.Find(new List<int[]>
            {
                new[] { 0, 0 }, new[] { 5, 0 }, new[] { 2, 4 }, new[] { 2, 1 }
            });
            Assert.AreEqual(3, triangle.Count);

            var grid = ConvexHull.Find(new List<int[]>
            {
                new[] { 0, 0 }, new[] { 1, 0 }, new[] { 2, 0 },
                new[] { 0, 1 }, new[] { 1, 1 }, new[] { 2, 1 },
                new[] { 0, 2 }, new[] { 1, 2 }, new[] { 2, 2 }
            });
            Assert.AreEqual(4, grid.Count);
            CollectionAssert.AreEquivalent(
                new[] { "0,0", "2,0", "2,2", "0,2" },
                grid.Select(p => p[0] + "," + p[1]).ToArray());

            var collinear = ConvexHull.Find(new List<int[]>
            {
                new[] { 0, 0 }, new[] { 1, 0 }, new[] { 2, 0 }, new[] { 3, 0 }
            });
            Assert.AreEqual(2, collinear.Count);
            CollectionAssert.AreEquivalent(
                new[] { "0,0", "3,0" },
                collinear.Select(p => p[0] + "," + p[1]).ToArray());
        }
    }
}