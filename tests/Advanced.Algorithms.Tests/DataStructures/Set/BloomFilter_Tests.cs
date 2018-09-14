using Advanced.Algorithms.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Advanced.Algorithms.Tests.DataStructures
{
    [TestClass]
    public class BloomFilter_Tests
    {
        [TestMethod]
        public void BloomFilter_Smoke_Test()
        {
            var filter = new BloomFilter<string>(100);

            filter.AddKey("cat");
            filter.AddKey("rat");

            Assert.IsTrue(filter.KeyExists("cat"));
            Assert.IsFalse(filter.KeyExists("bat"));
        }
    }
}
