using Advanced.Algorithms.NumericalMethods;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.NumericalMethods
{
    [TestClass]
    public class Primality_Tests
    {
        [TestMethod]
        public void Prime_Smoke_Test()
        {
            Assert.IsTrue(PrimeTester.IsPrime(11));
            Assert.IsFalse(PrimeTester.IsPrime(50));
            Assert.IsTrue(PrimeTester.IsPrime(101));
        }
    }
}
