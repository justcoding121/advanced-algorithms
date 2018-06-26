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
            var pointComparer = new PointComparer();

            var line1 = new Line(new Point(1, 1), new Point(10, 1));
            var line2 = new Line(new Point(1, 2), new Point(10, 2));

            Assert.IsTrue(pointComparer.Equals(null, LineIntersection.Find(line1, line2)));

            line1 = new Line(new Point(10, 0), new Point(0, 10));
            line2 = new Line(new Point(0, 10), new Point(10, 10));

            Assert.IsTrue(pointComparer.Equals(new Point(0, 10), LineIntersection.Find(line1, line2)));

            line1 = new Line(new Point(0, 0), new Point(10, 10));
            line2 = new Line(new Point(0, 10), new Point(10, 10));

            Assert.IsTrue(pointComparer.Equals(new Point(10, 10), LineIntersection.Find(line1, line2)));


            line1 = new Line(new Point(10, 0), new Point(0, 10));
            line2 = new Line(new Point(0, 0), new Point(10, 10));

            Assert.IsTrue(pointComparer.Equals(new Point(5, 5), LineIntersection.Find(line1, line2)));

            line1 = new Line(new Point(-5, -5), new Point(0, 0));
            line2 = new Line(new Point(1, 1), new Point(10, 10));

            Assert.IsTrue(pointComparer.Equals(default(Point), LineIntersection.Find(line1, line2)));

            line1 = new Line(new Point(3, -5), new Point(3, 10));
            line2 = new Line(new Point(0, 5), new Point(10, 5));

            Assert.IsTrue(pointComparer.Equals(new Point(3, 5), LineIntersection.Find(line1, line2)));

            line1 = new Line(new Point(0, 5), new Point(10, 5));
            line2 = new Line(new Point(3, -5), new Point(3, 10));

            Assert.IsTrue(pointComparer.Equals(new Point(3, 5), LineIntersection.Find(line1, line2)));

            line1 = new Line(new Point(0, 5), new Point(10, 5));
            line2 = new Line(new Point(3, -5), new Point(5, 15));

            Assert.IsTrue(pointComparer.Equals(new Point(4, 5), LineIntersection.Find(line1, line2)));

            line1 = new Line(new Point(0, -5), new Point(0, 5));
            line2 = new Line(new Point(-3, 0), new Point(3, 0));

            Assert.IsTrue(pointComparer.Equals(new Point(0, 0), LineIntersection.Find(line1, line2)));
        }
    }
}
