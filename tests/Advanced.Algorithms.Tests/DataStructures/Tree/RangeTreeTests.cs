using System.Linq;
using Advanced.Algorithms.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.DataStructures
{
    [TestClass]
    public class RangeTreeTests
    {
        /// <summary>
        ///     Smoke test
        /// </summary>
        [TestMethod]
        public void RangeTree1D_Smoke_Test()
        {
            var tree = new RangeTree<int>(1);

            tree.Insert(new[] { 0 });
            tree.Insert(new[] { 1 });
            tree.Insert(new[] { 2 });
            tree.Insert(new[] { 3 });
            tree.Insert(new[] { 4 });
            tree.Insert(new[] { 5 });
            tree.Insert(new[] { 6 });
            tree.Insert(new[] { 7 });

            //IEnumerable test
            Assert.AreEqual(tree.Count, tree.Count());

            var rangeResult = tree.RangeSearch(new[] { 2 }, new[] { 6 });
            Assert.IsTrue(rangeResult.Count == 5);

            //IEnumerable test
            Assert.AreEqual(tree.Count, tree.Count());

            tree.Delete(new[] { 4 });
            rangeResult = tree.RangeSearch(new[] { -1 }, new[] { 6 });
            Assert.IsTrue(rangeResult.Count == 6);

            tree.Delete(new[] { 0 });
            tree.Delete(new[] { 1 });
            tree.Delete(new[] { 2 });
            tree.Delete(new[] { 3 });
            tree.Delete(new[] { 5 });
            tree.Delete(new[] { 6 });
            tree.Delete(new[] { 7 });
        }

        [TestMethod]
        public void RangeTree2D_Smoke_Test()
        {
            var tree = new RangeTree<int>(2);

            tree.Insert(new[] { 0, 1 });
            tree.Insert(new[] { 1, 1 });
            tree.Insert(new[] { 2, 5 });
            tree.Insert(new[] { 3, 6 });
            tree.Insert(new[] { 4, 5 });
            tree.Insert(new[] { 4, 7 });
            tree.Insert(new[] { 5, 8 });
            tree.Insert(new[] { 6, 9 });
            tree.Insert(new[] { 7, 10 });

            //IEnumerable test
            Assert.AreEqual(tree.Count, tree.Count());

            var rangeResult = tree.RangeSearch(new[] { 1, 1 }, new[] { 3, 7 });
            Assert.IsTrue(rangeResult.Count == 3);

            tree.Delete(new[] { 2, 5 });
            rangeResult = tree.RangeSearch(new[] { 1, 1 }, new[] { 3, 7 });
            Assert.IsTrue(rangeResult.Count == 2);

            //IEnumerable test
            Assert.AreEqual(tree.Count, tree.Count());

            tree.Delete(new[] { 3, 6 });
            rangeResult = tree.RangeSearch(new[] { 1, 1 }, new[] { 3, 7 });
            Assert.IsTrue(rangeResult.Count == 1);

            tree.Delete(new[] { 0, 1 });
            tree.Delete(new[] { 1, 1 });
            tree.Delete(new[] { 4, 5 });
            tree.Delete(new[] { 4, 7 });
            tree.Delete(new[] { 5, 8 });
            tree.Delete(new[] { 6, 9 });
            tree.Delete(new[] { 7, 10 });

            //IEnumerable test
            Assert.AreEqual(tree.Count, tree.Count());
        }
    }
}