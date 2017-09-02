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
    /// http://www.geeksforgeeks.org/closest-pair-of-points/
    /// </summary>
    [TestClass]
    public class ClosestPointPair_Tests
    {
        //[TestMethod]
        public void ClosestPointPair_Smoke_Test()
        {
            var testPoints = new List<int[]>()
            {
                new int[]{ 2, 3},
                new int[]{ 12, 30},
                new int[]{ 40, 50},
                new int[]{ 5, 1},
                new int[]{ 12, 10},
                new int[]{ 3, 4}
            };

            Assert.AreEqual(1.414214, ClosestPointPair.Find(testPoints));
        }
    }
}
