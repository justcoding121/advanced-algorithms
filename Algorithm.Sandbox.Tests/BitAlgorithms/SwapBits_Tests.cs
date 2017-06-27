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
    /// http://www.geeksforgeeks.org/swap-bits-in-a-given-number/
    /// </summary>
    [TestClass]
    public class SwapBits_Tests
    {
        //[TestMethod]
        public void SmokeTest()
        {
            Assert.AreEqual(227, SwapBits.Swap(47, 1, 5, 3));
            Assert.AreEqual(7 , SwapBits.Swap(28, 0, 3, 2));
        }
    }
}
