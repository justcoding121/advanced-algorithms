using Advanced.Algorithms.Numerical;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.Numerical
{
    [TestClass]
    public class PrimalityTests
    {
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
    }
}