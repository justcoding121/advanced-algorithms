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
        [TestMethod]
        public void RectIntersection_Smoke_Test()
        {
            var result = RectIntersection.FindIntersection(new Rectangle()
            {
                leftCorner = new Point() { x = 0, y = 10 },
                rightCorner = new Point() { x = 10, y = 0 }
            },
            new Rectangle()
            {
                leftCorner = new Point() { x = 5, y = 5 },
                rightCorner = new Point() { x = 15, y = 0 }
            });

            Assert.AreEqual(result, new Rectangle()
            {
                leftCorner = new Point() { x = 5, y = 5 },
                rightCorner = new Point() { x = 10, y = 0 }
            });

            result = RectIntersection.FindIntersection(new Rectangle()
            {
                leftCorner = new Point() { x = 0, y = 10 },
                rightCorner = new Point() { x = 4, y = 0 }
            },
            new Rectangle()
            {
               leftCorner = new Point() { x = 5, y = 5 },
               rightCorner = new Point() { x = 15, y = 0 }
            });

            Assert.AreEqual(result, default(Rectangle));
        }

    }
}
