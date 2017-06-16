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
    /// Problem statement in detail below
    /// http://www.geeksforgeeks.org/dice-throw-problem/
    /// </summary>
    [TestClass]
    public class DiceThrow_Tests
    {
        [TestMethod]
        public void DiceThrow_Smoke_Test()
        {
            Assert.AreEqual(0, DiceThrow.WaysToGetSum(1, 2, 4));
            Assert.AreEqual(2, DiceThrow.WaysToGetSum(3, 2, 2));
            Assert.AreEqual(21, DiceThrow.WaysToGetSum(8, 3, 6));
            Assert.AreEqual(4, DiceThrow.WaysToGetSum(5, 2, 4));
            Assert.AreEqual(6, DiceThrow.WaysToGetSum(5, 3, 4));

        }
    }
}
