using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advanced.Algorithms.Combinatorics;

namespace Advanced.Algorithms.Tests.Combinatorics
{
    [TestClass]
    public class Permutation_Tests
    {
        //for verification
        readonly Func<int, int> factorial = n => n == 0 ? 1 :
            Enumerable.Range(1, n).Aggregate((acc, x) => acc * x);


        [TestMethod]
        public void Permutation_Without_Repetitions_Smoke_Test()
        {
            var input = "".ToCharArray().ToList();
            var permuations = Permutation.Find<char>(input);
            Assert.AreEqual(factorial(input.Count), permuations.Count);

            input = "cookie".ToCharArray().ToList();
            permuations = Permutation.Find<char>(input);
            Assert.AreEqual(factorial(input.Count), permuations.Count);

            input = "monster".ToCharArray().ToList();
            permuations = Permutation.Find<char>(input);
            Assert.AreEqual(factorial(input.Count), permuations.Count);
        }


        [TestMethod]
        public void Permutation_With_Repetition_Smoke_Test()
        {
            var input = "".ToCharArray().ToList();
            var permuations = Permutation.Find<char>(input, true);
            Assert.AreEqual(Math.Pow(input.Count, input.Count), permuations.Count);

            input = "pen".ToCharArray().ToList();
            permuations = Permutation.Find<char>(input, true);
            Assert.AreEqual(Math.Pow(input.Count, input.Count), permuations.Count);

            input = "scan".ToCharArray().ToList();
            permuations = Permutation.Find<char>(input, true);
            Assert.AreEqual(Math.Pow(input.Count, input.Count), permuations.Count);
        }

        [TestMethod]
        public void Permutation_Without_Repetition_Without_Inversions_Smoke_Test()
        {
            var input = "".ToCharArray().ToList();
            var permuations = Permutation.Find<char>(input, false, false);
            Assert.AreEqual(factorial(input.Count) / 2, permuations.Count);

            input = "abc".ToCharArray().ToList();
            permuations = Permutation.Find<char>(input, false, false);
            Assert.AreEqual(factorial(input.Count) / 2, permuations.Count);

            input = "acde".ToCharArray().ToList();
            permuations = Permutation.Find<char>(input, false, false);
            Assert.AreEqual(factorial(input.Count) / 2, permuations.Count);
        }


        [TestMethod]
        public void Permutation_With_Repetition_Without_Inversions_Smoke_Test()
        {

            var input = "".ToCharArray().ToList();
            var permuations = Permutation.Find<char>(input, true, false);
            Assert.AreEqual(0, permuations.Count);

            input = "pen".ToCharArray().ToList();
            permuations = Permutation.Find<char>(input, true, false);
            Assert.AreEqual(9, permuations.Count);

            input = "cool".ToCharArray().ToList();
            permuations = Permutation.Find<char>(input, true, false);
            Assert.AreEqual(80, permuations.Count);
        }

       
    }
}
