using Advanced.Algorithms.Geometry;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.Geometry
{
    [TestClass]
    public class LineIntersectionTests
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

            Assert.IsTrue(pointComparer.Equals(default, LineIntersection.Find(line1, line2)));

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

        [TestMethod]
        public void LineIntersection_Corner_Cases_Test()
        {
            var pointComparer = new PointComparer();

            var verticalA = new Line(new Point(2, 0), new Point(2, 5));
            var verticalB = new Line(new Point(4, 0), new Point(4, 5));
            Assert.IsNull(LineIntersection.Find(verticalA, verticalB));

            var horizontalA = new Line(new Point(0, 3), new Point(5, 3));
            var horizontalB = new Line(new Point(0, 7), new Point(5, 7));
            Assert.IsNull(LineIntersection.Find(horizontalA, horizontalB));

            var overlapVerticalA = new Line(new Point(1, 0), new Point(1, 4));
            var overlapVerticalB = new Line(new Point(1, 2), new Point(1, 6));
            Assert.IsTrue(pointComparer.Equals(new Point(1, 2),
                LineIntersection.Find(overlapVerticalA, overlapVerticalB)));

            var overlapHorizontalA = new Line(new Point(0, 1), new Point(4, 1));
            var overlapHorizontalB = new Line(new Point(2, 1), new Point(6, 1));
            Assert.IsTrue(pointComparer.Equals(new Point(2, 1),
                LineIntersection.Find(overlapHorizontalA, overlapHorizontalB)));

            var same = new Line(new Point(0, 0), new Point(1, 1));
            Assert.ThrowsException<System.ArgumentException>(() => LineIntersection.Find(same, same));

            Assert.IsFalse(same.Intersects(new Line(new Point(0, 1), new Point(1, 2))));
            Assert.IsTrue(horizontalA.Intersects(verticalA));
            Assert.IsTrue(pointComparer.Equals(new Point(2, 3), horizontalA.Intersection(verticalA)));
        }

        /// <summary>
        /// Oracle: hand-computed crossings, T-junction, endpoint touch, diagonal overlap.
        /// </summary>
        [TestMethod]
        public void LineIntersection_Oracle_HandCases_Test()
        {
            var pointComparer = new PointComparer();

            Assert.IsTrue(pointComparer.Equals(new Point(5, 5),
                LineIntersection.Find(
                    new Line(new Point(0, 0), new Point(10, 10)),
                    new Line(new Point(0, 10), new Point(10, 0)))));

            Assert.IsTrue(pointComparer.Equals(new Point(5, 5),
                LineIntersection.Find(
                    new Line(new Point(0, 5), new Point(10, 5)),
                    new Line(new Point(5, 5), new Point(5, 0)))));

            Assert.IsNull(LineIntersection.Find(
                new Line(new Point(0, 0), new Point(1, 1)),
                new Line(new Point(0, 1), new Point(1, 2))));

            Assert.IsTrue(pointComparer.Equals(new Point(2, 2),
                LineIntersection.Find(
                    new Line(new Point(0, 0), new Point(4, 4)),
                    new Line(new Point(2, 2), new Point(6, 6)))));

            Assert.IsNull(LineIntersection.Find(
                new Line(new Point(0, 0), new Point(1, 1)),
                new Line(new Point(3, 3), new Point(5, 5))));
        }
    }
}