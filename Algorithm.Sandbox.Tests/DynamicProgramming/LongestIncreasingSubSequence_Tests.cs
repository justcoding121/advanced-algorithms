using Algorithm.Sandbox.DynamicProgramming;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Algorithm.Sandbox.Tests.DynamicProgramming
{
    [TestClass]
    public class LongestIncreasingSubSequence_Tests
    {
        [TestMethod]
        public void LongestIncreasingSubSequence_Smoke_Test()
        {
            CollectionAssert.AreEqual(new int[] { 15, 27, 38, 55, 65, 85 }, 
                LongestIncreasingSubSequence.FindSequence(new int[] { 15, 27, 14, 38, 26, 55, 46, 65, 85 }));

            CollectionAssert.AreEqual(new int[] { 10, 22, 33, 50, 60, 80 },
                LongestIncreasingSubSequence.FindSequence(new int[] { 10, 22, 9, 33, 21, 50, 41, 60, 80 }));
        }
    }
}
