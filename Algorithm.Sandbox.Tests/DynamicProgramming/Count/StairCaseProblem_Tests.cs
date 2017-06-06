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
    /// http://www.geeksforgeeks.org/count-ways-reach-nth-stair/
    /// </summary>
    [TestClass]
    public class StairCaseProblem_Tests
    {
        [TestMethod]
        public void StairCase_SmokeTest()
        {
            Assert.AreEqual(1, StairCaseProblem.CountWays(1));
            Assert.AreEqual(2, StairCaseProblem.CountWays(2));
            Assert.AreEqual(5, StairCaseProblem.CountWays(4));

        }
    }
}
