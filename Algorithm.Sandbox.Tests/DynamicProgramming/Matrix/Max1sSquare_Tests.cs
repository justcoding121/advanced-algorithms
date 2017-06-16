using Algorithm.Sandbox.DynamicProgramming.Matrix;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Algorithm.Sandbox.Tests.DynamicProgramming.Matrix
{
    /// <summary>
    /// Problem statement below
    /// http://www.geeksforgeeks.org/maximum-size-sub-matrix-with-all-1s-in-a-binary-matrix/
    /// </summary>
    [TestClass]
    public class Max1sSquare_Tests
    {
        [TestMethod]
        public void Max1sSquare_Smoke_Test()
        {
            var testMatrix = new int[6,5] {
                   {0, 1, 1, 0, 1},
                   {1, 1, 0, 1, 0},
                   {0, 1, 1, 1, 0},
                   {1, 1, 1, 1, 0},
                   {1, 1, 1, 1, 1},
                   {0, 0, 0, 0, 0}
            };

            var result = Max1sSquare
                .FindMax(testMatrix);

            Assert.AreEqual(9, result);
        }
    }
}
