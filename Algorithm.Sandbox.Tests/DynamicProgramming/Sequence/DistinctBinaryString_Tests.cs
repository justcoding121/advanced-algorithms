using Algorithm.Sandbox.DynamicProgramming;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.Tests.DynamicProgramming
{
    /// <summary>
    /// Problem statement in detail below
    /// http://www.geeksforgeeks.org/count-number-binary-strings-without-consecutive-1s/
    /// </summary>
    [TestClass]
    public class DistinctBinaryString_Tests
    {
        [TestMethod]
        public void DistinctBinaryString_Smoke_Test()
        {
            Assert.AreEqual(2, DistinctBinaryString.Count(1));
            Assert.AreEqual(3, DistinctBinaryString.Count(2));
            Assert.AreEqual(5, DistinctBinaryString.Count(3));
            Assert.AreEqual(8, DistinctBinaryString.Count(4));
            Assert.AreEqual(13, DistinctBinaryString.Count(5));
        }
    }
}
