using System;
using Advanced.Algorithms.Numerical;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.Numerical
{
    [TestClass]
    public class ExponentiationTests
    {
        [TestMethod]
        public void Fast_Exponent_Smoke_Test()
        {
            var result = FastExponentiation.BySquaring(2, 5);

            Assert.AreEqual(32, result);

            result = FastExponentiation.BySquaring(2, 6);

            Assert.AreEqual(64, result);
        }

        [TestMethod]
        public void Fast_Exponent_Corner_Cases()
        {
            Assert.AreEqual(1, FastExponentiation.BySquaring(7, 0));
            Assert.AreEqual(7, FastExponentiation.BySquaring(7, 1));
            Assert.AreEqual(81, FastExponentiation.BySquaring(3, 4));
            Assert.AreEqual(243, FastExponentiation.BySquaring(3, 5));
            Assert.AreEqual(1, FastExponentiation.BySquaring(1, 100));
        }

        [TestMethod]
        public void Fast_Exponent_Oracle_Vs_MathPow()
        {
            for (var b = -5; b <= 8; b++)
            {
                if (b == 0)
                {
                    Assert.AreEqual(1, FastExponentiation.BySquaring(0, 0));
                    Assert.AreEqual(0, FastExponentiation.BySquaring(0, 1));
                    Assert.AreEqual(0, FastExponentiation.BySquaring(0, 5));
                    continue;
                }

                for (var p = 0; p <= 10; p++)
                {
                    // stay within int range
                    var expected = Math.Pow(b, p);
                    if (expected > int.MaxValue || expected < int.MinValue) continue;

                    Assert.AreEqual((int)expected, FastExponentiation.BySquaring(b, p),
                        $"{b}^{p}");
                }
            }

            Assert.AreEqual(-8, FastExponentiation.BySquaring(-2, 3));
            Assert.AreEqual(16, FastExponentiation.BySquaring(-2, 4));
            Assert.AreEqual(1, FastExponentiation.BySquaring(-1, 0));
            Assert.AreEqual(-1, FastExponentiation.BySquaring(-1, 5));
        }
    }
}