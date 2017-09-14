using Algorithm.Sandbox.BitAlgorithms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.Tests.BitAlgorithms
{
    [TestClass]
    public class CalcLogarithm_Tests
    {
        [TestMethod]
        public void CalcLogarithm_Smoke_Test()
        {
            Assert.AreEqual(3, CalcLogarithm.CalcBase2LogFloor(9));
            Assert.AreEqual(3, CalcLogarithm.CalcBase2LogFloor(8));
            Assert.AreEqual(5, CalcLogarithm.CalcBase2LogFloor(32));

            Assert.AreEqual(2, CalcLogarithm.CalcBase10LogFloor(102));
            Assert.AreEqual(3, CalcLogarithm.CalcBase10LogFloor(1000));
        }
    }
}
