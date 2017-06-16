using Algorithm.Sandbox.NumericalMethods;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Algorithm.Sandbox.Tests.NumericalMethods
{
    [TestClass]
    public class Exponentiation_Tests
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
