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

        [TestMethod]
        public void Prime_Generation_Corner_Cases()
        {
            CollectionAssert.AreEqual(new[] { 2 }, PrimeGenerator.GetAllPrimes(2));
            CollectionAssert.AreEqual(new[] { 2, 3 }, PrimeGenerator.GetAllPrimes(3));
            CollectionAssert.AreEqual(new[] { 2, 3, 5, 7 }, PrimeGenerator.GetAllPrimes(9));
            CollectionAssert.AreEqual(
                new[] { 2, 3, 5, 7, 11, 13, 17, 19, 23 },
                PrimeGenerator.GetAllPrimes(25));
            CollectionAssert.AreEqual(
                new[] { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47 },
                PrimeGenerator.GetAllPrimes(49));
        }
    }
}