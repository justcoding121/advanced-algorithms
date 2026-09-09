using System;
using Advanced.Algorithms.Distributed;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests
{
    [TestClass]
    public class LruCacheTests
    {
        [TestMethod]
        public void LRUCache_Smoke_Test()
        {
            var cache = new LruCache<int, int>(2);

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

        [TestMethod]
        public void LRUCache_Invalid_Capacity_Throws()
        {
            Assert.ThrowsException<ArgumentException>(() => new LruCache<int, int>(0));
            Assert.ThrowsException<ArgumentException>(() => new LruCache<int, int>(-1));
        }

        [TestMethod]
        public void LRUCache_Capacity_One_And_Miss()
        {
            var cache = new LruCache<int, int>(1);

            Assert.AreEqual(0, cache.Get(99));

            cache.Put(1, 10);
            Assert.AreEqual(10, cache.Get(1));

            cache.Put(2, 20);
            Assert.AreEqual(0, cache.Get(1));
            Assert.AreEqual(20, cache.Get(2));
        }

        [TestMethod]
        public void LRUCache_Adversarial_Update_And_Eviction_Order()
        {
            var cache = new LruCache<int, int>(2);

            cache.Put(1, 1);
            cache.Put(1, 11); // update must not throw or inflate capacity
            Assert.AreEqual(11, cache.Get(1));

            cache.Put(2, 2);
            cache.Put(3, 3); // evicts 1 (least recently used)
            Assert.AreEqual(0, cache.Get(1));
            Assert.AreEqual(2, cache.Get(2));
            Assert.AreEqual(3, cache.Get(3));

            cache.Put(2, 22); // update 2; 3 becomes LRU
            cache.Put(4, 4);
            Assert.AreEqual(0, cache.Get(3));
            Assert.AreEqual(22, cache.Get(2));
            Assert.AreEqual(4, cache.Get(4));
        }
    }
}