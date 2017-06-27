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
    public class LineIntersection_Tests
    {
        //[TestMethod]
        public void SmokeTest()
        {
            var line1StartPt = new int[] { 1, 1};
            var line1EndPt = new int[] { 10, 1};

            var line2StartPt = new int[] { 1, 2};
            var line2EndPt = new int[] {10, 2};

            Assert.IsFalse(LineIntersection.DoIntersect(line1StartPt, line1EndPt, line2StartPt, line2EndPt));

            line1StartPt = new int[] { 10, 0 };
            line1EndPt = new int[] { 0, 10 };

            line2StartPt = new int[] { 0, 0 };
            line2EndPt = new int[] { 10, 10 };

            Assert.IsFalse(LineIntersection.DoIntersect(line1StartPt, line1EndPt, line2StartPt, line2EndPt));
        }
    }
}
