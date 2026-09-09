using System;
using System.Linq;
using Advanced.Algorithms.DataStructures;
using Advanced.Algorithms.Geometry;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.DataStructures
{
    public class QuadTree_Tests
    {
        [TestClass]
        public class QuadTreeTests
        {
            [TestMethod]
            public void QuadTree_Smoke_Test()
            {
                var tree = new QuadTree<object>();

                tree.Insert(new Point(0, 1));
                tree.Insert(new Point(1, 1));
                tree.Insert(new Point(2, 5));
                tree.Insert(new Point(3, 6));
                tree.Insert(new Point(4, 5));
                tree.Insert(new Point(4, 7));
                tree.Insert(new Point(5, 8));
                tree.Insert(new Point(6, 9));
                tree.Insert(new Point(7, 10));

                var rangeResult = tree.RangeSearch(new Rectangle(new Point(1, 7), new Point(3, 1)));
                Assert.IsTrue(rangeResult.Count == 3);

                //IEnumerable test using linq
                Assert.AreEqual(tree.Count, tree.Count());

                tree.Delete(new Point(2, 5));
                rangeResult = tree.RangeSearch(new Rectangle(new Point(1, 7), new Point(3, 1)));
                Assert.IsTrue(rangeResult.Count == 2);
                Assert.AreEqual(tree.Count, tree.Count());

                tree.Delete(new Point(3, 6));
                rangeResult = tree.RangeSearch(new Rectangle(new Point(1, 7), new Point(3, 1)));
                Assert.IsTrue(rangeResult.Count == 1);
                Assert.AreEqual(tree.Count, tree.Count());

                tree.Delete(new Point(0, 1));
                tree.Delete(new Point(1, 1));
                tree.Delete(new Point(4, 5));
                tree.Delete(new Point(4, 7));
                tree.Delete(new Point(5, 8));
                tree.Delete(new Point(6, 9));
                tree.Delete(new Point(7, 10));

                Assert.AreEqual(0, tree.Count);
                Assert.AreEqual(0, tree.Count());
            }

            [TestMethod]
            public void QuadTree_Delete_All_Clears_Tree()
            {
                var tree = new QuadTree<object>();

                tree.Insert(new Point(0, 0));
                tree.Insert(new Point(1, 2));
                tree.Insert(new Point(3, 1));

                tree.Delete(new Point(0, 0));
                tree.Delete(new Point(1, 2));
                tree.Delete(new Point(3, 1));

                Assert.AreEqual(0, tree.Count);
                Assert.AreEqual(0, tree.Count());
                Assert.AreEqual(0, tree.RangeSearch(new Rectangle(new Point(-10, 10), new Point(10, -10))).Count);

                Assert.ThrowsException<ArgumentException>(() => tree.Delete(new Point(0, 0)));
            }

            [TestMethod]
            public void QuadTree_Delete_Triggers_Reconstruction()
            {
                var tree = new QuadTree<object>();

                // Even count so deleting half forces reconstruction (deleted >= live).
                tree.Insert(new Point(2, 2));
                tree.Insert(new Point(1, 1));
                tree.Insert(new Point(1, 3));
                tree.Insert(new Point(3, 3));

                tree.Delete(new Point(1, 1));
                tree.Delete(new Point(1, 3));

                Assert.AreEqual(2, tree.Count);
                Assert.AreEqual(2, tree.Count());

                var rangeResult = tree.RangeSearch(new Rectangle(new Point(0, 4), new Point(4, 0)));
                Assert.AreEqual(2, rangeResult.Count);
                Assert.IsTrue(rangeResult.Any(x => x.Item1.X == 2 && x.Item1.Y == 2));
                Assert.IsTrue(rangeResult.Any(x => x.Item1.X == 3 && x.Item1.Y == 3));

                // Already-deleted / missing points should throw.
                Assert.ThrowsException<ArgumentException>(() => tree.Delete(new Point(1, 1)));
                Assert.ThrowsException<ArgumentException>(() => tree.Delete(new Point(9, 9)));
            }

            [TestMethod]
            public void QuadTree_Delete_Uses_Tolerant_Float_Equality()
            {
                var tree = new QuadTree<object>();

                tree.Insert(new Point(0.1 + 0.2, 1.0));
                Assert.AreEqual(1, tree.Count);

                // 0.1 + 0.2 is not exactly 0.3; tolerant Find should still match.
                tree.Delete(new Point(0.3, 1.0));
                Assert.AreEqual(0, tree.Count);
                Assert.AreEqual(0, tree.Count());
            }

            [TestMethod]
            public void QuadTree_Delete_With_Fresh_Point_Instance()
            {
                var tree = new QuadTree<object>();

                tree.Insert(new Point(1.5, 2.5));
                tree.Delete(new Point(1.5, 2.5));

                Assert.AreEqual(0, tree.Count);
            }

            [TestMethod]
            public void QuadTree_Insert_Duplicate_Within_Tolerance_Throws()
            {
                var tree = new QuadTree<object>();

                tree.Insert(new Point(0.1 + 0.2, 1.0));

                Assert.ThrowsException<ArgumentException>(() => tree.Insert(new Point(0.3, 1.0)));
                Assert.AreEqual(1, tree.Count);
            }

            [TestMethod]
            public void QuadTree_Empty_Range_And_Missing_Delete()
            {
                var tree = new QuadTree<object>();

                Assert.AreEqual(0, tree.Count);
                Assert.AreEqual(0, tree.Count());
                Assert.AreEqual(0, tree.RangeSearch(new Rectangle(new Point(0, 1), new Point(1, 0))).Count);
                Assert.ThrowsException<ArgumentException>(() => tree.Delete(new Point(0, 0)));
            }
        }
    }
}
