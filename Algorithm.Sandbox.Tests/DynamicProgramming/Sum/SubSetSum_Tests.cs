using Algorithm.Sandbox.DynamicProgramming.Sum;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Algorithm.Sandbox.Tests.DynamicProgramming.Sum
{
    /// <summary>
    /// Problem statement
    /// http://www.geeksforgeeks.org/dynamic-programming-subset-sum-problem/
    /// </summary>
    [TestClass]
    public class SubSetSum_Tests
    {
        [TestMethod]
        public void SubSetSum_Smoke_Test()
        {
            Assert.IsTrue(SubSetSum.HasSubSet(new int[] { 3, 34, 4, 12, 5, 2 }, 9));
        }
    }
}
