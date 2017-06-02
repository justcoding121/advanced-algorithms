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
    /// http://www.geeksforgeeks.org/write-an-efficient-c-program-to-reverse-bits-of-a-number/
    /// </summary>
    [TestClass]
    public class ReverseBits_Tests
    {
        [TestMethod]
        public void SmokeTest()
        {
            Assert.AreEqual(1, ReverseBits.Reverse(2147483648));
        }
    }
}
