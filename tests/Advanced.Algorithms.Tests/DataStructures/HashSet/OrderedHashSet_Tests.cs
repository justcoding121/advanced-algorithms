using System;
using System.Linq;
using Advanced.Algorithms.DataStructures.Foundation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.DataStructures
{
    [TestClass]
    public class OrderedHashSet_Tests
    {
        /// <summary>
        ///     key value HashSet tests
        /// </summary>
        [TestMethod]
        public void OrderedHashSet_Test()
        {
            var hashSet = new OrderedHashSet<int>();

            var nodeCount = 1000;

            //insert test
            for (var i = 0; i <= nodeCount; i++)
            {
                hashSet.Add(i);
                Assert.AreEqual(true, hashSet.Contains(i));
            }

            //IEnumerable test using linq
            Assert.AreEqual(hashSet.Count, hashSet.Count());
            Assert.AreEqual(hashSet.Count, hashSet.AsEnumerableDesc().Count());

            for (var i = 0; i <= nodeCount; i++)
            {
                hashSet.Remove(i);
                Assert.AreEqual(false, hashSet.Contains(i));
            }

            //IEnumerable test using linq
            Assert.AreEqual(hashSet.Count, hashSet.Count());
            Assert.AreEqual(hashSet.Count, hashSet.AsEnumerableDesc().Count());

            var rnd = new Random();
            var testSeries = Enumerable.Range(1, nodeCount).OrderBy(x => rnd.Next()).ToList();

            foreach (var item in testSeries)
            {
                hashSet.Add(item);
                Assert.AreEqual(true, hashSet.Contains(item));
            }

            //IEnumerable test using linq
            Assert.AreEqual(hashSet.Count, hashSet.Count());
            Assert.AreEqual(hashSet.Count, hashSet.AsEnumerableDesc().Count());

            for (var i = 1; i <= nodeCount; i++)
            {
                hashSet.Remove(i);
                Assert.AreEqual(false, hashSet.Contains(i));
            }

            //IEnumerable test using linq
            Assert.AreEqual(hashSet.Count, hashSet.Count());
            Assert.AreEqual(hashSet.Count, hashSet.AsEnumerableDesc().Count());
        }
    }
}