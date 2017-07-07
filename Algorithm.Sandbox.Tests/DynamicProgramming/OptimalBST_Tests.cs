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
    public class OptimalBST_Tests
    {
        //[TestMethod]
        public void Smoke_Test()
        {
            Assert.AreEqual(142, OptimalBST.FindOptimalCost(
                new int[] { 10, 12, 20 },
                new int[] { 34, 8, 50 }
                ));
        }
    }
}
