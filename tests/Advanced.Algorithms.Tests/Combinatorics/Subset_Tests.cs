using System;
using System.Linq;
using Advanced.Algorithms.Combinatorics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.Combinatorics
{
    [TestClass]
    public class SubsetTests
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

        [TestMethod]
        public void Subset_Corner_Cases()
        {
            var input = "a".ToCharArray().ToList();
            var subsets = Subset.Find(input);
            Assert.AreEqual(2, subsets.Count);
            Assert.AreEqual(0, subsets[0].Count);
            CollectionAssert.AreEqual(new[] { 'a' }, subsets[1]);

            input = "ab".ToCharArray().ToList();
            subsets = Subset.Find(input);
            Assert.AreEqual(4, subsets.Count);
            Assert.AreEqual(0, subsets[0].Count);
            CollectionAssert.AreEqual(new[] { 'a' }, subsets[1]);
            CollectionAssert.AreEqual(new[] { 'a', 'b' }, subsets[2]);
            CollectionAssert.AreEqual(new[] { 'b' }, subsets[3]);
        }
    }
}