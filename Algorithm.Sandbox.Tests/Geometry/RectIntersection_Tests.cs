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
    /// http://www.geeksforgeeks.org/find-two-rectangles-overlap/
    /// </summary>
    [TestClass]
    public class RectIntersection_Tests
    {
        //[TestMethod]
        public void RectIntersection_Smoke_Test()
        {
            int[] l1 = new int[] { 0, 10 }, r1 = new int[] { 10, 0 };
            int[] l2 = new int[]{ 5, 5 }, r2 = new int[]{ 15, 0 };

            Assert.IsTrue(RectIntersection.DoIntersect(l1, r1, l2, r2));
        }
    }
}
