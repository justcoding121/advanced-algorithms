using System;
using System.Linq;
using Advanced.Algorithms.DataStructures.Foundation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.DataStructures
{
    [TestClass]
    public class OrderedDictionary_Tests
    {
        /// <summary>
        ///     key value dictionary tests
        /// </summary>
        [TestMethod]
        public void OrderedDictionary_Test()
        {
            var dictionary = new OrderedDictionary<int, int>();

            var nodeCount = 1000;

            //insert test
            for (var i = 0; i <= nodeCount; i++)
            {
                dictionary.Add(i, i);
                Assert.AreEqual(true, dictionary.ContainsKey(i));
            }

            //IEnumerable test using linq
            Assert.AreEqual(dictionary.Count, dictionary.Count());
            Assert.AreEqual(dictionary.Count, dictionary.AsEnumerableDesc().Count());

            for (var i = 0; i <= nodeCount; i++)
            {
                dictionary.Remove(i);
                Assert.AreEqual(false, dictionary.ContainsKey(i));
            }

            //IEnumerable test using linq
            Assert.AreEqual(dictionary.Count, dictionary.Count());
            Assert.AreEqual(dictionary.Count, dictionary.AsEnumerableDesc().Count());

            var rnd = new Random();
            var testSeries = Enumerable.Range(1, nodeCount).OrderBy(x => rnd.Next()).ToList();

            foreach (var item in testSeries)
            {
                dictionary.Add(item, item);
                Assert.AreEqual(true, dictionary.ContainsKey(item));
            }

            //IEnumerable test using linq
            Assert.AreEqual(dictionary.Count, dictionary.Count());
            Assert.AreEqual(dictionary.Count, dictionary.AsEnumerableDesc().Count());

            for (var i = 1; i <= nodeCount; i++)
            {
                dictionary.Remove(i);
                Assert.AreEqual(false, dictionary.ContainsKey(i));
            }

            //IEnumerable test using linq
            Assert.AreEqual(dictionary.Count, dictionary.Count());
            Assert.AreEqual(dictionary.Count, dictionary.AsEnumerableDesc().Count());
        }
    }
}