using System;
using System.Linq;
using Advanced.Algorithms.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.DataStructures
{
    [TestClass]
    public class SkipListTests
    {
        [TestMethod]
        public void SkipList_Test()
        {
            var skipList = new SkipList<int>();

            for (var i = 1; i < 100; i++) skipList.Insert(i);

            for (var i = 1; i < 100; i++) Assert.AreEqual(i, skipList.Find(i));

            Assert.AreEqual(0, skipList.Find(101));

            for (var i = 1; i < 100; i++)
            {
                skipList.Delete(i);
                Assert.AreEqual(0, skipList.Find(i));
            }

            for (var i = 1; i < 50; i++) skipList.Insert(i);

            Assert.ThrowsException<ArgumentException>(() => skipList.Insert(25));
            Assert.ThrowsException<ArgumentException>(() => skipList.Delete(52));

            //IEnumerable test using linq
            Assert.AreEqual(skipList.Count, skipList.Count());

            for (var i = 1; i < 50; i++) Assert.AreEqual(i, skipList.Find(i));

            for (var i = 1; i < 50; i++)
            {
                skipList.Delete(i);
                Assert.AreEqual(0, skipList.Find(i));
            }
        }

        [TestMethod]
        public void SkipList_Duplicate_And_Missing_Throws()
        {
            var skipList = new SkipList<int>();
            skipList.Insert(1);

            Assert.ThrowsException<ArgumentException>(() => skipList.Insert(1));
            Assert.ThrowsException<ArgumentException>(() => skipList.Delete(2));
            Assert.AreEqual(1, skipList.Find(1));
        }
    }
}
