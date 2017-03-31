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
        /// key value HashSet tests 
        /// </summary>
        [TestMethod]
        public void TreeHashSet_Test()
        {
            var HashSet = new AsTreeHashSet<int>();

            int nodeCount = 1000 * 10;
            //insert test


            for (int i = 0; i <= nodeCount; i++)
            {
                HashSet.Add(i);
                Assert.AreEqual(true, HashSet.Contains(i));
            }


            for (int i = 0; i <= nodeCount; i++)
            {
                HashSet.Remove(i);
                Assert.AreEqual(false, HashSet.Contains(i));
            }


            var rnd = new Random();
            var testSeries = Enumerable.Range(1, nodeCount).OrderBy(x => rnd.Next()).ToList();

            foreach (var item in testSeries)
            {
                HashSet.Add(item);
                Assert.AreEqual(true, HashSet.Contains(item));
            }

            for (int i = 1; i <= nodeCount; i++)
            {
                HashSet.Remove(i);
                Assert.AreEqual(false, HashSet.Contains(i));
            }

        }
    }
}
