using System;
using System.Linq;
using Advanced.Algorithms.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.DataStructures
{
    [TestClass]
    public class SegmentTreeTests
    {
        /// <summary>
        ///     Smoke test
        /// </summary>
        [TestMethod]
        public void SegmentTree_Sum_Smoke_Test()
        {
            var testArray = new[] { 1, 3, 5, 7, 9, 11 };

            //tree with sum operation
            var tree = new SegmentTree<int>(testArray,
                (x, y) => x + y,
                () => 0);

            var sum = tree.RangeResult(1, 3);

            Assert.AreEqual(15, sum);
        }

        [TestMethod]
        public void SegmentTree_Corner_Cases()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new SegmentTree<int>(null, (x, y) => x + y, () => 0));
            Assert.ThrowsException<ArgumentNullException>(() => new SegmentTree<int>(new[] { 1 }, null, () => 0));
            Assert.ThrowsException<ArgumentNullException>(() => new SegmentTree<int>(new[] { 1 }, (x, y) => x + y, null));

            var tree = new SegmentTree<int>(new[] { 1, 3, 5 }, (x, y) => x + y, () => 0);
            Assert.ThrowsException<ArgumentException>(() => tree.RangeResult(-1, 1));
            Assert.ThrowsException<ArgumentException>(() => tree.RangeResult(0, 3));
            Assert.ThrowsException<ArgumentException>(() => tree.RangeResult(2, 1));
            Assert.AreEqual(1, tree.RangeResult(0, 0));
            Assert.AreEqual(9, tree.RangeResult(0, 2));
            Assert.AreEqual(3, tree.Count());

            var empty = new SegmentTree<int>(Array.Empty<int>(), (x, y) => x + y, () => 0);
            Assert.AreEqual(0, empty.Count());
            Assert.ThrowsException<ArgumentException>(() => empty.RangeResult(0, 0));
        }

        [TestMethod]
        public void SegmentTree_Range_Oracle()
        {
            var input = new[] { 1, 3, 5, 7, 9, 11 };
            var sumTree = new SegmentTree<int>(input, (x, y) => x + y, () => 0);
            var minTree = new SegmentTree<int>(input, Math.Min, () => int.MaxValue);

            for (var i = 0; i < input.Length; i++)
            for (var j = i; j < input.Length; j++)
            {
                var sum = 0;
                var min = int.MaxValue;
                for (var k = i; k <= j; k++)
                {
                    sum += input[k];
                    min = Math.Min(min, input[k]);
                }

                Assert.AreEqual(sum, sumTree.RangeResult(i, j));
                Assert.AreEqual(min, minTree.RangeResult(i, j));
            }
        }
    }
}