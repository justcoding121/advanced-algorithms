using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using Advanced.Algorithms.Combinatorics;

namespace Advanced.Algorithms.Tests.Combinatorics
{
    [TestClass]
    public class Combination_Tests
    {
        //for verification
        static readonly Func<int, int> factorial = n => n == 0 ? 1 :
            Enumerable.Range(1, n).Aggregate((acc, x) => acc * x);

        //for verification
        static readonly Func<int, int, int> combination = (int n, int r)
            => n == 0 || r == 0 ? 0 : factorial(n) / (factorial(r) * factorial(n - r));

        [TestMethod]
        public void Combination_Without_Repetitions_Smoke_Test()
        {
            var input = "".ToCharArray().ToList();
            var combinations = Combination.Find<char>(input, 2, false);
            Assert.AreEqual(combination(input.Count, 2), combinations.Count);

            input = "cookie".ToCharArray().ToList();
            combinations = Combination.Find<char>(input, 3, false);
            Assert.AreEqual(combination(input.Count, 3), combinations.Count);

            input = "monster".ToCharArray().ToList();
            combinations = Combination.Find<char>(input, 4, false);
            Assert.AreEqual(combination(input.Count, 4), combinations.Count);
        }


        [TestMethod]
        public void Combination_With_Repetitions_Smoke_Test()
        {
            var input = "".ToCharArray().ToList();
            var combinations = Combination.Find<char>(input, 3, true);
            Assert.AreEqual(0, combinations.Count);

            input = "pen".ToCharArray().ToList();
            combinations = Combination.Find<char>(input, 2, true);
            Assert.AreEqual(combination(input.Count + 2 - 1, 2), combinations.Count);

            input = "scan".ToCharArray().ToList();
            combinations = Combination.Find<char>(input, 3, true);
            Assert.AreEqual(combination(input.Count + 3 - 1, 3), combinations.Count);
        }

    }
}
