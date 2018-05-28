using Advanced.Algorithms.Geometry;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.Geometry
{
    [TestClass]
    public class LineIntersection_Tests
    {
        [TestMethod]
        public void LineIntersection_Smoke_Test()
        {
            var line1 = new Line(new Point(1, 1), new Point(10, 1));
            var line2 = new Line(new Point(1, 2), new Point(10, 2));

            Assert.AreEqual(null, LineIntersection.FindIntersection(line1, line2));

            line1 = new Line(new Point(10, 0), new Point(0, 10));
            line2 = new Line(new Point(0, 10), new Point(10, 10));

            Assert.AreEqual(new Point(0, 10), LineIntersection.FindIntersection(line1, line2));

            line1 = new Line(new Point(0, 0), new Point(10, 10));
            line2 = new Line(new Point(0, 10), new Point(10, 10));

            Assert.AreEqual(new Point(10, 10), LineIntersection.FindIntersection(line1, line2));


            line1 = new Line(new Point(10, 0), new Point(0, 10));
            line2 = new Line(new Point(0, 0), new Point(10, 10));

            Assert.AreEqual(new Point(5, 5), LineIntersection.FindIntersection(line1, line2));

            line1 = new Line(new Point(-5, -5), new Point(0, 0));
            line2 = new Line(new Point(1, 1), new Point(10, 10));

            Assert.AreEqual(default(Point), LineIntersection.FindIntersection(line1, line2));

            line1 = new Line(new Point(3, -5), new Point(3, 10));
            line2 = new Line(new Point(0, 5), new Point(10, 5));

            Assert.AreEqual(new Point(3, 5), LineIntersection.FindIntersection(line1, line2));

            line1 = new Line(new Point(0, 5), new Point(10, 5));
            line2 = new Line(new Point(3, -5), new Point(3, 10));

            Assert.AreEqual(new Point(3, 5), LineIntersection.FindIntersection(line1, line2));

            line1 = new Line(new Point(0, 5), new Point(10, 5));
            line2 = new Line(new Point(3, -5), new Point(5, 15));

            Assert.AreEqual(new Point(4, 5), LineIntersection.FindIntersection(line1, line2));

            line1 = new Line(new Point(0, -5), new Point(0, 5));
            line2 = new Line(new Point(-3, 0), new Point(3, 0));

            Assert.AreEqual(new Point(0, 0), LineIntersection.FindIntersection(line1, line2));
        }
    }
}
