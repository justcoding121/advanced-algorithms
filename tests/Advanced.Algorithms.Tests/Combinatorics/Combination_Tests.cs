using System;
using System.Linq;
using Advanced.Algorithms.Combinatorics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.Combinatorics
{
    [TestClass]
    public class CombinationTests
    {
        //for verification
        static readonly Func<int, int> Factorial = n =>
            n == 0 ? 1 : Enumerable.Range(1, n).Aggregate((acc, x) => acc * x);

        //for verification
        static readonly Func<int, int, int> Combination = (int n, int r)
            => n == 0 ? 0 : Factorial(n) / (Factorial(r) * Factorial(n - r));

        [TestMethod]
        public void Combination_Without_Repetitions_Smoke_Test()
        {
            var input = "".ToCharArray().ToList();
            var combinations = Algorithms.Combinatorics.Combination.Find<char>(input, 2, false);
            Assert.AreEqual(Combination(input.Count, 2), combinations.Count);

            input = "cookie".ToCharArray().ToList();
            combinations = Algorithms.Combinatorics.Combination.Find<char>(input, 3, false);
            Assert.AreEqual(Combination(input.Count, 3), combinations.Count);

            input = "monster".ToCharArray().ToList();
            combinations = Algorithms.Combinatorics.Combination.Find<char>(input, 4, false);
            Assert.AreEqual(Combination(input.Count, 4), combinations.Count);

            input = "pen".ToCharArray().ToList();
            combinations = Algorithms.Combinatorics.Combination.Find<char>(input, 0, false);
            Assert.AreEqual(Combination(input.Count, 0), combinations.Count);

            input = "pen".ToCharArray().ToList();
            combinations = Algorithms.Combinatorics.Combination.Find<char>(input, input.Count, false);
            Assert.AreEqual(Combination(input.Count, input.Count), combinations.Count);
        }


        [TestMethod]
        public void Combination_With_Repetitions_Smoke_Test()
        {
            var input = "".ToCharArray().ToList();
            var combinations = Algorithms.Combinatorics.Combination.Find<char>(input, 3, true);
            Assert.AreEqual(0, combinations.Count);

            input = "pen".ToCharArray().ToList();
            combinations = Algorithms.Combinatorics.Combination.Find<char>(input, 2, true);
            Assert.AreEqual(Combination(input.Count + 2 - 1, 2), combinations.Count);

            input = "scan".ToCharArray().ToList();
            combinations = Algorithms.Combinatorics.Combination.Find<char>(input, 3, true);
            Assert.AreEqual(Combination(input.Count + 3 - 1, 3), combinations.Count);

            input = "scan".ToCharArray().ToList();
            combinations = Algorithms.Combinatorics.Combination.Find<char>(input, 0, true);
            Assert.AreEqual(Combination(input.Count + 0 - 1, 0), combinations.Count);

            input = "scan".ToCharArray().ToList();
            combinations = Algorithms.Combinatorics.Combination.Find<char>(input, input.Count, true);
            Assert.AreEqual(Combination(input.Count + input.Count - 1, input.Count), combinations.Count);
        }
    }
}