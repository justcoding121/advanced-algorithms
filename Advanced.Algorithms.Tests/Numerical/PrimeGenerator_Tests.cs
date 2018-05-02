using Microsoft.VisualStudio.TestTools.UnitTesting;
using Advanced.Algorithms.Numerical;

namespace Advanced.Algorithms.Tests.Numerical
{
    [TestClass]
    public class PrimeGenerator_Tests
    {
        [TestMethod]
        public void Prime_Generation_Smoke_Test()
        {
            Assert.AreEqual(5, PrimeGenerator.GetAllPrimes(11).Count);
            Assert.AreEqual(8, PrimeGenerator.GetAllPrimes(20).Count);
        }
    }
}
