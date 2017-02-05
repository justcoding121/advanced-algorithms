using Algorithm.Sandbox.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Algorithm.Sandbox.Tests.DataStructures
{
    [TestClass]
    public class TreeHashSet_Tests
    {
        /// <summary>
        /// key value dictionary tests 
        /// </summary>
        [TestMethod]
        public void TreeHashSet_Test()
        {
            var hashSet = new AsTreeHashSet<int, int>();

            int nodeCount = 1000 * 10;
            //insert test


            for (int i = 0; i <= nodeCount; i++)
            {
                hashSet.Add(i, i);
                Assert.AreEqual(true, hashSet.ContainsKey(i));
            }


            for (int i = 0; i <= nodeCount; i++)
            {
                hashSet.Remove(i);
                Assert.AreEqual(false, hashSet.ContainsKey(i));
            }


            var rnd = new Random();
            var testSeries = Enumerable.Range(1, nodeCount).OrderBy(x => rnd.Next()).ToList();

            foreach (var item in testSeries)
            {
                hashSet.Add(item, item);
                Assert.AreEqual(true, hashSet.ContainsKey(item));
            }

            for (int i = 1; i <= nodeCount; i++)
            {
                hashSet.Remove(i);
                Assert.AreEqual(false, hashSet.ContainsKey(i));
            }

        }
    }
}
