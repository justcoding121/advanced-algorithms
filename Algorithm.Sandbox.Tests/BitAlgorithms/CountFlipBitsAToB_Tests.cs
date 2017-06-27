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
    /// http://www.geeksforgeeks.org/count-number-of-bits-to-be-flipped-to-convert-a-to-b/
    /// </summary>
    [TestClass]
    public class CountFlipBitsAToB_Tests
    {
        //[TestMethod]
        public void SmokeTest()
        {
            Assert.AreEqual(4, CountFlipBitsAToB.Count(146, 137));
        }
    }
}
