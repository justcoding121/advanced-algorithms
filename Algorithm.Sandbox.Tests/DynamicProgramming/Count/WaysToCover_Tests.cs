using Algorithm.Sandbox.DynamicProgramming.Count;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.Tests.DynamicProgramming.Count
{
    /// <summary>
    /// Problem statement below
    /// http://www.geeksforgeeks.org/count-number-of-ways-to-cover-a-distance/
    /// </summary>
    [TestClass]
    public class WaysToCover_Tests
    {
        [TestMethod]
        public void WaysToCoverDistance_Smoke_Test()
        {
            Assert.AreEqual(7, WaysToCoverDistance.GetWays(4));
        }
    }
}
