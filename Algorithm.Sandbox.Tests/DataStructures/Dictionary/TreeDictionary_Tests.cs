using Algorithm.Sandbox.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Algorithm.Sandbox.Tests.DataStructures
{
    [TestClass]
    public class TreeDictionary_Tests
    {
        /// <summary>
        /// key value dictionary tests 
        /// </summary>
        [TestMethod]
        public void TreeDictionary_Test()
        {
            var Dictionary = new AsTreeDictionary<int, int>();

            int nodeCount = 1000 * 10;
            //insert test


            for (int i = 0; i <= nodeCount; i++)
            {
                Dictionary.Add(i, i);
                Assert.AreEqual(true, Dictionary.ContainsKey(i));
            }


            for (int i = 0; i <= nodeCount; i++)
            {
                Dictionary.Remove(i);
                Assert.AreEqual(false, Dictionary.ContainsKey(i));
            }


            var rnd = new Random();
            var testSeries = Enumerable.Range(1, nodeCount).OrderBy(x => rnd.Next()).ToList();

            foreach (var item in testSeries)
            {
                Dictionary.Add(item, item);
                Assert.AreEqual(true, Dictionary.ContainsKey(item));
            }

            for (int i = 1; i <= nodeCount; i++)
            {
                Dictionary.Remove(i);
                Assert.AreEqual(false, Dictionary.ContainsKey(i));
            }

        }
    }
}
