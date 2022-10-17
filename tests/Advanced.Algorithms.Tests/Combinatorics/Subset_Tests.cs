using System;
using System.Linq;
using Advanced.Algorithms.Combinatorics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.Combinatorics
{
    [TestClass]
    public class Subset_Tests
    {
        [TestMethod]
        public void Subset_Smoke_Test()
        {
            var input = "".ToCharArray().ToList();
            var subsets = Subset.Find(input);
            Assert.AreEqual(Math.Pow(2, input.Count), subsets.Count);

            input = "cookie".ToCharArray().ToList();
            subsets = Subset.Find(input);
            Assert.AreEqual(Math.Pow(2, input.Count), subsets.Count);

            input = "monster".ToCharArray().ToList();
            subsets = Subset.Find(input);
            Assert.AreEqual(Math.Pow(2, input.Count), subsets.Count);
        }
    }
}