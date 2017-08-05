using Algorithm.Sandbox.BitAlgorithms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.Tests.BitAlgorithms
{
    public class CalcLogarithm_Tests
    {
        public void CalcLogarithm_Smoke_Test()
        {
            Assert.AreEqual(3, CalcLogarithm.CalcBase2Log(8));
            Assert.AreEqual(10, CalcLogarithm.CalcBase10Log(100));
        }
    }
}
