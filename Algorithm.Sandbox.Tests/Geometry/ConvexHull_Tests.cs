using Algorithm.Sandbox.Geometry;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.Tests.Geometry
{
    [TestClass]
    public class ConvexHull_Tests
    {
        //[TestMethod]
        public void SmokeTest()
        {
            var testPoints = new List<int[]>()
            {
                new int[]{ 0, 3},
                new int[]{ 2, 2},
                new int[]{ 1, 1},
                new int[]{ 2, 1},
                new int[]{ 3, 0},
                new int[]{ 0, 0},
                new int[]{ 3, 3}
            };

            var result = ConvexHull.Find(testPoints);

            Assert.AreEqual(4, result.Count);
        }
    }
}
