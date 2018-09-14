using Advanced.Algorithms.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Advanced.Algorithms.Tests.DataStructures
{
    [TestClass]
    public class SkipList_Tests
    {
        [TestMethod]
        public void SkipList_Test()
        {
            var skipList = new SkipList<int>();

            for (int i = 1; i < 100; i++)
            {
                skipList.Insert(i);
            }

            for (int i = 1; i < 100; i++)
            {
                Assert.AreEqual(i, skipList.Find(i));
            }

            Assert.AreEqual(0, skipList.Find(101));

            for (int i = 1; i < 100; i++)
            {
                skipList.Delete(i);
                Assert.AreEqual(0, skipList.Find(i));
            }

            for (int i = 1; i < 50; i++)
            {
                skipList.Insert(i);
            }

            try
            {
                skipList.Insert(25);
                Assert.Fail("Duplicate insertion allowed.");
            }
            catch (Exception) { }

            try
            {
                skipList.Delete(52);
                Assert.Fail("Deletion of item not in skip list did'nt throw exception.");
            }
            catch (Exception) { }

            //IEnumerable test using linq
            Assert.AreEqual(skipList.Count, skipList.Count());

            for (int i = 1; i < 50; i++)
            {
                Assert.AreEqual(i, skipList.Find(i));
            }

            for (int i = 1; i < 50; i++)
            {
                skipList.Delete(i);
                Assert.AreEqual(0, skipList.Find(i));
            }
        }
    }
}
