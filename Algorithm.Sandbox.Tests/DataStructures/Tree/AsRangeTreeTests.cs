using Algorithm.Sandbox.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Algorithm.Sandbox.Tests.DataStructures.Tree
{
    [TestClass]
    public class AsRangeTreeTests
    {
        /// <summary>
        /// Smoke test
        /// </summary>
        [TestMethod]
        public void AsRangeTree1D_Smoke_Test()
        {
            var tree = new AsDRangeTree<int>(1);

            tree.Insert(new int[] { 0 });
            tree.Insert(new int[] { 1 });
            tree.Insert(new int[] { 2 });
            tree.Insert(new int[] { 3 });
            tree.Insert(new int[] { 4 });
            tree.Insert(new int[] { 4 });
            tree.Insert(new int[] { 5 });
            tree.Insert(new int[] { 6 });
            tree.Insert(new int[] { 7 });

            var rangeResult = tree.GetInRange(new int[] { 2 }, new int[] { 6 });
            Assert.IsTrue(rangeResult.Length == 6);

            tree.Delete(new int[] { 4 });
            rangeResult = tree.GetInRange(new int[] { 2 }, new int[] { 6 });
            Assert.IsTrue(rangeResult.Length == 5);

            tree.Delete(new int[] { 4 });
            rangeResult = tree.GetInRange(new int[] { -1 }, new int[] { 6 });
            Assert.IsTrue(rangeResult.Length == 6);

            tree.Delete(new int[] { 0 });
            tree.Delete(new int[] { 1 });
            tree.Delete(new int[] { 2 });
            tree.Delete(new int[] { 3 });
            tree.Delete(new int[] { 5 });
            tree.Delete(new int[] { 6 });
            tree.Delete(new int[] { 7 });

        }

        [TestMethod]
        public void AsRangeTree2D_Smoke_Test()
        {
            var tree = new AsDRangeTree<int>(2);

            tree.Insert(new int[] { 0, 1 });
            tree.Insert(new int[] { 1, 1 });
            tree.Insert(new int[] { 2, 5 });
            tree.Insert(new int[] { 3, 6 });
            tree.Insert(new int[] { 4, 5 });
            tree.Insert(new int[] { 4, 7 });
            tree.Insert(new int[] { 5, 8 });
            tree.Insert(new int[] { 6, 9 });
            tree.Insert(new int[] { 7, 10 });

            var rangeResult = tree.GetInRange(new int[] { 1, 1 }, new int[] { 3, 7 });
            Assert.IsTrue(rangeResult.Length == 3);

            tree.Delete(new int[] { 2, 5 });
            rangeResult = tree.GetInRange(new int[] { 1, 1 }, new int[] { 3, 7 });
            Assert.IsTrue(rangeResult.Length == 2);

            tree.Delete(new int[] { 3, 6 });
            rangeResult = tree.GetInRange(new int[] { 1, 1 }, new int[] { 3, 7 });
            Assert.IsTrue(rangeResult.Length == 1);

            tree.Delete(new int[] { 0, 1 });
            tree.Delete(new int[] { 1, 1 });
            tree.Delete(new int[] { 4, 5 });
            tree.Delete(new int[] { 4, 7 });
            tree.Delete(new int[] { 5, 8 });
            tree.Delete(new int[] { 6, 9 });
            tree.Delete(new int[] { 7, 10 });


        }
    }
}
