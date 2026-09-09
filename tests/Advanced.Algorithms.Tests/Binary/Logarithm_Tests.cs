using System;
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

        [TestMethod]
        public void Logarithm_Oracle_Vs_Math()
        {
            // values near powers of ten expose floor(log2(x))/floor(log2(10)) errors
            var samples = new[]
            {
                1, 2, 3, 8, 9, 10, 31, 32, 99, 100, 999, 1000, 1024, 9999, 10000, int.MaxValue
            };

            foreach (var x in samples)
            {
                Assert.AreEqual((int)Math.Floor(Math.Log(x, 2)), Logarithm.CalcBase2LogFloor(x),
                    $"base-2 floor log mismatch for {x}");
                Assert.AreEqual((int)Math.Floor(Math.Log10(x)), Logarithm.CalcBase10LogFloor(x),
                    $"base-10 floor log mismatch for {x}");
            }

            for (var x = 1; x <= 2000; x++)
            {
                Assert.AreEqual((int)Math.Floor(Math.Log(x, 2)), Logarithm.CalcBase2LogFloor(x));
                Assert.AreEqual((int)Math.Floor(Math.Log10(x)), Logarithm.CalcBase10LogFloor(x));
            }
        }
    }
}
