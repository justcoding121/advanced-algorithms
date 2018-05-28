using Advanced.Algorithms.Geometry;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.Geometry
{
    [TestClass]
    public class PointRotation_Tests
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
    }
}
