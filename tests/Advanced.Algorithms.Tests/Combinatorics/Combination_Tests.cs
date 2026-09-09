using System;
using System.Linq;
using Advanced.Algorithms.Combinatorics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.Combinatorics
{
    [TestClass]
    public class CombinationTests
    {
        // multiplicative C(n,r) avoids factorial overflow for oracle checks
        static int CombinationCount(int n, int r)
        {
            if (r < 0 || r > n) return 0;
            if (r == 0 || r == n) return 1;
            long result = 1;
            for (var i = 1; i <= r; i++)
                result = result * (n - r + i) / i;
            return (int)result;
        }

        // combinations with repetition: C(n+r-1, r)
        static int CombinationWithRepCount(int n, int r)
        {
            if (r == 0) return 1;
            if (n == 0) return 0;
            return CombinationCount(n + r - 1, r);
        }

        [TestMethod]
        public void Combination_Without_Repetitions_Smoke_Test()
        {
            var input = "".ToCharArray().ToList();
            var combinations = Algorithms.Combinatorics.Combination.Find<char>(input, 2, false);
            Assert.AreEqual(CombinationCount(input.Count, 2), combinations.Count);

            input = "cookie".ToCharArray().ToList();
            combinations = Algorithms.Combinatorics.Combination.Find<char>(input, 3, false);
            Assert.AreEqual(CombinationCount(input.Count, 3), combinations.Count);

            input = "monster".ToCharArray().ToList();
            combinations = Algorithms.Combinatorics.Combination.Find<char>(input, 4, false);
            Assert.AreEqual(CombinationCount(input.Count, 4), combinations.Count);

            input = "pen".ToCharArray().ToList();
            combinations = Algorithms.Combinatorics.Combination.Find<char>(input, 0, false);
            Assert.AreEqual(CombinationCount(input.Count, 0), combinations.Count);

            input = "pen".ToCharArray().ToList();
            combinations = Algorithms.Combinatorics.Combination.Find<char>(input, input.Count, false);
            Assert.AreEqual(CombinationCount(input.Count, input.Count), combinations.Count);
        }


        [TestMethod]
        public void Combination_With_Repetitions_Smoke_Test()
        {
            var input = "".ToCharArray().ToList();
            var combinations = Algorithms.Combinatorics.Combination.Find<char>(input, 3, true);
            Assert.AreEqual(0, combinations.Count);

            input = "pen".ToCharArray().ToList();
            combinations = Algorithms.Combinatorics.Combination.Find<char>(input, 2, true);
            Assert.AreEqual(CombinationWithRepCount(input.Count, 2), combinations.Count);

            input = "scan".ToCharArray().ToList();
            combinations = Algorithms.Combinatorics.Combination.Find<char>(input, 3, true);
            Assert.AreEqual(CombinationWithRepCount(input.Count, 3), combinations.Count);

            input = "scan".ToCharArray().ToList();
            combinations = Algorithms.Combinatorics.Combination.Find<char>(input, 0, true);
            Assert.AreEqual(CombinationWithRepCount(input.Count, 0), combinations.Count);

            input = "scan".ToCharArray().ToList();
            combinations = Algorithms.Combinatorics.Combination.Find<char>(input, input.Count, true);
            Assert.AreEqual(CombinationWithRepCount(input.Count, input.Count), combinations.Count);
        }

        [TestMethod]
        public void Combination_Without_Repetitions_Corner_Cases()
        {
            var input = "a".ToCharArray().ToList();
            var combinations = Algorithms.Combinatorics.Combination.Find(input, 1, false);
            Assert.AreEqual(1, combinations.Count);
            CollectionAssert.AreEqual(new[] { 'a' }, combinations[0]);

            combinations = Algorithms.Combinatorics.Combination.Find(input, 0, false);
            Assert.AreEqual(1, combinations.Count);
            Assert.AreEqual(0, combinations[0].Count);

            combinations = Algorithms.Combinatorics.Combination.Find(input, 2, false);
            Assert.AreEqual(0, combinations.Count);

            input = "ab".ToCharArray().ToList();
            combinations = Algorithms.Combinatorics.Combination.Find(input, 1, false);
            Assert.AreEqual(2, combinations.Count);
            CollectionAssert.AreEqual(new[] { 'a' }, combinations[0]);
            CollectionAssert.AreEqual(new[] { 'b' }, combinations[1]);
        }

        [TestMethod]
        public void Combination_With_Repetitions_Corner_Cases()
        {
            var input = "a".ToCharArray().ToList();
            var combinations = Algorithms.Combinatorics.Combination.Find(input, 2, true);
            Assert.AreEqual(1, combinations.Count);
            CollectionAssert.AreEqual(new[] { 'a', 'a' }, combinations[0]);

            combinations = Algorithms.Combinatorics.Combination.Find(input, 0, true);
            Assert.AreEqual(1, combinations.Count);
            Assert.AreEqual(0, combinations[0].Count);
        }

        [TestMethod]
        public void Combination_Oracle_Count_Formula()
        {
            for (var n = 0; n <= 7; n++)
            {
                var input = Enumerable.Range(0, n).ToList();
                for (var r = 0; r <= n + 1; r++)
                {
                    Assert.AreEqual(CombinationCount(n, r),
                        Algorithms.Combinatorics.Combination.Find(input, r, false).Count,
                        $"C({n},{r})");
                    Assert.AreEqual(CombinationWithRepCount(n, r),
                        Algorithms.Combinatorics.Combination.Find(input, r, true).Count,
                        $"Crep({n},{r})");
                }
            }

            // empty n, r=0 => one empty combination
            var empty = Algorithms.Combinatorics.Combination.Find("".ToCharArray().ToList(), 0, false);
            Assert.AreEqual(1, empty.Count);
            Assert.AreEqual(0, empty[0].Count);
        }
    }
}