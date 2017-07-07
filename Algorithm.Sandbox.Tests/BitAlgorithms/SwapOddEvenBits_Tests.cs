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
    /// http://www.geeksforgeeks.org/swap-all-odd-and-even-bits/
    /// </summary>
    [TestClass]
    public class SwapOddEvenBits_Tests
    {
        //[TestMethod]
        public void Smoke_Test()
        {
            Assert.AreEqual(43, SwapOddEvenBits.Swap(23));
        }
    }
}
