using Advanced.Algorithms.Binary;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Advanced.Algorithms.Tests.Binary
{

    [TestClass]
    public class NextPowOfTwo_Tests
    {
        [TestMethod]
        public void NextPowOfTwo_Smoke_Test()
        {
            Assert.AreEqual(8, NextPowOfTwo.Next(5));
        }
    }
}
