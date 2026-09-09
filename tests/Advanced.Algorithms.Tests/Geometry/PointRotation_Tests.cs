using Advanced.Algorithms.Geometry;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.Geometry
{
    [TestClass]
    public class PointRotationTests
    {
        [TestMethod]
        public void PointRotation_Smoke_Test()
        {
            var result = PointRotation.Rotate(
                new Point(0, 0),
                new Point(5, 5),
                -45);

            Assert.AreEqual(7, (int)result.X);
            Assert.AreEqual(0, (int)result.Y);

            result = PointRotation.Rotate(
                new Point(0, 0),
                new Point(5, 5),
                -90);

            Assert.AreEqual(5, (int)result.X);
            Assert.AreEqual(-5, (int)result.Y);
        }

        [TestMethod]
        public void PointRotation_Corner_Cases_Test()
        {
            var unchanged = PointRotation.Rotate(new Point(1, 1), new Point(4, 5), 0);
            Assert.AreEqual(4, unchanged.X, 1e-9);
            Assert.AreEqual(5, unchanged.Y, 1e-9);

            var fullTurn = PointRotation.Rotate(new Point(0, 0), new Point(3, 4), 360);
            Assert.AreEqual(3, fullTurn.X, 1e-9);
            Assert.AreEqual(4, fullTurn.Y, 1e-9);

            var aboutSelf = PointRotation.Rotate(new Point(2, 2), new Point(2, 2), 90);
            Assert.AreEqual(2, aboutSelf.X, 1e-9);
            Assert.AreEqual(2, aboutSelf.Y, 1e-9);
        }

        /// <summary>
        /// Oracle: 90/180/270 about origin and non-origin center.
        /// </summary>
        [TestMethod]
        public void PointRotation_Oracle_CardinalAngles_Test()
        {
            var r90 = PointRotation.Rotate(new Point(0, 0), new Point(1, 0), 90);
            Assert.AreEqual(0, r90.X, 1e-9);
            Assert.AreEqual(1, r90.Y, 1e-9);

            var r180 = PointRotation.Rotate(new Point(0, 0), new Point(3, 4), 180);
            Assert.AreEqual(-3, r180.X, 1e-9);
            Assert.AreEqual(-4, r180.Y, 1e-9);

            var r270 = PointRotation.Rotate(new Point(1, 1), new Point(2, 1), 270);
            Assert.AreEqual(1, r270.X, 1e-9);
            Assert.AreEqual(0, r270.Y, 1e-9);
        }
    }
}