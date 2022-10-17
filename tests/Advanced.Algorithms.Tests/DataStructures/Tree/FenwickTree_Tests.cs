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
    }
}