using System;
using System.Linq;
using Advanced.Algorithms.Combinatorics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.Combinatorics
{
    [TestClass]
    public class PermutationTests
    {
        // P(n,r); P(n,0)=1; P(0,r)=0 for r>0
        static int PermutationCount(int n, int r)
        {
            if (r < 0 || r > n) return 0;
            long result = 1;
            for (var i = 0; i < r; i++)
                result *= n - i;
            return (int)result;
        }

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
            Assert.AreEqual(PermutationCount(input.Count, input.Count), permuations.Count);

            input = "cookie".ToCharArray().ToList();
            permuations = Algorithms.Combinatorics.Permutation.Find<char>(input, input.Count);
            Assert.AreEqual(PermutationCount(input.Count, input.Count), permuations.Count);

            input = "monster".ToCharArray().ToList();
            permuations = Algorithms.Combinatorics.Permutation.Find<char>(input, input.Count);
            Assert.AreEqual(PermutationCount(input.Count, input.Count), permuations.Count);

            input = "cookie".ToCharArray().ToList();
            permuations = Algorithms.Combinatorics.Permutation.Find<char>(input, 2);
            Assert.AreEqual(PermutationCount(input.Count, 2), permuations.Count);

            input = "monster".ToCharArray().ToList();
            permuations = Algorithms.Combinatorics.Permutation.Find<char>(input, 3);
            Assert.AreEqual(PermutationCount(input.Count, 3), permuations.Count);
        }

        [TestMethod]
        public void Permutation_Without_Repetitions_Corner_Cases()
        {
            var input = "a".ToCharArray().ToList();
            var permutations = Algorithms.Combinatorics.Permutation.Find(input, 1);
            Assert.AreEqual(1, permutations.Count);
            CollectionAssert.AreEqual(new[] { 'a' }, permutations[0]);

            permutations = Algorithms.Combinatorics.Permutation.Find(input, 0);
            Assert.AreEqual(1, permutations.Count);
            Assert.AreEqual(0, permutations[0].Count);

            permutations = Algorithms.Combinatorics.Permutation.Find(input, 2);
            Assert.AreEqual(0, permutations.Count);

            input = "ab".ToCharArray().ToList();
            permutations = Algorithms.Combinatorics.Permutation.Find(input, 2);
            Assert.AreEqual(2, permutations.Count);
            CollectionAssert.AreEqual(new[] { 'a', 'b' }, permutations[0]);
            CollectionAssert.AreEqual(new[] { 'b', 'a' }, permutations[1]);
        }

        [TestMethod]
        public void Permutation_With_Repetition_Corner_Cases()
        {
            var input = "a".ToCharArray().ToList();
            var permutations = Algorithms.Combinatorics.Permutation.Find(input, 2, true);
            Assert.AreEqual(1, permutations.Count);
            CollectionAssert.AreEqual(new[] { 'a', 'a' }, permutations[0]);

            permutations = Algorithms.Combinatorics.Permutation.Find(input, 0, true);
            Assert.AreEqual(1, permutations.Count);
            Assert.AreEqual(0, permutations[0].Count);
        }

        [TestMethod]
        public void Permutation_Oracle_Count_Formula()
        {
            for (var n = 0; n <= 6; n++)
            {
                var input = Enumerable.Range(0, n).ToList();
                for (var r = 0; r <= n + 1; r++)
                {
                    Assert.AreEqual(PermutationCount(n, r),
                        Algorithms.Combinatorics.Permutation.Find(input, r, false).Count,
                        $"P({n},{r})");

                    var withRepExpected = r == 0 ? 1 : (int)Math.Pow(n, r);
                    Assert.AreEqual(withRepExpected,
                        Algorithms.Combinatorics.Permutation.Find(input, r, true).Count,
                        $"Prep({n},{r})");
                }
            }
        }
    }
}