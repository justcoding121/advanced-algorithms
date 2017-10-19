using Advanced.Algorithms.BitAlgorithms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced.Algorithms.Tests.BitAlgorithms
{
    /// <summary>
    /// Problem details below
    /// http://www.geeksforgeeks.org/write-an-efficient-c-program-to-reverse-bits-of-a-number/
    /// </summary>
    [TestClass]
    public class ReverseBits_Tests
    {
        [TestMethod]
        public void ReverseBits_Smoke_Test()
        {
            Assert.AreEqual((uint)1, ReverseBits.Reverse(2147483648));
        }
    }
}
