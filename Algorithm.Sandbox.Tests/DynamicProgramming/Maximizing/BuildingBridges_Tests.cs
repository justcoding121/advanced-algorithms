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
    /// Problem details below
    /// http://www.geeksforgeeks.org/dynamic-programming-building-bridges/
    /// </summary>
    [TestClass]
    public class BuildingBridges_Tests
    {
        [TestMethod]
        public void Building_Bridges_SmokeTest()
        {
            Assert.AreEqual(5, BuildingBridges
                .GetMaxBridges(
                new int[] { 1, 2, 3, 4, 5, 6, 7, 8 },
                new int[] { 5, 1, 8, 3, 4, 2, 6, 7 }
                ));
        }
    }
}
