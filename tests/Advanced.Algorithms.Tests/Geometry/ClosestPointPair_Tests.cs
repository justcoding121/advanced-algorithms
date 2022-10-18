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

            Assert.AreEqual(1.414, ClosestPointPair.Find(testPoints), 3);
        }
    }
}