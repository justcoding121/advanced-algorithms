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
    /// http://www.geeksforgeeks.org/find-the-maximum-subarray-xor-in-a-given-array/
    /// </summary>
    [TestClass]
    public class MaxSubArrayXOR_Tests
    {
        [TestMethod]
        public void MaxSubArrayXOR_Smoke_Test()
        {
            Assert.AreEqual(7, MaxSubArrayXOR.FindMax(new int[] { 1, 2, 3, 4 }));
        }
    }
}
