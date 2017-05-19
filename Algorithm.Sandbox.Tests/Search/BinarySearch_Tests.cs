using Algorithm.Sandbox.Search;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Algorithm.Sandbox.Tests.Search
{
    [TestClass]
    public class BinarySearch_Tests
    {
        [TestMethod]
        public void Search_Smoke_Test()
        {
            var test = new int[] { 2, 3, 5, 7, 11, 13, 17, 19,
                23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79 };

            Assert.AreEqual(15, BinarySearch.Search(test, 53));
            Assert.AreEqual(-1, BinarySearch.Search(test, 80));
        }
    }
}
