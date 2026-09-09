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

        [TestMethod]
        public void Subset_Oracle_PowerSet_Count()
        {
            for (var n = 0; n <= 10; n++)
            {
                var input = Enumerable.Range(0, n).ToList();
                var subsets = Subset.Find(input);
                Assert.AreEqual(1 << n, subsets.Count, $"2^{n} subsets");

                // every subset size should appear C(n,k) times
                for (var k = 0; k <= n; k++)
                {
                    Assert.AreEqual(Binomial(n, k), subsets.Count(s => s.Count == k),
                        $"subsets of size {k} for n={n}");
                }
            }
        }

        private static int Binomial(int n, int k)
        {
            if (k < 0 || k > n) return 0;
            if (k == 0 || k == n) return 1;
            long result = 1;
            for (var i = 1; i <= k; i++)
                result = result * (n - k + i) / i;
            return (int)result;
        }
    }
}