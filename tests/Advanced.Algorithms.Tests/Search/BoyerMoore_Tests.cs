﻿using System;
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
    }
}