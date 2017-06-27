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
    /// http://www.geeksforgeeks.org/smallest-power-of-2-greater-than-or-equal-to-n/
    /// </summary>
    [TestClass]
    public class NextPowOfTwo_Tests
    {
        //[TestMethod]
        public void SmokeTest()
        {
            Assert.AreEqual(8, NextPowOfTwo.Next(5));
        }
    }
}
