using Advanced.Algorithms.Binary;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Advanced.Algorithms.Tests.Binary
{
    [TestClass]
    public class Logarithm_Tests
    {
        [TestMethod]
        public void Logarithm_Smoke_Test()
        {
            Assert.AreEqual(3, Logarithm.CalcBase2LogFloor(9));
            Assert.AreEqual(3, Logarithm.CalcBase2LogFloor(8));
            Assert.AreEqual(5, Logarithm.CalcBase2LogFloor(32));

            Assert.AreEqual(2, Logarithm.CalcBase10LogFloor(102));
            Assert.AreEqual(3, Logarithm.CalcBase10LogFloor(1000));
        }
    }
}
