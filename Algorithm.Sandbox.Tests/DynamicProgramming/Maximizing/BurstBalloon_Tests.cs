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
    /// https://leetcode.com/problems/burst-balloons/#/description
    /// </summary>
    public class BurstBalloon_Tests
    {
        public void SmokeTest()
        {
            Assert.AreEqual(167, BurstBalloon.MaxCoins(new int[] { 3, 1, 5, 8 }));
        }
    }
}
