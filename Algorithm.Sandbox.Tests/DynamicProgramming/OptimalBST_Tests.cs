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
    /// http://www.geeksforgeeks.org/dynamic-programming-set-24-optimal-binary-search-tree/
    /// </summary>
    [TestClass]
    public class OptimalBST_Tests
    {
        [TestMethod]
        public void OptimalBST_Smoke_Test()
        {
            Assert.AreEqual(118, OptimalBST.FindOptimalCost(
               new int[] { 10, 12 },
               new int[] { 34, 50 }
               ));

            Assert.AreEqual(142, OptimalBST.FindOptimalCost(
                new int[] { 10, 12, 20 },
                new int[] { 34, 8, 50 }
                ));

            Assert.AreEqual(415, OptimalBST.FindOptimalCost(
              new int[] { 10, 12, 20, 12, 15, 18 },
              new int[] { 34, 8, 50, 23, 50, 31 }
              ));
        }
    }
}
