using Algorithm.Sandbox.Sorting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Algorithm.Sandbox.Tests.Sorting
{
    /// <summary>
    /// Problem statement below
    /// http://www.geeksforgeeks.org/nearly-sorted-algorithm/
    /// </summary>
    [TestClass]
    public class SortAlmostSorted_Tests
    {
        private static int[] testArray =
            new int[] { 2, 6, 3, 12, 56, 8 };

        /// <summary>
        /// </summary>
        [TestMethod]
        public void SortAlmostSorted_Smoke_Test()
        {
            CollectionAssert.AreEqual(new int[] { 2, 3, 6, 8, 12, 56 },
                SortAlmostSorted.Sort(new int[] { 2, 6, 3, 12, 56, 8 }, 3));
        }
    }
}
