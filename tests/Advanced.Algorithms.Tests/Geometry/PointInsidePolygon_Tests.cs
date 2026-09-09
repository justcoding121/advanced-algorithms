using System.Collections.Generic;
using Advanced.Algorithms.Geometry;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.Geometry
{
    [TestClass]
    public class PointInsidePolygonTests
    {
        [TestMethod]
        public void PointInsidePolygon_Smoke_Test()
        {
            var polygon = new Polygon(new List<Line>
            {
                new Line(new Point(0, 0), new Point(10, 10)),
                new Line(new Point(10, 10), new Point(11, 11)),
                new Line(new Point(11, 11), new Point(0, 10))
            });

            var testPoint = new Point(20, 20);

            Assert.IsFalse(PointInsidePolygon.IsInside(polygon, testPoint));

            testPoint = new Point(5, 5);
            Assert.IsTrue(PointInsidePolygon.IsInside(polygon, testPoint));
        }

        [TestMethod]
        public void PointInsidePolygon_Corner_Cases_Test()
        {
            var square = new Polygon(new List<Line>
            {
                new Line(new Point(0, 0), new Point(10, 0)),
                new Line(new Point(10, 0), new Point(10, 10)),
                new Line(new Point(10, 10), new Point(0, 10)),
                new Line(new Point(0, 10), new Point(0, 0))
            });

            Assert.IsTrue(PointInsidePolygon.IsInside(square, new Point(5, 5)));
            Assert.IsFalse(PointInsidePolygon.IsInside(square, new Point(20, 5)));
            Assert.IsFalse(PointInsidePolygon.IsInside(square, new Point(5, 20)));
        }
    }
}