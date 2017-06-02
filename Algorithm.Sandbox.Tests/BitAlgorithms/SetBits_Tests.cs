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
    /// problem details below
    /// http://www.geeksforgeeks.org/count-total-set-bits-in-all-numbers-from-1-to-n/
    /// </summary>
    [TestClass]
    public class SetBits_Tests
    {
        [TestMethod]
        public void SmokeTest()
        {
            Assert.AreEqual(4, SetBits.Count(3));
            Assert.AreEqual(9, SetBits.Count(6));
            Assert.AreEqual(12, SetBits.Count(7));
            Assert.AreEqual(13, SetBits.Count(8));
        }
    }
}
