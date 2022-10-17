using System.Collections.Generic;
using Advanced.Algorithms.Geometry;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.Geometry
{
    [TestClass]
    public class ConvexHull_Tests
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
    }
}