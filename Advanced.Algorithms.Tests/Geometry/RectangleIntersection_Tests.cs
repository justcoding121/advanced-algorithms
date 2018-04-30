using Advanced.Algorithms.Geometry;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced.Algorithms.Tests.Geometry
{

    [TestClass]
    public class RectangleIntersection_Tests
    {
        [TestMethod]
        public void RectIntersection_Smoke_Test()
        {
            var result = RectangleIntersection.FindIntersection(new Rectangle()
            {
                LeftTopCorner = new Point() { x = 0, y = 10 },
                RightBottomCorner = new Point() { x = 10, y = 0 }
            },
            new Rectangle()
            {
                LeftTopCorner = new Point() { x = 5, y = 5 },
                RightBottomCorner = new Point() { x = 15, y = 0 }
            });

            Assert.AreEqual(result, new Rectangle()
            {
                LeftTopCorner = new Point() { x = 5, y = 5 },
                RightBottomCorner = new Point() { x = 10, y = 0 }
            });

            result = RectangleIntersection.FindIntersection(new Rectangle()
            {
                LeftTopCorner = new Point() { x = 0, y = 10 },
                RightBottomCorner = new Point() { x = 4, y = 0 }
            },
            new Rectangle()
            {
               LeftTopCorner = new Point() { x = 5, y = 5 },
               RightBottomCorner = new Point() { x = 15, y = 0 }
            });

            Assert.AreEqual(result, default(Rectangle));
        }

    }
}
