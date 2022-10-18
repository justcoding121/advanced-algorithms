using Advanced.Algorithms.Numerical;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.Numerical
{
    [TestClass]
    public class PrimeGeneratorTests
    {
        [TestMethod]
        public void Prime_Generation_Smoke_Test()
        {
            Assert.AreEqual(5, PrimeGenerator.GetAllPrimes(11).Count);
            Assert.AreEqual(8, PrimeGenerator.GetAllPrimes(20).Count);
        }
    }
}