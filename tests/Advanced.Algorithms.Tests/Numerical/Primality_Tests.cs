using System.Linq;
using Advanced.Algorithms.Numerical;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.Numerical
{
    [TestClass]
    public class PrimalityTests
    {
        private static readonly int[] PrimesTo100 =
        {
            2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47,
            53, 59, 61, 67, 71, 73, 79, 83, 89, 97
        };

        [TestMethod]
        public void Prime_Smoke_Test()
        {
            Assert.IsTrue(PrimeTester.IsPrime(11));
            Assert.IsFalse(PrimeTester.IsPrime(50));
            Assert.IsTrue(PrimeTester.IsPrime(101));
        }

        [TestMethod]
        public void Prime_Corner_Cases()
        {
            Assert.IsFalse(PrimeTester.IsPrime(-7));
            Assert.IsFalse(PrimeTester.IsPrime(0));
            Assert.IsFalse(PrimeTester.IsPrime(1));
            Assert.IsTrue(PrimeTester.IsPrime(2));
            Assert.IsTrue(PrimeTester.IsPrime(3));
            Assert.IsFalse(PrimeTester.IsPrime(4));
            Assert.IsFalse(PrimeTester.IsPrime(9));
            Assert.IsFalse(PrimeTester.IsPrime(25));
            Assert.IsFalse(PrimeTester.IsPrime(49));
            Assert.IsTrue(PrimeTester.IsPrime(29));
        }

        [TestMethod]
        public void Prime_Oracle_Vs_Known_List()
        {
            for (var n = -5; n <= 100; n++)
            {
                var expected = PrimesTo100.Contains(n);
                Assert.AreEqual(expected, PrimeTester.IsPrime(n), $"IsPrime({n})");
            }

            // additional composites / primes beyond 100
            Assert.IsTrue(PrimeTester.IsPrime(101));
            Assert.IsTrue(PrimeTester.IsPrime(103));
            Assert.IsFalse(PrimeTester.IsPrime(121)); // 11^2
            Assert.IsFalse(PrimeTester.IsPrime(143)); // 11*13
            Assert.IsTrue(PrimeTester.IsPrime(997));
        }
    }
}