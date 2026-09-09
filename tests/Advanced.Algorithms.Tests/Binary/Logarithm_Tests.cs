using Advanced.Algorithms.Binary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.Binary
{
    [TestClass]
    public class LogarithmTests
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

        [TestMethod]
        public void Logarithm_Edge_Cases()
        {
            Assert.AreEqual(0, Logarithm.CalcBase2LogFloor(1));
            Assert.AreEqual(1, Logarithm.CalcBase2LogFloor(2));
            Assert.AreEqual(1, Logarithm.CalcBase2LogFloor(3));
            Assert.AreEqual(4, Logarithm.CalcBase2LogFloor(16));
            Assert.AreEqual(4, Logarithm.CalcBase2LogFloor(31));
            Assert.AreEqual(30, Logarithm.CalcBase2LogFloor(int.MaxValue));

            Assert.AreEqual(0, Logarithm.CalcBase10LogFloor(1));
            Assert.AreEqual(1, Logarithm.CalcBase10LogFloor(10));
            Assert.AreEqual(1, Logarithm.CalcBase10LogFloor(31));
            Assert.AreEqual(2, Logarithm.CalcBase10LogFloor(100));
            Assert.AreEqual(3, Logarithm.CalcBase10LogFloor(1024));
        }
    }
}
