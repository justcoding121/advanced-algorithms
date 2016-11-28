using Algorithm.Sandbox.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Algorithm.Sandbox.Tests.DataStructures.Lists
{
    [TestClass]
    public class SkipList_Tests
    {
        /// <summary>
        /// A skip list test
        /// </summary>
        [TestMethod]
        public void SkipList_Test()
        {
            var skipList = new AsSkipList<int>();

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
