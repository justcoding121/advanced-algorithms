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

        [TestMethod]
        public void BloomFilter_Accuracy_Test()
        {
            var bloomFilter = new BloomFilter<string>(10);

            bloomFilter.AddKey("foo");
            bloomFilter.AddKey("bar");
            bloomFilter.AddKey("apple");
            bloomFilter.AddKey("orange");
            bloomFilter.AddKey("banana");

            Assert.IsTrue(bloomFilter.KeyExists("bar"));
            Assert.IsFalse(bloomFilter.KeyExists("ba111r"));

            Assert.IsTrue(bloomFilter.KeyExists("banana"));
            Assert.IsFalse(bloomFilter.KeyExists("dfs11j"));

            Assert.IsTrue(bloomFilter.KeyExists("foo"));
            Assert.IsFalse(bloomFilter.KeyExists("1foo"));

            Assert.IsTrue(bloomFilter.KeyExists("apple"));
            Assert.IsFalse(bloomFilter.KeyExists("applefoo"));

            Assert.IsTrue(bloomFilter.KeyExists("orange"));
            Assert.IsFalse(bloomFilter.KeyExists("orangew"));
        }
    }
}
