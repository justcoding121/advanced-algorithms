using System;
using System.Linq;
using Advanced.Algorithms.Search;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.Search
{
    [TestClass]
    public class BinarySearchTests
    {
        [TestMethod]
        public void Search_Smoke_Test()
        {
            var test = new[]
            {
                2, 3, 5, 7, 11, 13, 17, 19,
                23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79
            };

            Assert.AreEqual(15, BinarySearch.Search(test, 53));
            Assert.AreEqual(-1, BinarySearch.Search(test, 80));
        }

        [TestMethod]
        public void Search_Single_Element()
        {
            Assert.AreEqual(0, BinarySearch.Search(new[] { 7 }, 7));
            Assert.AreEqual(-1, BinarySearch.Search(new[] { 7 }, 8));
        }

        [TestMethod]
        public void Search_First_And_Last()
        {
            var test = new[] { 1, 3, 5, 7, 9 };

            Assert.AreEqual(0, BinarySearch.Search(test, 1));
            Assert.AreEqual(4, BinarySearch.Search(test, 9));
            Assert.AreEqual(-1, BinarySearch.Search(test, 4));
        }

        [TestMethod]
        public void Search_Empty_Returns_Minus_One()
        {
            Assert.AreEqual(-1, BinarySearch.Search(Array.Empty<int>(), 1));
        }

        [TestMethod]
        public void Search_Oracle_Against_Known_Indices()
        {
            var rnd = new Random(11);

            for (var t = 0; t < 50; t++)
            {
                var sorted = Enumerable.Range(0, rnd.Next(1, 40))
                    .Select(_ => rnd.Next(0, 100))
                    .Distinct()
                    .OrderBy(x => x)
                    .ToArray();

                foreach (var value in sorted)
                {
                    var index = BinarySearch.Search(sorted, value);
                    Assert.IsTrue(index >= 0);
                    Assert.AreEqual(value, sorted[index]);
                }

                Assert.AreEqual(-1, BinarySearch.Search(sorted, -1));
                Assert.AreEqual(-1, BinarySearch.Search(sorted, 101));
            }
        }
    }
}
