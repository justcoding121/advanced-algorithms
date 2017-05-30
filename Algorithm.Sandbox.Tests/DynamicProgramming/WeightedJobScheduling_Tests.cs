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
    /// http://www.geeksforgeeks.org/weighted-job-scheduling/
    /// </summary>
    [TestClass]
    public class WeightedJobScheduling_Tests
    {
        [TestMethod]
        public void SmokeTest()
        {
            Assert.AreEqual(250, WeightedJobScheduling.GetMaxProfit(new List<WeightedJob>()
            {
                new WeightedJob(new int[] {1, 2, 50}),
                new WeightedJob(new int[] {3, 5, 20}),
                new WeightedJob(new int[] {6, 19, 100}),
                new WeightedJob(new int[] {2, 100, 200})
            }));
        }
    }
}
