using Algorithm.Sandbox.BitAlgorithms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.Tests.BitAlgorithms
{
    /// <summary>
    ///  checks if any 8-bit byte in 32-bit word is 0
    /// </summary>
    [TestClass]
    public class CheckWordForZerobyte_Tests
    {
        //[TestMethod]
        public void CheckWordForZerobyte_Smoke_Test()
        {
            Assert.IsTrue(CheckWordForZeroByte.HasZeroBytes(1));
            Assert.IsFalse(CheckWordForZeroByte.HasZeroBytes(int.MaxValue));
        }
    }
}
