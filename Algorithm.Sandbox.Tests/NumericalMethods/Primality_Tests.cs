using Algorithm.Sandbox.NumericalMethods;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Algorithm.Sandbox.Tests.NumericalMethods
{
    [TestClass]
    public class Primality_Tests
    {
        [TestMethod]
        public void Smoke_Test_Prime()
        {
            Assert.IsTrue(PrimeTester.IsPrime(11));
            Assert.IsFalse(PrimeTester.IsPrime(50));
            Assert.IsTrue(PrimeTester.IsPrime(101));
        }
    }
}
