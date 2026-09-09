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
    }
}