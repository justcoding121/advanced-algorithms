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
        [TestMethod]
        public void LineIntersection_Smoke_Test()
        {
            var line1 = new Line { x1 = 1, y1 = 1, x2 = 10, y2 = 1 };
            var line2 = new Line { x1 = 1, y1 = 2, x2 = 10, y2 = 2 };

            Assert.AreEqual(default(Point), LineIntersection.FindIntersection(line1, line2));

            line1 = new Line { x1 = 10, y1 = 0, x2 = 0, y2 = 10 };
            line2 = new Line { x1 = 0, y1 = 10, x2 = 10, y2 = 10 };

            Assert.AreEqual(new Point() { x = 0, y = 10 }, LineIntersection.FindIntersection(line1, line2));


            line1 = new Line { x1 = 0, y1 = 0, x2 = 10, y2 = 10 };
            line2 = new Line { x1 = 0, y1 = 10, x2 = 10, y2 = 10 };

            Assert.AreEqual(new Point() { x = 10, y = 10 }, LineIntersection.FindIntersection(line1, line2));

            line1 = new Line { x1 = 10, y1 = 0, x2 = 0, y2 = 10 };
            line2 = new Line { x1 = 0, y1 = 0, x2 = 10, y2 = 10 };

            Assert.AreEqual(new Point() { x = 5, y = 5 }, LineIntersection.FindIntersection(line1, line2));

            line1 = new Line { x1 = -5, y1 = -5, x2 = 0, y2 = 0 };
            line2 = new Line { x1 = 1, y1 = 1, x2 = 10, y2 = 10 };

            Assert.AreEqual(default(Point), LineIntersection.FindIntersection(line1, line2));

            line1 = new Line { x1 = 3, y1 = -5, x2 =3, y2 = 10 };
            line2 = new Line { x1 = 0, y1 = 5, x2 = 10, y2 = 5 };

            Assert.AreEqual(new Point() { x = 3, y = 5 }, LineIntersection.FindIntersection(line1, line2));

            line1 = new Line { x1 = 0, y1 = 5, x2 = 10, y2 = 5 }; 
            line2 = new Line { x1 = 3, y1 = -5, x2 = 3, y2 = 10 };

            Assert.AreEqual(new Point() { x = 3, y = 5 }, LineIntersection.FindIntersection(line1, line2));


            line1 = new Line { x1 = 0, y1 = 5, x2 = 10, y2 = 5 };
            line2 = new Line { x1 = 3, y1 = -5, x2 = 5, y2 = 15 };

            Assert.AreEqual(new Point() { x = 4, y = 5 }, LineIntersection.FindIntersection(line1, line2));



            line1 = new Line { x1 = 0, y1 = -5, x2 = 0, y2 = 5 };
            line2 = new Line { x1 = -3, y1 = 0, x2 = 3, y2 = 0 };

            Assert.AreEqual(new Point() { x = 0, y = 0 }, LineIntersection.FindIntersection(line1, line2));
        }
    }
}
