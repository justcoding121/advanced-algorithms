using Advanced.Algorithms.Numerical;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Advanced.Algorithms.Tests.Numerical
{
    [TestClass]
    public class KthSmallest_Tests
    {
        [TestMethod]
        public void KthSmallest_Smoke_Test()
        {
            var kthSmallest = new KthSmallest<int>();

            var k = kthSmallest
                .FindKthSmallest(new int[] { 12, 3, 5, 7, 4, 19, 26 }, 3);

            Assert.AreEqual(k, 5);
        }
    }
}
