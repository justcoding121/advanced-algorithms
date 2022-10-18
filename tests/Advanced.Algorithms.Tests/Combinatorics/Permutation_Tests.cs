using System;
using System.Linq;
using Advanced.Algorithms.Combinatorics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.Combinatorics
{
    [TestClass]
    public class PermutationTests
    {
        //for verification
        static readonly Func<int, int> Factorial = n =>
            n == 0 ? 1 : Enumerable.Range(1, n).Aggregate((acc, x) => acc * x);

        //for verification
        static readonly Func<int, int, int> Permutation = (int n, int r)
            => n == 0 || r == 0 ? 1 : Factorial(n) / Factorial(n - r);

        [TestMethod]
        public void Permutation_With_Repetition_Smoke_Test()
        {
            var input = "".ToCharArray().ToList();
            var permuations = Algorithms.Combinatorics.Permutation.Find<char>(input, input.Count, true);
            Assert.AreEqual(Math.Pow(input.Count, input.Count), permuations.Count);

            input = "pen".ToCharArray().ToList();
            permuations = Algorithms.Combinatorics.Permutation.Find<char>(input, input.Count, true);
            Assert.AreEqual(Math.Pow(input.Count, input.Count), permuations.Count);

            input = "scan".ToCharArray().ToList();
            permuations = Algorithms.Combinatorics.Permutation.Find<char>(input, input.Count, true);
            Assert.AreEqual(Math.Pow(input.Count, input.Count), permuations.Count);

            input = "scan".ToCharArray().ToList();
            permuations = Algorithms.Combinatorics.Permutation.Find<char>(input, 2, true);
            Assert.AreEqual(Math.Pow(input.Count, 2), permuations.Count);

            input = "scan".ToCharArray().ToList();
            permuations = Algorithms.Combinatorics.Permutation.Find<char>(input, 3, true);
            Assert.AreEqual(Math.Pow(input.Count, 3), permuations.Count);

            input = "scaner".ToCharArray().ToList();
            permuations = Algorithms.Combinatorics.Permutation.Find<char>(input, 4, true);
            Assert.AreEqual(Math.Pow(input.Count, 4), permuations.Count);
        }


        [TestMethod]
        public void Permutation_Without_Repetitions_Smoke_Test()
        {
            var input = "".ToCharArray().ToList();
            var permuations = Algorithms.Combinatorics.Permutation.Find<char>(input, input.Count);
            Assert.AreEqual(Permutation(input.Count, input.Count), permuations.Count);

            input = "cookie".ToCharArray().ToList();
            permuations = Algorithms.Combinatorics.Permutation.Find<char>(input, input.Count);
            Assert.AreEqual(Permutation(input.Count, input.Count), permuations.Count);

            input = "monster".ToCharArray().ToList();
            permuations = Algorithms.Combinatorics.Permutation.Find<char>(input, input.Count);
            Assert.AreEqual(Permutation(input.Count, input.Count), permuations.Count);

            input = "cookie".ToCharArray().ToList();
            permuations = Algorithms.Combinatorics.Permutation.Find<char>(input, 2);
            Assert.AreEqual(Permutation(input.Count, 2), permuations.Count);

            input = "monster".ToCharArray().ToList();
            permuations = Algorithms.Combinatorics.Permutation.Find<char>(input, 3);
            Assert.AreEqual(Permutation(input.Count, 3), permuations.Count);
        }
    }
}