using Algorithm.Sandbox.BitAlgorithms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.Tests.BitAlgorithms
{
    public class CountBits_Tests
    {
        public void CountBits_Smoke_Test()
        {
            Assert.AreEqual(32, CountBits.CountSetBits(int.MaxValue));
        }
    }
}
