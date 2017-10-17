using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithm.Sandbox.Combinatorics;

namespace Algorithm.Sandbox.Tests.Combinatorics
{
    [TestClass]
    public class Variation_Tests
    {
        //for verification
        static readonly Func<int, int> factorial = n => n == 0 ? 1 :
            Enumerable.Range(1, n).Aggregate((acc, x) => acc * x);

        //for verification
        static readonly Func<int, int, int> combination = (int n, int r)
            => n == 0 || r == 0 ? 0 : factorial(n) / (factorial(r) * factorial(n - r));

        [TestMethod]
        public void Variation_Without_Repetitions_Smoke_Test()
        {
            var input = "".ToCharArray().ToList();
            var Variations = Variation.Find<char>(input, 2, false);
            Assert.AreEqual(combination(input.Count, 2) * factorial(2), Variations.Count);

            input = "cookie".ToCharArray().ToList();
            Variations = Variation.Find<char>(input, 3, false);
            Assert.AreEqual(combination(input.Count, 3) * factorial(3), Variations.Count);

            input = "monsters".ToCharArray().ToList();
            Variations = Variation.Find<char>(input, 3, false);
            Assert.AreEqual(combination(input.Count, 3) * factorial(3), Variations.Count);
        }


        [TestMethod]
        public void Variation_With_Repetitions_Smoke_Test()
        {
            var input = "abcd".ToCharArray().ToList();
            var variations = Variation.Find<char>(input, 2, true);
            Assert.AreEqual(Math.Pow(input.Count, 2), variations.Count);

            input = "scan".ToCharArray().ToList();
            variations = Variation.Find<char>(input, 3, true);
            Assert.AreEqual(Math.Pow(input.Count, 3), variations.Count);

            input = "".ToCharArray().ToList();
            variations = Variation.Find<char>(input, 3, true);
            Assert.AreEqual(0, variations.Count);
        }

    }
}
