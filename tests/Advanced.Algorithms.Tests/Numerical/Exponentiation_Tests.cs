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
    }
}