using Advanced.Algorithms.Geometry;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.Geometry
{
    [TestClass]
    public class RectangleIntersectionTests
    {
        [TestMethod]
        public void RectIntersection_Smoke_Test()
        {
            var rectangleComparer = new RectangleComparer();

            var result = RectangleIntersection.FindIntersection(new Rectangle
                {
                    LeftTop = new Point(0, 10),
                    RightBottom = new Point(10, 0)
                },
                new Rectangle
                {
                    LeftTop = new Point(5, 5),
                    RightBottom = new Point(15, 0)
                });

            Assert.IsTrue(rectangleComparer.Equals(result, new Rectangle
            {
                LeftTop = new Point(5, 5),
                RightBottom = new Point(10, 0)
            }));

            result = RectangleIntersection.FindIntersection(new Rectangle
                {
                    LeftTop = new Point(0, 10),
                    RightBottom = new Point(4, 0)
                },
                new Rectangle
                {
                    LeftTop = new Point(5, 5),
                    RightBottom = new Point(15, 0)
                });

            Assert.IsTrue(rectangleComparer.Equals(result, null));
        }

        [TestMethod]
        public void RectIntersection_Corner_Cases_Test()
        {
            var a = new Rectangle
            {
                LeftTop = new Point(0, 10),
                RightBottom = new Point(10, 0)
            };
            var touching = new Rectangle
            {
                LeftTop = new Point(10, 5),
                RightBottom = new Point(20, 0)
            };

            Assert.IsTrue(RectangleIntersection.DoIntersect(a, touching));

            var above = new Rectangle
            {
                LeftTop = new Point(0, 20),
                RightBottom = new Point(10, 11)
            };
            Assert.IsFalse(RectangleIntersection.DoIntersect(a, above));
            Assert.IsNull(RectangleIntersection.FindIntersection(a, above));

            Assert.ThrowsException<System.ArgumentException>(() =>
                new Rectangle(new Point(0, 0), new Point(10, 10)));
            Assert.ThrowsException<System.ArgumentException>(() =>
                new Rectangle(new Point(10, 10), new Point(0, 0)));
        }

        /// <summary>
        /// Oracle: overlap, containment, disjoint, and edge-touch vs hand-expected rectangles.
        /// </summary>
        [TestMethod]
        public void RectIntersection_Oracle_HandCases_Test()
        {
            var comparer = new RectangleComparer();
            var outer = new Rectangle(new Point(0, 10), new Point(10, 0));
            var inner = new Rectangle(new Point(2, 8), new Point(8, 2));
            Assert.IsTrue(comparer.Equals(inner, RectangleIntersection.FindIntersection(outer, inner)));

            var overlap = new Rectangle(new Point(5, 5), new Point(15, 0));
            Assert.IsTrue(comparer.Equals(
                new Rectangle(new Point(5, 5), new Point(10, 0)),
                RectangleIntersection.FindIntersection(outer, overlap)));

            Assert.IsFalse(RectangleIntersection.DoIntersect(outer,
                new Rectangle(new Point(20, 10), new Point(30, 0))));

            Assert.IsTrue(RectangleIntersection.DoIntersect(outer,
                new Rectangle(new Point(10, 5), new Point(20, 0))));
        }
    }
}