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
    /// http://www.geeksforgeeks.org/dynamic-programming-set-11-egg-dropping-puzzle/
    /// </summary>
    [TestClass]
    public class MinEggDrop_Tests
    {
        [TestMethod]
        public void Smoke_Test_MinEggDrop()
        {
            Assert.AreEqual(8, MinEggDrop.GetWays(36, 2));
        }
    }
}
