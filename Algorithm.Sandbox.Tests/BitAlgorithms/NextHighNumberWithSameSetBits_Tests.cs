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
    public class NextHighNumberWithSameSetBits_Tests
    {
        [TestMethod]
        public void NextHighNumberWithSameSetBits_Smoke_Test()
        {
            Assert.AreEqual(163, NextHighNumberWithSameSetBits.Find(156));
        }
    }
}
