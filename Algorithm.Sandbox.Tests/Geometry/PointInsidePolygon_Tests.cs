using Algorithm.Sandbox.Geometry;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.Tests.Geometry
{
    /// <summary>
    /// Problem details below
    /// http://www.geeksforgeeks.org/how-to-check-if-a-given-point-lies-inside-a-polygon/
    /// </summary>
    [TestClass]
    public class PointInsidePolygon_Tests
    {
        //[TestMethod]
        public void Smoke_Test()
        {
            var polygon = new List<int[]>() {
                new int[] { 0, 0 },
                new int[] { 10, 0 },
                new int[] { 10, 10 },
                new int[] { 0, 10 }
            };

            var testPoint = new int[] { 20, 20 };

            Assert.IsFalse(PointInsidePolygon.IsInside(polygon, testPoint));

            testPoint = new int[] { 5, 5 };
            Assert.IsTrue(PointInsidePolygon.IsInside(polygon, testPoint));
        }
    }
}
