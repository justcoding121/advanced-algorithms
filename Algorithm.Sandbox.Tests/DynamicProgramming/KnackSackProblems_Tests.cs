using Algorithm.Sandbox.DataStructures;
using Algorithm.Sandbox.DynamicProgramming;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Algorithm.Sandbox.Tests.DynamicProgramming
{
    [TestClass]
    public class KnackSackProblems_Tests
    {
        [TestMethod]
        public void KnackSack10_Tests()
        {
            //sample inputs
            int[] weights = new int[] { 10, 20, 30 };
            int[] values = new int[] { 60, 100, 120 };

            //max weight capacity of bag
            int W = 49;

            var result = KnackSackProblems.KnackSack_10_Recursive(W, weights, values, weights.Length, new AsDictionary<string, int>());

            Assert.AreEqual(result, 180);

        }

        /// <summary>
        /// Gets the minimum number of coins to fit in the amount 
        /// </summary>
        [TestMethod]
        public void KnackSack_Fractional_Tests()
        {
            //sample inputs
            int[] weights = new int[] { 5, 20, 10, 12 };
            int[] values = new int[] { 50, 140, 60, 60 };

            //max weight capacity of bag
            int W = 30;

            var result = KnackSackProblems.KnackSack_Fractional(W, weights, values);

            Assert.AreEqual(result, 220);
        }
    }
}
