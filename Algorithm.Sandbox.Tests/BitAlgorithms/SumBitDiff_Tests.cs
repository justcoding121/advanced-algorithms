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
    /// http://www.geeksforgeeks.org/sum-of-bit-differences-among-all-pairs/
    /// </summary>
    [TestClass]
    public class SumBitDiff_Tests
    {
        //[TestMethod]
        public void SumBitDiff_Smoke_Test()
        {
            Assert.AreEqual(8, SumBitDiff.Sum(new int[] { 1, 3, 5 }));
        }
    }
}
