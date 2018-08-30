using Advanced.Algorithms.Numerical;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.Numerical
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
