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
    /// http://www.geeksforgeeks.org/next-higher-number-with-same-number-of-set-bits/
    /// </summary>
    [TestClass]
    public class NextNumberWithSameSetBits_Tests
    {
        [TestMethod]
        public void NextHighNumberWithSameSetBits_Smoke_Test()
        {
            Assert.AreEqual(163, NextNumberWithSameSetBits.NextHigh(156));
            Assert.AreEqual(6, NextNumberWithSameSetBits.NextHigh(5));
            Assert.AreEqual(13, NextNumberWithSameSetBits.NextHigh(11));

            Assert.AreEqual(156, NextNumberWithSameSetBits.NextSmall(163));
            Assert.AreEqual(5, NextNumberWithSameSetBits.NextSmall(6));
            Assert.AreEqual(11, NextNumberWithSameSetBits.NextSmall(13));
        }
    }
}
