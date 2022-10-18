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
    }
}