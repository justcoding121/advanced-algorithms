using System;
using System.Collections.Generic;
using System.Linq;
using Advanced.Algorithms.Search;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.Search
{
    [TestClass]
    public class BoyerMooreTests
    {
        [TestMethod]
        public void BoyerMoore_Majority_Finder_Test()
        {
            var elementCount = 1000;

            var rnd = new Random();
            var randomNumbers = new List<int>();

            while (randomNumbers.Count < elementCount / 2) randomNumbers.Add(rnd.Next(0, elementCount));

            var majorityElement = rnd.Next(0, elementCount);

            randomNumbers.AddRange(Enumerable.Repeat(majorityElement, elementCount / 2 + 1));
            randomNumbers = randomNumbers.OrderBy(x => rnd.Next()).ToList();

            var expected = majorityElement;
            var actual = BoyerMoore<int>.FindMajority(randomNumbers);

            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void BoyerMoore_No_Majority_Returns_Default()
        {
            var actual = BoyerMoore<int>.FindMajority(new[] { 1, 2, 3, 4 });

            Assert.AreEqual(0, actual);
        }

        [TestMethod]
        public void BoyerMoore_All_Identical()
        {
            Assert.AreEqual(5, BoyerMoore<int>.FindMajority(new[] { 5, 5, 5 }));
        }

        [TestMethod]
        public void BoyerMoore_Single_Element()
        {
            Assert.AreEqual(9, BoyerMoore<int>.FindMajority(new[] { 9 }));
        }

        [TestMethod]
        public void BoyerMoore_Oracle_Against_Count()
        {
            var rnd = new Random(19);
            var fixtures = new[]
            {
                new[] { 1, 2, 1, 1, 3, 1, 1 },
                new[] { 1, 2, 1, 2, 3 },
                new[] { 1, 1, 2, 2 },
                new[] { 7 },
                new[] { 4, 4, 4, 1, 2 },
                new[] { 9, 8, 9, 8, 9, 8, 9 }
            };

            foreach (var input in fixtures) AssertMajorityMatchesCount(input);

            for (var t = 0; t < 40; t++)
            {
                var n = rnd.Next(1, 40);
                var input = Enumerable.Range(0, n).Select(_ => rnd.Next(0, 5)).ToArray();
                AssertMajorityMatchesCount(input);
            }
        }

        private static void AssertMajorityMatchesCount(int[] input)
        {
            var actual = BoyerMoore<int>.FindMajority(input);
            var majority = input
                .GroupBy(x => x)
                .Where(g => g.Count() > input.Length / 2)
                .Select(g => (int?)g.Key)
                .FirstOrDefault();

            Assert.AreEqual(majority ?? 0, actual);
        }
    }
}
