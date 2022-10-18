using Advanced.Algorithms.Binary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.Binary
{
    [TestClass]
    public class GcdTests
    {
        [TestMethod]
        public void GCD_Smoke_Test()
        {
            Assert.AreEqual(3, Gcd.Find(-9, 3));
            Assert.AreEqual(15, Gcd.Find(45, 30));

            Assert.AreEqual(1, Gcd.Find(3, 5));
        }
    }
}