using Advanced.Algorithms.Geometry;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
                LeftTopCorner = new Point(0, 10),
                RightBottomCorner = new Point(10, 0)
            },
            new Rectangle()
            {
                LeftTopCorner = new Point(5, 5),
                RightBottomCorner = new Point(15, 0)
            });

            Assert.AreEqual(result, new Rectangle()
            {
                LeftTopCorner = new Point(5, 5),
                RightBottomCorner = new Point(10, 0)
            });

            result = RectangleIntersection.FindIntersection(new Rectangle()
            {
                LeftTopCorner = new Point(0, 10),
                RightBottomCorner = new Point(4, 0)
            },
            new Rectangle()
            {
                LeftTopCorner = new Point(5, 5),
                RightBottomCorner = new Point(15, 0)
            });

            Assert.AreEqual(result, null);
        }

    }
}
