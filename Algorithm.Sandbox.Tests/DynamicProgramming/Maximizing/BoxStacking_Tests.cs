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
    /// http://www.geeksforgeeks.org/dynamic-programming-set-21-box-stacking-problem/
    /// </summary>
    [TestClass]
    public class BoxStacking_Tests
    {
        [TestMethod]
        public void SmokeTest()
        {
           Assert.AreEqual(60, BoxStacking.GetMaxHeight(new List<int[]>() {
                new int[] { 4, 6, 7 },
                new int[] { 1, 2, 3 },
                new int[] { 4, 5, 6 },
                new int[] { 10, 12, 32 }
            }));

        }
    }
}
