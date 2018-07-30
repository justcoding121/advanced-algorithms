using Advanced.Algorithms.Distributed;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests
{
    [TestClass]
    public class LRUCache_Tests
    {

        [TestMethod]
        public void LRUCache_Smoke_Test()
        {
            var cache = new LRUCache<int,int>(2);

            cache.Put(1, 1);
            cache.Put(2, 2);
            Assert.AreEqual(1, cache.Get(1));

            cache.Put(3, 3);
            Assert.AreEqual(0, cache.Get(2));

            cache.Put(4, 4);
            Assert.AreEqual(0, cache.Get(1));
            Assert.AreEqual(3, cache.Get(3));
            Assert.AreEqual(4, cache.Get(4));
        }

    }
}