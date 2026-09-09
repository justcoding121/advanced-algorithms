using System;
using System.Linq;
using Advanced.Algorithms.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.DataStructures
{
    [TestClass]
    public class FenwickTreeTests
    {
        /// <summary>
        ///     Smoke test
        /// </summary>
        [TestMethod]
        public void FenwickTree_Sum_Smoke_Test()
        {
            var testArray = new[] { 1, 3, 5, 7, 9, 11 };

            //tree with sum operation
            var tree = new FenwickTree<int>(testArray,
                (x, y) => x + y);

            var sum = tree.PrefixSum(3);

            Assert.AreEqual(16, sum);
        }

        [TestMethod]
        public void FenwickTree_Corner_Cases()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new FenwickTree<int>(null, (x, y) => x + y));
            Assert.ThrowsException<ArgumentNullException>(() => new FenwickTree<int>(new[] { 1 }, null));

            var tree = new FenwickTree<int>(new[] { 1, 2, 3 }, (x, y) => x + y);
            Assert.ThrowsException<ArgumentException>(() => tree.PrefixSum(-1));
            Assert.ThrowsException<ArgumentException>(() => tree.PrefixSum(3));
            Assert.AreEqual(1, tree.PrefixSum(0));
            Assert.AreEqual(3, tree.PrefixSum(1));
            Assert.AreEqual(6, tree.PrefixSum(2));
            Assert.AreEqual(3, tree.Count());

            var empty = new FenwickTree<int>(Array.Empty<int>(), (x, y) => x + y);
            Assert.AreEqual(0, empty.Count());
            Assert.ThrowsException<ArgumentException>(() => empty.PrefixSum(0));
        }

        [TestMethod]
        public void FenwickTree_PrefixSum_Oracle()
        {
            var input = new[] { 1, 3, 5, 7, 9, 11 };
            var tree = new FenwickTree<int>(input, (x, y) => x + y);

            var expected = 0;
            for (var i = 0; i < input.Length; i++)
            {
                expected += input[i];
                Assert.AreEqual(expected, tree.PrefixSum(i));
            }

            var single = new FenwickTree<int>(new[] { 42 }, (x, y) => x + y);
            Assert.AreEqual(42, single.PrefixSum(0));
        }
    }
}