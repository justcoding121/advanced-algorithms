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
    /// http://www.geeksforgeeks.org/dynamic-programming-set-13-cutting-a-rod/
    /// </summary>
    [TestClass]
    public class CuttingRod_Tests
    {
        [TestMethod]
        public void CuttingRod_Smoke_Test()
        {
            Assert.AreEqual(22, CuttingRod.GetMaxProfit(
                new int[] { 1, 2, 3, 4, 5, 6, 7, 8 },
                new int[] { 1, 5, 8, 9, 10, 17, 17, 20 }
                ));
        }
    }
}
