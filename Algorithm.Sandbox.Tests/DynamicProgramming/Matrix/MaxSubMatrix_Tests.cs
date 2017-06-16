using Algorithm.Sandbox.DynamicProgramming.Matrix;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Algorithm.Sandbox.Tests.DynamicProgramming.Matrix
{
    /// <summary>
    /// Problem statement below
    /// http://www.geeksforgeeks.org/dynamic-programming-set-27-max-sum-rectangle-in-a-2d-matrix/
    /// </summary>
    [TestClass]
    public class MaxSubMatrix_Tests
    {
        [TestMethod]
        public void MaxSubMatrix_Smoke_Test()
        {
            var testMatrix = new int[4, 5] {
                       {1, 2, -1, -4, -20},
                       {-8, -3, 4, 2, 1},
                       {3, 8, 10, 1, 3},
                       {-4, -1, 1, 7, -6}
                      };

            var result = MaxSubMatrix
                .FindMaxSubMatrixSum(testMatrix);

            Assert.AreEqual(29, result);
        }
    }
}
