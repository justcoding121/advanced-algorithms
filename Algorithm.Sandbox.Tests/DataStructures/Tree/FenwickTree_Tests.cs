using Algorithm.Sandbox.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Algorithm.Sandbox.Tests.DataStructures.Tree
{
    [TestClass]
    public class AsFenwickTreeTests
    {
        /// <summary>
        /// Smoke test
        /// </summary>
        [TestMethod]
        public void AsFenwickTree_Sum_Smoke_Test()
        {
            var testArray = new int[] { 1, 3, 5, 7, 9, 11 };

            //tree with sum operation
            var tree = new AsFenwickTree<int>(testArray,
                new Func<int, int, int>((x, y) => x + y));

            var sum = tree.GetPrefixSum(3);

            Assert.AreEqual(16, sum);
        }
    }
}
