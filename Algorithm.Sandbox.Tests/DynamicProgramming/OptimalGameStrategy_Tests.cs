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
    /// Problem statement below
    /// http://www.geeksforgeeks.org/dynamic-programming-set-31-optimal-strategy-for-a-game/
    /// </summary>
    public class OptimalGameStrategy_Tests
    {
        //[TestMethod]
        public void Smoke_Tests()
        {
            Assert.AreEqual(22, OptimalGameStrategy.GetStrategy(new int[] { 8, 15, 3, 7 }));
            Assert.AreEqual(4, OptimalGameStrategy.GetStrategy(new int[] { 2, 2, 2, 2 }));
            Assert.AreEqual(42, OptimalGameStrategy.GetStrategy(new int[] { 20, 30, 2, 2, 2, 10 }));
        }
    }
}
