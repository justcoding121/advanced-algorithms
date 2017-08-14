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
    /// Problem details below
    /// http://www.geeksforgeeks.org/program-to-count-number-of-set-bits-in-an-big-array/
    /// </summary>
    [TestClass]
    public class SetBitsBigArray_Tests
    {
        [TestMethod]
        public void SetBitsBigArray_Smoke_Test()
        {
            Assert.AreEqual(0, SetBitsBigArray.CountSetBits(new int[] { 3, 5, 7, 8 }));
        }
    }
}
