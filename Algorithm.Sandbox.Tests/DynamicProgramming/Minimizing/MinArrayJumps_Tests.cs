using Algorithm.Sandbox.DynamicProgramming.Minimizing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.Tests.DynamicProgramming.Minimizing
{
    /// <summary>
    /// Problem statement below
    /// http://www.geeksforgeeks.org/minimum-number-of-jumps-to-reach-end-of-a-given-array/
    /// </summary>
    [TestClass]
    public class MinArrayJumps_Tests
    {
        [TestMethod]
        public void MinArrayJumpsSmoke_Test()
        {
            Assert.AreEqual(3, MinArrayJumps.GetMinJumps(new int[] { 1, 3, 5, 8, 9, 2, 6, 7, 6, 8, 9 }));
        }
    }
}
